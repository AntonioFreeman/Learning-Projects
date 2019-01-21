﻿using Examenator.Classes;
using Examenator.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.AbstractClasses
{
    public abstract class BaseTask : ITask
    {
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
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

        private ObservableCollection<TextAnswer> answers;
        public ObservableCollection<TextAnswer> Answers
        {
            get { return answers; }
            set
            {
                answers = value;
                OnPropertyChanged("Answers");
            }
        }

        public override string ToString()
        {
            return Title;
        }

        public virtual void Assign(BaseTask task)
        {
            Title = task.Title;
            Question = task.Question;
            var answers = new ObservableCollection<TextAnswer>();
            foreach(var a in task.Answers)
            {
               if(a != null) answers.Add((TextAnswer)a.Clone());
            }
            Answers = answers;
        }

        public virtual BaseTask Clone()
        {
            var cloned = (BaseTask)Activator.CreateInstance(GetType());
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
