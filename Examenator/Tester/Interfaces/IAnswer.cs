using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Interfaces
{
    public interface IAnswer 
    {
        bool Correct { get; set; }
        bool Cheked { get; set; }
    }
}
