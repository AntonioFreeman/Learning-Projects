using Examenator.Classes;
using Examenator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Examenator.ViewModels
{
    public class ExamenViewModel: INotifyPropertyChanged
    {
        private int hours = 0;
        private int minutes = 0;
        private int seconds = 0;
        private int countTask = 1;
        private int timeEndOfExamen;               
        public Result Result {get; set; }
        public EventHandler CloseHandler; 
        public DispatcherTimer TimeExamen;       

        public ExamenViewModel(Result result, Examen examen)
        {
            Subject = examen.Subject;
            Result = result;
            CreateExamen(examen);
            CurrentTask = CurrentExamen.ElementAt(0);
            NumberTask = countTask.ToString();
            timeEndOfExamen = examen.TimeExamen;
            RunTimer();            
        }
        
        public void RunTimer()
        {
            TimeExamen= new DispatcherTimer();
            TimeExamen.Tick += new EventHandler(TimeExamen_Tick);
            TimeExamen.Interval = new TimeSpan(0, 0, 1);
            TimeExamen.Start();
        }
       
        public void EndExamen()
        {
            TimeExamen.Stop();
            Result.TimeExecute = new TimeSpan (hours, minutes, seconds);
            var resultWindow = new ResultWindow(Result);
            resultWindow.Show();
        }

        private void TimeExamen_Tick(object sender, EventArgs e)
        {
            seconds++;
            if (seconds == 60) { minutes++; seconds = 0; }
            if (minutes == 60) { hours++; minutes = 0; }
            if (hours * 60 + minutes == timeEndOfExamen) EndExamen();
            ElapsedTime = string.Format("Прошло времени: {0}:{1}:{2}", hours, minutes, seconds);
        }

        private void CreateExamen (Examen examen)
        {
            Random random = new Random();
            int amountTask =examen.AmountTask;
            CurrentExamen = new List<TextTask>();
            var list = new List<TextTask>();
            var usedIndexes = new int[amountTask+1];
            for(int i = 0; i<amountTask; i++)
            {
                int index;                
                do index = random.Next(1, amountTask+1);
                while (usedIndexes.Contains(index));               
                list.Add((TextTask)examen.Tasks.ElementAt(index - 1));
                usedIndexes[index] = index;                
            }
            foreach(var t in list)
            {
                CurrentExamen.Add((TextTask)t.Clone());
            }
        }

        public bool ConditionAnswer()
        {
            return CheckBox_1 || CheckBox_2 || CheckBox_3 || CheckBox_4;
        }

        private RelayCommand answerCommand;
        public RelayCommand AnswerCommand
        {
            get
            {
                return answerCommand ?? (answerCommand = new RelayCommand(obj =>
                {
                    if(CheckBox_1 == CurrentTask.Answers.ElementAt(0).Correct &&
                       CheckBox_2 == CurrentTask.Answers.ElementAt(1).Correct &&
                       CheckBox_3 == CurrentTask.Answers.ElementAt(2).Correct &&
                       CheckBox_4 == CurrentTask.Answers.ElementAt(3).Correct)
                    {
                        Result.CorrectAnswers++;
                    }
                    else
                    {
                        Result.UncorrectAnswers++;
                    }

                    if (countTask == CurrentExamen.Count)
                    {
                        EndExamen();
                        var handler = CloseHandler;
                        if (handler != null) handler.Invoke(this, EventArgs.Empty);
                    }
                    else
                    { 
                    CurrentTask = CurrentExamen.ElementAt(countTask);
                    countTask++;
                    NumberTask = countTask.ToString();
                    }
                }, (obj) => ConditionAnswer()));
            }
        }        
        
        private string subject;
        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }

        private string elapsedTime;
        public string ElapsedTime
        {
            get { return elapsedTime; }
            set
            {
                elapsedTime = value;
                OnPropertyChanged("ElapsedTime");
            }
        }

        private TextTask currentTask;
        public TextTask CurrentTask
        {
            get { return currentTask; }
            set
            {
                currentTask = value;
                NumberTask = countTask.ToString();
                Question = currentTask.Question;
                Answer_1 = currentTask.Answers.ElementAt(0).ValueAnswer;
                Answer_2 = currentTask.Answers.ElementAt(1).ValueAnswer;
                Answer_3 = currentTask.Answers.ElementAt(2).ValueAnswer;
                Answer_4 = currentTask.Answers.ElementAt(3).ValueAnswer;
                CheckBox_1 = false;
                CheckBox_2 = false;
                CheckBox_3 = false;
                CheckBox_4 = false;
                OnPropertyChanged("CurrentTask");
            }
        }

        private List<TextTask> currentExamen;
        public List<TextTask> CurrentExamen
        {
            get { return currentExamen; }
            set
            {
                currentExamen = value;
                OnPropertyChanged("CurrentExamen");
            }
        }

        private string numberTask;
        public string NumberTask
        {
            get { return numberTask; }
            set
            {
                numberTask = value;
                OnPropertyChanged("NumberTask");
            }
        }

        private string question;
        public string Question
        {
            get { return question; }
            set
            {
                question = value;
                OnPropertyChanged("Question");
            }
        }

        private string answer_1;
        public string Answer_1
        {
            get { return answer_1; }
            set
            {
                answer_1 = value;
                OnPropertyChanged("Answer_1");
            }
        }

        private string answer_2;
        public string Answer_2
        {
            get { return answer_2; }
            set
            {
                answer_2 = value;
                OnPropertyChanged("Answer_2");
            }
        }

        private string answer_3;
        public string Answer_3
        {
            get { return answer_3; }
            set
            {
                answer_3 = value;
                OnPropertyChanged("Answer_3");
            }
        }

        private string answer_4;
        public string Answer_4
        {
            get { return answer_4; }
            set
            {
                answer_4 = value;
                OnPropertyChanged("Answer_4");
            }
        }

        private bool chechBox_1;
        public bool CheckBox_1
        {
            get { return chechBox_1; }
            set
            {
                chechBox_1 = value;
                OnPropertyChanged("CheckBox_1");
            }
        }

        private bool chechBox_2;
        public bool CheckBox_2
        {
            get { return chechBox_2; }
            set
            {
                chechBox_2 = value;
                OnPropertyChanged("CheckBox_2");
            }
        }

        private bool chechBox_3;
        public bool CheckBox_3
        {
            get { return chechBox_3; }
            set
            {
                chechBox_3 = value;
                OnPropertyChanged("CheckBox_3");
            }
        }

        private bool chechBox_4;
        public bool CheckBox_4
        {
            get { return chechBox_4; }
            set
            {
                chechBox_4 = value;
                OnPropertyChanged("CheckBox_4");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
