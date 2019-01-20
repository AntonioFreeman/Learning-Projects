using Examenator.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.ViewModels
{
    public class ExamenViewModel: INotifyPropertyChanged
    {

        public ExamenViewModel(Examen examen, int amountTask, TimeSpan timeExamen)
        {
            Subject = examen.Subject;
            Result = new Result(); 
        }

        public Result Result { get; set; } 

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

        private bool? chechBox_1;
        public bool? CheckBox_1
        {
            get { return (bool)chechBox_1; }
            set
            {
                chechBox_1 = value;
                OnPropertyChanged("CheckBox_1");
            }
        }

        private bool? chechBox_2;
        public bool? CheckBox_2
        {
            get { return (bool)chechBox_2; }
            set
            {
                chechBox_2 = value;
                OnPropertyChanged("CheckBox_2");
            }
        }

        private bool? chechBox_3;
        public bool? CheckBox_3
        {
            get { return (bool)chechBox_3; }
            set
            {
                chechBox_3 = value;
                OnPropertyChanged("CheckBox_3");
            }
        }

        private bool? chechBox_4;
        public bool? CheckBox_4
        {
            get { return (bool)chechBox_4; }
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
