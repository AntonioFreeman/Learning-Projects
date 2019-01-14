using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class Test : INotifyPropertyChanged
    {
        private string title;
        private int amountQuestions;
        private List<Question> questions;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
