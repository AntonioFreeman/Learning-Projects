using Examenator.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Classes
{
    public class Exam : BaseExam
    {
        public int AmountTask { get; set; }
        public int TimeExam { get; set; }
       
        public Exam()
        {
            Subject = "Новый экзамен";
            Tasks = new ObservableCollection<BaseTask>();
            AmountTask = 0;
            TimeExam = 0;
            Procent_3 = 55;
            Procent_4 = 70;
            Procent_5 = 85;
        }
        
        override public void Assign(BaseExam exam)
        {
            base.Assign(exam);
            AmountTask = ((Exam)exam).AmountTask;
            TimeExam = ((Exam)exam).TimeExam;
        }

        override public BaseExam Clone()
        {
            var cloned = (Exam)Activator.CreateInstance(GetType());
            cloned.Assign(this);
            return cloned;
        }
    }
}
