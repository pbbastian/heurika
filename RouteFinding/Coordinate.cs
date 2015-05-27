using System;
using System.Diagnostics;

namespace RouteFinding
{
	public class Coordinate
	{
		public int X { get; private set; }

		public int Y { get; private set; }

		public Coordinate (int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public double Distance (Coordinate other)
		{
			return Math.Sqrt (Math.Pow (this.X - other.X, 2) + Math.Pow (this.Y - other.Y, 2));
		}

		public override bool Equals (Object obj)
		{
			if (obj == null || !(obj is Coordinate)) {
				return false;
			}

			var other = (Coordinate)obj;

			return this.X == other.X && this.Y == other.Y;
		}

		public override int GetHashCode ()
		{
			return X.GetHashCode () * 17 + Y.GetHashCode () * 31;
		}

		public override string ToString ()
		{
			return string.Format ("[Coordinate: X={0}, Y={1}]", X, Y);
		}
	}
}

