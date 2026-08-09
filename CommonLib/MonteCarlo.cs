using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    class MonteCarloTree
    {
        public static (int, T) MonteCarlo<T>(T state, bool isMax, int simulations = 1000) where T : IGameState<T>
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
				int wins = 0;
				for (int i = 0; i < simulations; i++)
				{
					var result = Simulate(child, !isMax);
					if ((isMax && result == 1) || (!isMax && result == -1))
					{
						wins++;
					}
				}
				var value = (wins, child);
				if (isMax)
				{
					if (value.Item1 > bestValue.Item1)
					{
						bestValue = value;
						bestValue.state = child;
					}
				}
				else
				{
					if (value.Item1 < bestValue.Item1)
					{
						bestValue = value;
						bestValue.state = child;
					}
				}
			}
			return bestValue;
		}
	}
}
