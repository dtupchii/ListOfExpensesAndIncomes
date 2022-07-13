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

        public void SaveData(BindingList<TransactionsModel> trasactionList)
        {
            using (StreamWriter writer = File.CreateText(listPath))
            {
                string output = JsonConvert.SerializeObject(trasactionList);
                writer.Write(output);
            }
        }

        public BindingList<TransactionsModel> ReadData()
        {
            if (File.Exists(listPath))
            {
                using (StreamReader reader = File.OpenText(listPath))
                {
                    string text = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<BindingList<TransactionsModel>>(text);
                }
            }
            else
                return new BindingList<TransactionsModel>();
        }

        public void SaveBalance (double balance)
        {
            using (StreamWriter writer = new StreamWriter(balancePath, false))
            {               
                writer.Write(balance);
                writer.Close();
            }
        }
        public double LoadBalance()
        {
            double balance = 0;
            if (File.Exists(balancePath))
            {
                using (StreamReader reader = new StreamReader(balancePath))
                {
                    balance = Convert.ToDouble(reader.ReadToEnd());
                    reader.Close();
                }
            }
            else
                balance = 0;

            return balance;
        }
    }
}
