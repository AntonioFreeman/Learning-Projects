﻿using Examenator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.AbstractClasses
{
    public abstract class BaseExamen : IExam
    {
        private string subject;
        public string Subject { get { return subject; }

            set
            {
                subject = value;
                OnPropertyChanged("Subject");
            } }

        public ObservableCollection<BaseTask> Tasks { get; set; }

        //public override string ToString()
        //{
        //    if (Subject == null) return "";
        //    return 
        //        Subject.ToString();
        //}

        public virtual void Assign(BaseExamen examen)
        {
            Subject = examen.Subject;

            var tasks = new ObservableCollection<BaseTask>();
            foreach (var e in examen.Tasks)
            {
                if (e != null) tasks.Add(e.Clone());
            }
            Tasks = tasks;
        }

        public virtual BaseExamen Clone()
        {
            var cloned = (BaseExamen)Activator.CreateInstance(GetType());
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
