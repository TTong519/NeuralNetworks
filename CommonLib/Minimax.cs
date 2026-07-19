using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class MinimaxTree
    {
        public static (int, T) Minimax<T>(T state, bool isMax) where T : IGameState<T>
        {
            if (state.IsTerminal)
            {
                if (state.IsWin) return (1, state);
                else if (state.IsLoss) return (-1, state);
                else if (state.IsTie) return (0, state);
            }
            var children = state.GetChildren(isMax);
            var bestValue = (isMax ? int.MinValue : int.MaxValue, state);

            foreach (var child in children)
            {
                var value = Minimax(child, !isMax);
                if(isMax)
                {
                    if(value.Item1 > bestValue.Item1)
                    {
                        bestValue = value;
                    }
                }
                else
                {
                    if (value.Item1 < bestValue.Item1)
                    {
                        bestValue = value;
                    }
                }
            }

            return bestValue;
        }
    }
}
