using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public static class MinimaxTree
    {
        public static (int, T) Minimax<T>(T state, bool isMax, int alpha = int.MinValue, int beta = int.MaxValue) where T : IGameState<T>
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
                var value = Minimax(child, !isMax, alpha, beta);
                if(isMax)
                {
                    if(value.Item1 > bestValue.Item1)
                    {
                        bestValue = value;
                        bestValue.state = child;
                    }
                    if(value.Item1 >= alpha)
                    {
                        alpha = value.Item1;
                    }
                }
                else
                {
                    if (value.Item1 < bestValue.Item1)
                    {
                        bestValue = value;
                        bestValue.state = child;
                    }
                    if(value.Item1 <= beta)
                    {
                        beta = value.Item1;
                    }
                }
                if(alpha >= beta)
                {
                    break;
                }
            }

            return bestValue;
        }
    }
}
