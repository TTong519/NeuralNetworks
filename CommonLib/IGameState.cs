using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public interface IGameState<T> where T : IGameState<T>
    {
        bool IsWin { get; set; }
        bool IsLoss { get; set; }
        bool IsTie { get; set; }
        bool IsTerminal { get; set; }
        T[] GetChildren(bool isMax);
    }
}
