using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Search
{
	public class NodeStateEqualityComparer<TState, TAction> : IEqualityComparer<Node<TState, TAction>>
	{
		private IEqualityComparer<TState> equalityComparer;

		public NodeStateEqualityComparer (IEqualityComparer<TState> equalityComparer)
		{
			this.equalityComparer = equalityComparer;
		}

		public bool Equals (Node<TState, TAction> x, Node<TState, TAction> y)
		{
			return this.equalityComparer.Equals (x.State, y.State);
		}

		public int GetHashCode (Node<TState, TAction> obj)
		{
			return this.equalityComparer.GetHashCode (obj.State);
		}
	}
}

