using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    class MonteCarloTree
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
			    T bestChild = default(T);
			    foreach (var child in current.Children)
			    {
				    double UCT = (child.Count / child.Value) + 1.5 * (System.Math.Sqrt(System.Math.Log(root.Value)) / child.Value);
				    if (UCT > bestUCTValue)
				    {
					    bestUCTValue = UCT;
					    bestChild = child;
				    }
 			    }
				current.Count++;
				current = bestChild;
				isMax = !isMax;
            }
		    return (current, isMax);
	    }
		private static void BackProp<T>(T state, bool isMax) where T : IMonteCarloGameState<T>
		{
			state.GenerateChildren(isMax);
			BackProp(state.Children[Random.Shared.Next(0, state.Children.Count)], !isMax);
            foreach (var child in state.Children)
			{
				if(child.Value == int.MaxValue && isMax)
				{
					state.Value = int.MaxValue;
                }
				else if(child.Value == int.MinValue && !isMax)
				{
					state.Value = int.MinValue;
				}
			}
        }
    }
}
