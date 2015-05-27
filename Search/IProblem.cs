using System;
using System.Collections.Generic;

namespace Search
{
	public interface IProblem<TState, TAction>
	{
		TState InitialState { get; }

		bool IsGoalState (TState state);

		IEnumerable<TAction> GetActions (TState state);

		TState Transition (TState state, TAction action);

		double StepCost (TAction action);

		double Heuristic (TState state);
	}
}

