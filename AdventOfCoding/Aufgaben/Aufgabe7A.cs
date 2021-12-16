using Lib;
using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe7A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			string line = reader.ReadAll();
			int[] positions = line.Split(',').Select(int.Parse).OrderBy(x => x).ToArray();
			int median = positions[(positions.Length - 1) / 2];
			positions = positions.Select(x => Math.Abs(x - median)).ToArray();
			this.result = positions.Sum();

			/* Output Region */
			if(!IsDebug())
				return;
		}

	}
}