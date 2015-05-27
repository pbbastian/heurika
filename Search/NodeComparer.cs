using System;
using System.Collections.Generic;

namespace Search
{
	public class NodeComparer<TState, TAction> : IComparer<Node<TState, TAction>>
	{
		public NodeComparer ()
		{
		}

		public int Compare (Node<TState, TAction> x, Node<TState, TAction> y)
		{
			if (x.Cost > y.Cost) {
				return 1;
			} else if (x.Cost < y.Cost) {
				return -1;
			} else {
				return 0;
			}
		}
	}
}

