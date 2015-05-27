using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace InferenceEngine
{
	public class CNFClause
	{
		private Dictionary<string,bool> literals;
		private int hashCode;

		public CNFClause ()
		{
			this.literals = new Dictionary<string,bool> ();
			this.hashCode = 0;
		}

		private CNFClause (Dictionary<string,bool> literals, int hashCode)
		{
			this.literals = literals;
			this.hashCode = hashCode;
		}

		public bool IsEmpty {
			get { return literals.Count == 0; }
		}

		public int Count {
			get { return literals.Count; }
		}

		public CNFClause Add (string symbol, bool isDenial)
		{
			bool currentIsDenial;
			if (!literals.TryGetValue (symbol, out currentIsDenial)) {
				literals.Add (symbol, isDenial);
				hashCode += (symbol.GetHashCode () * 17 + isDenial.GetHashCode () * 23);
			} else if (isDenial != currentIsDenial) {
				literals.Remove (symbol);
				hashCode -= (symbol.GetHashCode () * 17 + currentIsDenial.GetHashCode () * 23);
			}

			return this;
		}

		public CNFClause Resolution (CNFClause other)
		{
			foreach (var kvp in other.literals) {
				Add (kvp.Key, kvp.Value);
			}

			return this;
		}

		public CNFClause Clone ()
		{
			return new CNFClause (new Dictionary<string, bool> (literals), hashCode);
		}

		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (!(obj is CNFClause))
				return false;
			CNFClause other = (CNFClause)obj;

			foreach (var kvp in literals) {
				var key = kvp.Key;
				var value = kvp.Value;
				bool otherValue;

				if (!other.literals.TryGetValue (key, out otherValue) || value != otherValue) {
					return false;
				}
			}

			foreach (var kvp in other.literals) {
				var key = kvp.Key;
				var otherValue = kvp.Value;
				bool value;

				if (!literals.TryGetValue (key, out value) || otherValue != value) {
					return false;
				}
			}

			return true;
		}

		public override int GetHashCode ()
		{
			return hashCode;
		}

		public override string ToString ()
		{
			var sb = new StringBuilder (" ");

			foreach (var kvp in literals) {
				if (!kvp.Value) {
					sb.Append ("¬");
				}

				sb.Append (kvp.Key);
				sb.Append (" ");
			}

			return sb.ToString ();
		}
		
	}
}

