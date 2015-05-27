using System;
using System.Collections.Generic;
using System.IO;
using Search;
using System.Diagnostics;

namespace InferenceEngine
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var kb = new List<CNFClause> ();
			var file = new StreamReader ("breakfast.txt");
			string line;
			while ((line = file.ReadLine ()) != null) {
				var parts = line.Split (' ');
				var ifReached = false;
				var clause = new CNFClause ();
				foreach (var part in parts) {
					if (part.Equals ("if")) {
						ifReached = true;
					} else if (!ifReached) {
						if (part.StartsWith ("!")) {
							clause.Add (part.Substring (1), false);
						} else {
							clause.Add (part, true);
						}
					} else {
						if (part.StartsWith ("!")) {
							clause.Add (part.Substring (1), true);
						} else {
							clause.Add (part, false);
						}
					}
				}
				kb.Add (clause);
			}

			var hypothesis = new CNFClause ().Add ("breakfast", false);
			var problem = new InferenceProblem (kb, hypothesis);
			var algorithm = new AStarAlgorithm<CNFClause, CNFClause> ();

			// Measure time before warm-up.
			var sw = new Stopwatch ();
			sw.Start ();
			algorithm.Search (problem);
			sw.Stop ();
			var beforeJit = sw.Elapsed;

			// Perform warm-up.
			for (var i = 0; i < 50; i++) {
				algorithm.Search (problem);
			}

			// Measure time after warm-up.
			sw = new Stopwatch ();
			sw.Start ();
			var node = algorithm.Search (problem);
			sw.Stop ();
			var afterJit = sw.Elapsed;

			Console.WriteLine ("Before warm-up: {0} seconds\nAfter warm-up: {1} seconds", beforeJit, afterJit);

			while (node != null && node.Action != null) {
				Console.WriteLine (String.Format ("{0} , {1} | {2}", node.Parent.State, node.Action, !node.State.IsEmpty ? node.State.ToString () : " ⊥ "));
				node = node.Parent;
			}
		}
	}
}
