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
    public class Examen : BaseExamen
    {
        public int AmountTask { get; set; }
        public int TimeExamen { get; set; }
       
        public Examen()
        {
            Subject = "Новый экзамен";
            Tasks = new ObservableCollection<BaseTask>();
            AmountTask = 0;
            TimeExamen = 0;
            Procent_3 = 55;
            Procent_4 = 70;
            Procent_5 = 85;
        }
        
        override public void Assign(BaseExamen examen)
        {
            base.Assign(examen);
            AmountTask = ((Examen)examen).AmountTask;
            TimeExamen = ((Examen)examen).TimeExamen;
        }

        override public BaseExamen Clone()
        {
            var cloned = (Examen)Activator.CreateInstance(GetType());
            cloned.Assign(this);
            return cloned;
        }
    }
}
