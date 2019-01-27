using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Examenator.Classes
{
    public class Loader
    {
        private string textForSave;
        private BinaryFormatter binaryFormatter;

        public Loader()
        {
            binaryFormatter = new BinaryFormatter();
        }

        public Loader(String text)
        {
            textForSave = text;
        }

        public void SaveSerialisation(ObservableCollection<Examen> examens)
        {
            using (FileStream fs = new FileStream("examens.dat", FileMode.Create))
            {
                binaryFormatter.Serialize(fs, examens);
            }
        }

        public ObservableCollection<Examen> LoadDeserialisation()
        {
            ObservableCollection<Examen> newExamens;
            using (FileStream fs = new FileStream("examens.dat", FileMode.OpenOrCreate))
            {
                 newExamens = (ObservableCollection<Examen>)binaryFormatter.Deserialize(fs);
            }
            return newExamens;
        }

        public void SaveTextFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; 
            dlg.DefaultExt = ".text"; 
            dlg.Filter = "Text documents (.txt)|*.txt"; 

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename, false))
                {
                    file.WriteLine(textForSave);
                }
            }
        }
    }
}
