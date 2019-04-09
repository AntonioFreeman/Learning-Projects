using Examenator.Classes;
using Examenator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.ViewModels
{
    public class EnterNameViewModel: INotifyPropertyChanged
    {
        private Result currentResult;
        private Exam currentExam;
        public EventHandler CloseHandler;
        public ExamWindow ExamWindow;
                
        public EnterNameViewModel(Result currentResult, Exam currentExam)
        {
            this.currentResult = currentResult;
            this.currentExam = currentExam;
        }

        public string FirstName
        {
            get { return currentResult.FirstName; }
            set
            {
                currentResult.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string SecondName
        {
            get { return currentResult.SecondName; }
            set
            {
                currentResult.SecondName = value;
                OnPropertyChanged("SecondName");
            }
        }

        private RelayCommand okCommand;
        public RelayCommand OKCommand
        {
            get
            {
                return okCommand ?? (okCommand = new RelayCommand(obj =>
                {
                    ExamWindow = new ExamWindow(currentResult, currentExam);
                    ExamWindow.Show();
                    var handler = CloseHandler;
                    if (handler != null) handler.Invoke(this, EventArgs.Empty);
                }, obj => FirstName.Length>0 && SecondName.Length>0));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new RelayCommand(obj =>
                {
                    var handler = CloseHandler;
                    if (handler != null) handler.Invoke(this, EventArgs.Empty);
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
