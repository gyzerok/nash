using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using common;

namespace bot
{
    public class BSSBot : IBot
    {
        private Dictionary<List<string>, List<int>> preflopTable;

        public BSSBot()
        {
            var sr = new StreamReader("preflop_table");

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var actions = new List<int>();
                string tmpLine;
                for (int i = 0; i < 3; i++)
                {
                    tmpLine = sr.ReadLine();
                    actions = tmpLine.Split(' ').Select(n => int.Parse(n)).ToList();
                    this.preflopTable.Add(line.Split(' ').ToList(), actions);
                }
            }

            sr.Close();
        }

        public Activity Process(State state)
        {

            return null;
        }
    }
}
