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
		    
	    }

	    private static T Select<T>(T root) where T : IMonteCarloGameState<T>
	    {
		    T current = root;
		    while (current.Children.Count > 0)
		    {
			    double bestUCTValue = double.MinValue;
			    T  bestChild = null;
			    foreach (var child in current.Children)
			    {
				    double UCT = (child.Count / child.Value) + 1.5 * (System.Math.Sqrt(System.Math.Log(root.Value)) / child.Value);
				    if (UCT > bestUCTValue)
				    {
					    bestUCTValue = UCT;
					    bestChild = child;
				    }
 			    }
			    if(bestChild != null)
			    {
				    break;
			    }
			    current = bestChild;
		    }
		    return current;
	    }
    }
}
