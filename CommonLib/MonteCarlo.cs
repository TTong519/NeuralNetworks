using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class MonteCarloTree
    {
	    public static (int, T) MonteCarlo<T>(T state, bool isMax, int iterations = 1600) where T : IMonteCarloGameState<T>
	    {
			for (int i = 0; i < iterations; i++)
			{
				(T, bool) toSim = Select(state, isMax);
				BackProp(toSim.Item1, toSim.Item2);
            }
			var sorted = state.Children.OrderByDescending(c => c.Value).ToList();
			return (sorted[0].Value, sorted[0]);
        }
	    private static (T, bool) Select<T>(T root, bool isMax) where T : IMonteCarloGameState<T>
	    {
		    T current = root;
		    while (current.Children.Count > 0)
		    {
			    double bestUCTValue = double.MinValue;
			    T bestChild = default;
			    foreach (var child in current.Children)
			    {
					child.GenerateChildren(!isMax);
				    double UCT = (child.Value / (double)child.Count) + 1.5 * (System.Math.Sqrt(System.Math.Log(current.Count) / child.Count));
				    if (UCT > bestUCTValue)
				    {
					    bestUCTValue = UCT;
					    bestChild = child;
				    }
 			    }
				if(bestChild == null)
                {
                    break;
                }
                current.Count++;
				current = bestChild;
				isMax = !isMax;
            }
		    return (current, isMax);
	    }
		private static void BackProp<T>(T? state, bool isMax) where T : IMonteCarloGameState<T>
		{
			state.GenerateChildren(isMax);
			if(state.IsTerminal)
			{
				return;
			}
			double bestUCTValue = isMax ? double.MinValue : double.MaxValue;
			T bestChild = default;
			foreach (var child in state.Children)
			{
				child.GenerateChildren(!isMax);
                double UCT = (child.Value / (double)child.Count) + 1.5 * (System.Math.Sqrt(System.Math.Log(state.Count) / child.Count));
                if (isMax && UCT > bestUCTValue)
                {
                    bestUCTValue = UCT;
                    bestChild = child;
                }
                else if (!isMax && UCT < bestUCTValue)
                {
                    bestUCTValue = UCT;
                    bestChild = child;
                }
            }
			if(bestChild != null)
            {
                BackProp(bestChild, !isMax);
            }
            int temp = state.Children[0].Value;
            foreach (var child in state.Children)
			{
				if(isMax)
				{
					if(child.Value > temp)
					{
						temp = child.Value;
					}
				}
				else
				{
                    if (child.Value < temp)
                    {
                        temp = child.Value;
                    }
                }
			}
			state.Value = temp;
        }
    }
}
