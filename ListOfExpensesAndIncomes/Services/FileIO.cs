using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace ListOfExpensesAndIncomes
{
    internal class FileIO
    {
        private string listPath, balancePath;
        public FileIO(string listPATH, string balancePATH)
        {
            listPath = listPATH;
            balancePath = balancePATH;
        }

        public void SaveData(BindingList<Transaction> trasactionList)
        {
            using (StreamWriter writer = File.CreateText(listPath))
            {
                string output = JsonConvert.SerializeObject(trasactionList);
                writer.Write(output);
            }
        }

        public BindingList<Transaction> ReadData()
        {
            if (File.Exists(listPath))
            {
                using (StreamReader reader = File.OpenText(listPath))
                {
                    string text = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<BindingList<Transaction>>(text);
                }
            }
            else
                return new BindingList<Transaction>();
        }
    }
}
