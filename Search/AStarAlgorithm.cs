using System;
using DIBRIS.Hippie;
using System.Collections.Generic;
using System.Diagnostics;
using SD.Tools.Algorithmia.Heaps;

namespace Search
{
	public class AStarAlgorithm<TState, TAction>
		where TAction : class
		where TState : class
	{
		public AStarAlgorithm ()
		{
		}

		public Node<TState, TAction> Search (IProblem<TState, TAction> problem)
		{
			var frontier = HeapFactory.NewRawFibonacciHeap<Node<TState, TAction>,double> ();
			var nodes = new Dictionary<TState, IHeapHandle<Node<TState, TAction>,double>> ();
			var explored = new HashSet<TState> ();

			{
				var node = new Node<TState, TAction> (problem.InitialState, null, null, 0, problem.Heuristic (problem.InitialState));
				var handle = frontier.Add (node, node.Cost);
				nodes.Add (node.State, handle);
			}

			while (true) {
				if (frontier.Count == 0) {
					Debug.WriteLine ("FAILURE");
					return null;
				}

				frontier.RemoveMin ();
				var node = frontier.Min.Value;

				Debug.WriteLine ("POP " + node);
				nodes.Remove (node.State);

				if (problem.IsGoalState (node.State)) {
					Debug.WriteLine ("GOAL " + node);
					return node;
				}

				// Debug.WriteLine ("EXPLORED " + node);
				explored.Add (node.State);

				var actions = problem.GetActions (node.State);

				foreach (var action in actions) {
					var newState = problem.Transition (node.State, action);
					var childNode =
						new Node<TState, TAction> (newState, node, action, node.PathCost + problem.StepCost (action), problem.Heuristic (newState));
					// Debug.WriteLine ("CHILD " + childNode);

					IHeapHandle<Node<TState, TAction>,double> existingHandle = null;
					nodes.TryGetValue (childNode.State, out existingHandle);
					var existingNode = existingHandle != null ? existingHandle.Value : null;

					if (!explored.Contains (childNode.State) && existingNode == null) {
						var childHandle = frontier.Add (childNode, childNode.Cost);
						nodes.Add (childNode.State, childHandle);
						Debug.WriteLine ("INSERT " + childNode);
					} else if (existingNode != null && existingNode.Cost > childNode.Cost) {
						frontier.UpdateValue (existingHandle, childNode);
						frontier.UpdatePriorityOf (existingHandle, childNode.Cost);
						Debug.WriteLine ("UPDATE " + childNode);
					}
				}
			}

		}
	}
}

