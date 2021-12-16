using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe3A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			List<string> lines = reader.ReadAndGetLines().ToList();

			string gamma = "", epsilon = "";

			for(int i = 0; i < lines[0].Length; i++) {
				bool isOneMostCommon = lines.Count < lines.Count(x => x[i] == '1') * 2;
				gamma += Convert.ToInt32(isOneMostCommon);
				epsilon += Convert.ToInt32(!isOneMostCommon);
			}

			this.result = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
		}

	}
}