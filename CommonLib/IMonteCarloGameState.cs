using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public interface IMonteCarloGameState<T> where T : IMonteCarloGameState<T>
    {
        public bool IsWin { get; set; }
        public bool IsLoss { get; set; }
        public bool IsTie { get; set; }
        public bool IsTerminal { get; set; }
        public int Value { get; set; }
        public int Count { get; set; }
        public List<T> Children { get; set; }
        public void GenerateChildren(bool isMax);
    }
}
