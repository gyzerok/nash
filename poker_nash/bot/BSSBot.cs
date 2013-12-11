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
        private Dictionary<List<string>, List<List<int>>> preflopTable;
        private State state;

        public BSSBot()
        {
            var sr = new StreamReader("preflop_table");

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                var actions = new List<int>();
                var subTable = new List<List<int>>();
                string tmpLine;
                for (int i = 0; i < 3; i++)
                {
                    tmpLine = sr.ReadLine();
                    actions = tmpLine.Split(' ').Select(n => int.Parse(n)).ToList();
                    subTable.Add(actions);
                    this.preflopTable.Add(line.Split(' ').ToList(), subTable);
                }
            }

            sr.Close();
        }

        public Activity Process(State state)
        {
            this.state = state;

            if (this.state.Board.Street == Street.Preflop)
            {
                var subTable = this.GetSubtable();

                var decision = this.GetDecision();

                int i = 0;
                switch (decision)
                {
                    case Decision.Fold:
                        i = 0;
                        break;
                    case Decision.Call:
                        i = 1;
                        break;
                    case Decision.Raise:
                        i = 2;
                        break;
                }

                var strTable = subTable[i];

                decision = (Decision) strTable[(int) this.GetPosition() - 1];

                return new Activity(decision);
            }

            return null;
        }

        private List<List<int>> GetSubtable()
        {
            string hand = this.state.Hand.ToString();

            return this.preflopTable.Select(n => (n.Key.Contains(hand)) ? n.Value : null).ElementAt(0);
        }

        private Decision GetDecision()
        {
            var decision = Decision.Fold;

            for (int i = this.state.Board.Players.Count; i > 1; i++)
            {
                if (this.state.Board.Players[i].Activity.Bet > 0.02)
                {
                    decision = Decision.Raise;
                    break;
                }

                if (this.state.Board.Players[i].Activity.Bet == 0.02)
                {
                    decision = Decision.Call;
                    break;
                }
            }

            return decision;
        }

        private Position GetPosition()
        {
            if (this.state.Board.Dealer == this.state.Board.Players.Count - 1 ||
                this.state.Board.Dealer == this.state.Board.Players.Count - 2)
                return Position.Blind;


        }
    }
}
