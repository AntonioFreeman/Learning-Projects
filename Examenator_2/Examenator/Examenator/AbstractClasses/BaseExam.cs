﻿
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.AbstractClasses
{
    public abstract class BaseExam : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public ObservableCollection<BaseTask> Tasks { get; set; }

        private string subject;
        public string Subject
        { get { return subject; }
            set
            {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("PasswordText");
            }
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

        public virtual void Assign(BaseExam exam)
        {
            Id = exam.Id;
            Subject = exam.Subject;
            Password = exam.Password;
            Procent_3 = exam.Procent_3;
            Procent_4 = exam.Procent_4;
            Procent_5 = exam.Procent_5;
            var tasks = new ObservableCollection<BaseTask>();
            foreach (var e in exam.Tasks)
            {
                if (e != null) tasks.Add(e.Clone());
            }
            Tasks = tasks;
        }

        public virtual BaseExam Clone()
        {
            var cloned = (BaseExam)Activator.CreateInstance(GetType());
            cloned.Assign(this);
            return cloned;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
