using Examenator.AbstractClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Classes
{
    [Serializable]
    public class Examen : BaseExamen
    {
        public int AmountTask { get; set; }
        public int TimeExamen { get; set; }

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
