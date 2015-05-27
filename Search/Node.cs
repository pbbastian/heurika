using System;

namespace Search
{
	public class Node<TState, TAction>
	{
		public TState State { get; private set; }

		public Node<TState, TAction> Parent { get; private set; }

		public TAction Action { get; private set; }

		public double PathCost { get; private set; }

		public double HeuristicCost { get; private set; }

		public double Cost { get; private set; }

		public Node (TState state, Node<TState, TAction> parent, TAction action, double pathCost, double heuristicCost)
		{
			this.State = state;
			this.Parent = parent;
			this.Action = action;
			this.PathCost = pathCost;
			this.HeuristicCost = heuristicCost;
			this.Cost = PathCost + HeuristicCost;
		}

		public override string ToString ()
		{
			return string.Format ("[Node: State={0}, Action={1}, PathCost={2}, HeuristicCost={3}, Cost={4}]", State, Action, PathCost, HeuristicCost, Cost);
		}
		
	}
}

