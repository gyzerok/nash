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
            var sr = new StreamReader(@"C:\Users\Джордж\Documents\Visual Studio 2012\Projects\nash\poker_nash\bot\bin\Debug\preflop_table.txt");

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

            var positions = new List<int>();
            positions.Add(2);
            positions.Add(3);
            positions.Add(2);

            int minus = 9 - this.state.Board.Players.Count;
            int i = 0;
            while (minus > 0)
            {
                positions[0]--;
                minus--;

                if (positions[0] == 0) i++;
            }

            i = this.state.Board.Dealer;
            while (positions[i] != 0)
            {
                if (i == 0) return (Position)(positions.Count - 1);

                positions[positions.Count - 1]--;
                if (positions[positions.Count - 1] == 0) positions.RemoveAt(positions.Count - 1);
                i = this.state.Board.RightOf(i);
            }

            return Position.Early;
        }
    }
}
