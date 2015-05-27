using System;
using RouteFinding;

namespace RouteFinding
{
	public class Street
	{
		public Street (string name, Coordinate from, Coordinate to)
		{
			this.Name = name;
			this.From = from;
			this.To = to;
		}

		public string Name { get; set; }

		public Coordinate From { get; set; }

		public Coordinate To { get; set; }

		public double Distance ()
		{
			return From.Distance (To);
		}
	}
}

