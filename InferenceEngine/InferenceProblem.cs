using System;
using Search;
using System.Collections.Generic;

namespace InferenceEngine
{
	public class InferenceProblem : IProblem<CNFClause, CNFClause>
	{
		private IEnumerable<CNFClause> kb;
		private CNFClause hypothesis;

		public InferenceProblem (IEnumerable<CNFClause> kb, CNFClause hypothesis)
		{
			this.kb = kb;
			this.hypothesis = hypothesis;
		}

		public bool IsGoalState (CNFClause state)
		{
			return state.IsEmpty;
		}

		public IEnumerable<CNFClause> GetActions (CNFClause state)
		{
			return kb;
		}

		public CNFClause Transition (CNFClause state, CNFClause action)
		{
			return state.Clone ().Resolution (action);
		}

		public double StepCost (CNFClause action)
		{
			return action.Count;
		}

		public double Heuristic (CNFClause state)
		{
			return state.Count;
		}

		public CNFClause InitialState {
			get {
				return hypothesis;
			}
		}
	}
}

