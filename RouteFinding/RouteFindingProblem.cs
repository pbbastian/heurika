using System;
using System.Collections.Generic;
using Search;

namespace RouteFinding
{
	public class RouteFindingProblem : IProblem<Coordinate, Street>
	{
		private IDictionary<Coordinate, ICollection<Street>> streetMap;
		private Coordinate goal;

		public RouteFindingProblem (IDictionary<Coordinate, ICollection<Street>> streetMap, Coordinate initial, Coordinate goal)
		{
			this.streetMap = streetMap;
			this.goal = goal;
			InitialState = initial;
		}

		public Coordinate InitialState { get; private set; }

		public bool IsGoalState (Coordinate state)
		{
			return goal.Equals (state);
		}

		public IEnumerable<Street> GetActions (Coordinate state)
		{
			ICollection<Street> streets;
			if (streetMap.TryGetValue (state, out streets)) {
				return streets;
			} else {
				return new List<Street> ();
			}
		}

		public Coordinate Transition (Coordinate state, Street action)
		{
			return action.To;
		}

		public double StepCost (Street action)
		{
			return action.Distance ();
		}

		public double Heuristic (Coordinate state)
		{
			return state.Distance (goal);
		}
	}
}

