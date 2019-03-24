using Examenator.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.ViewModels
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        private string textResult;
        private int amountTask;              
        private int procent_3;        
        private int procent_4;        
        private int procent_5;
       
        public ResultViewModel(Result result)
        {
            amountTask = result.AmountTask;
            FirstName = result.FirstName;
            SecondName = result.SecondName;
            TimeExecute = result.TimeExecute;
            CorrectAnswers = result.CorrectAnswers;
            UncorrectAnswers = result.UncorrectAnswers;
            NotAnswered = amountTask - CorrectAnswers - UncorrectAnswers;           
            procent_3 = result.Procent_3;
            procent_4 = result.Procent_4;
            procent_5 = result.Procent_5;
            CalculationEstimate();
        }

        private void CalculationEstimate()
        {
            double procent = 0;
            if ((correctAnswers > 0)||(uncorrectAnswers >0)) procent = 100 * correctAnswers / amountTask ;
            if (procent < procent_3) Estimate = 2;
            else if (procent < procent_4) Estimate = 3;
            else if (procent < procent_5) Estimate = 4;
            else Estimate = 5;
        }

        private void CreateTextResult()
        {
            StringBuilder sb = new StringBuilder();            
            sb.AppendLine("Имя участника: " + firstName); 
            sb.AppendLine("Фамилия участника: " + secondName); 
            sb.AppendLine("Время экзамена: " + timeExecute); 
            sb.AppendLine("Количество правильных ответов: " + correctAnswers); 
            sb.AppendLine("Количество неправильных ответов: " + uncorrectAnswers); 
            sb.AppendLine("Количество неотвеченных вопросов: " + notAnswered); 
            sb.AppendLine("Оценка: " + estimate); 
            textResult = sb.ToString();
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string secondName;
        public string SecondName
        {
            get { return secondName; }
            set
            {
                secondName = value;
                OnPropertyChanged("SecondName");
            }
        }

        private TimeSpan timeExecute;
        public TimeSpan TimeExecute
        {
            get { return timeExecute; }
            set
            {
                timeExecute = value;
                OnPropertyChanged("TimeExecute");
            }
        }

        private int correctAnswers;
        public int CorrectAnswers
        {
            get { return correctAnswers; }
            set
            {
                correctAnswers = value;
                CalculationEstimate();
                OnPropertyChanged("CorrectAnswers");
            }
        }

        private int uncorrectAnswers;
        public int UncorrectAnswers
        {
            get { return uncorrectAnswers; }
            set
            {
                uncorrectAnswers = value;
                CalculationEstimate();
                OnPropertyChanged("UncorrectAnswers");
            }
        }

        private int notAnswered;
        public int NotAnswered
        {
            get { return notAnswered; }
            set
            {
                notAnswered = value;
                OnPropertyChanged("NotAnswered");
            }
        }

        private int estimate;
        public int Estimate
        {
            get { return estimate; }
            set
            {
                estimate = value;
                OnPropertyChanged("Estimate");
            }
        }
        
        private RelayCommand saveResultCommand;
        public RelayCommand SaveResultCommand
        {
            get
            {
                return saveResultCommand ?? (saveResultCommand = new RelayCommand(obj =>
                {
                    CreateTextResult();
                    var loader = new Loader(textResult);
                    loader.SaveTextFile();               
                }));
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
