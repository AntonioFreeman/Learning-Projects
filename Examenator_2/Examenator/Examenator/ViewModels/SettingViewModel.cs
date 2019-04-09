using Examenator.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Examenator.ViewModels
{
    public class SettingViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private Exam currentExam;
        private Exam baseExam;

        public EventHandler CloseHandler;

        public SettingViewModel(Exam exam)
        {
            baseExam = exam;
            currentExam = (Exam)exam.Clone();
            Procent_3 = currentExam.Procent_3;
            Procent_4 = currentExam.Procent_4;
            Procent_5 = currentExam.Procent_5;
        }

        private int procent_3;
        public int Procent_3
        {
            get { return procent_3; }
            set
            {
                procent_3 = value;
                OnPropertyChanged("Procent_3");
            }
        }

        private int procent_4;
        public int Procent_4
        {
            get { return procent_4; }
            set
            {
                procent_4 = value;
                OnPropertyChanged("Procent_4");
            }
        }

        private int procent_5;
        public int Procent_5
        {
            get { return procent_5; }
            set
            {
                procent_5 = value;
                OnPropertyChanged("Procent_5");
            }
        }

        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(obj =>
                {
                    {
                        baseExam.Procent_3 = Procent_3;
                        baseExam.Procent_4 = Procent_4;
                        baseExam.Procent_5 = Procent_5;
                        var handler = CloseHandler;
                        if (handler != null) handler.Invoke(this, EventArgs.Empty);
                    }
                }));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(obj =>
                {
                    {                       
                        var handler = CloseHandler;
                        if (handler != null) handler.Invoke(this, EventArgs.Empty);
                    }
                }));
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] {
        get
        {
            string error = String.Empty;
            switch (columnName)
            {
                case "Procent_3":
                    if ((Procent_3 <= 0) || (Procent_3 >= Procent_4))
                    {
                        error = "Процент должен быть больше 0 и меньше границы оценки 4";
                    }
                    break;
                case "Procent_4":
                        if ((Procent_4 <= Procent_3) || (Procent_4 >= Procent_5))
                        {
                            error = "Процент должен быть больше границы оценки 3 и меньше границы оценки 5";
                        }
                        break;
                case "Procent_5":
                        if ((Procent_5 <= Procent_4) || (Procent_5 >= 100))
                        {
                            error = "Процент должен быть больше границы оценки 3 и меньше границы оценки 5";
                        }
                        break;
            }
            return error;
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
