using Lib;
using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe8A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			string[] lines = reader.ReadAndGetLines();

			int count = 0;
			for(int i = 0; i < lines.Length; i++) {
				string[] rightSide = lines[i].Substring(lines[i].IndexOf('|') + 2).Split(' ');
				count += rightSide.Count(x => new int[] { 2, 3, 4, 7 }.Contains(x.Length));
			}
			this.result = count;
		}

	}
}