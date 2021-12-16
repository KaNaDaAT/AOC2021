using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe7B : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			string line = reader.ReadAll();
			IEnumerable<int> pos = line.Split(',').Select(int.Parse);
			int averageC = (int) Math.Ceiling(pos.Average());
			int averageF = (int) Math.Floor(pos.Average());
			IEnumerable<int> posC = pos.Select(x => Math.Abs(x - averageC));
			IEnumerable<int> posF = pos.Select(x => Math.Abs(x - averageF));
			this.result = Math.Min(posC.Select(x => x * (x + 1) / 2).Sum(), posF.Select(x => x * (x + 1) / 2).Sum());

			/* Output Region */
			if(!IsDebug())
				return;
		}

	}
}