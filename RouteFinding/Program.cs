using System;
using System.Collections.Generic;
using System.IO;
using Search;
using System.Diagnostics;

namespace RouteFinding
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var streetMap = new Dictionary<Coordinate, ICollection<Street>> ();

			var file = new StreamReader ("streets.txt");
			string line;
			while ((line = file.ReadLine ()) != null) {
				var parts = line.Split (' ');
				if (parts.Length == 5) {
					var street = new Street (parts [2],
						             new Coordinate (int.Parse (parts [0]), int.Parse (parts [1])),
						             new Coordinate (int.Parse (parts [3]), int.Parse (parts [4])));
					ICollection<Street> streets;
					if (streetMap.TryGetValue (street.From, out streets)) {
						streets.Add (street);
					} else {
						streets = new List<Street> { street };
						streetMap.Add (street.From, streets);
					}
				}
			}

			var problem = new RouteFindingProblem (streetMap, new Coordinate (35, 80), new Coordinate (45, 70));
			var algorithm = new AStarAlgorithm<Coordinate, Street> ();

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
				Console.WriteLine (String.Format ("{0} {1} {2} {3} {4}",
					node.Action.From.X, node.Action.From.Y, node.Action.Name, node.Action.To.X, node.Action.To.Y));
				node = node.Parent;
			}
		}
	}
}
