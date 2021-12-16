using Lib;
using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe1B : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			int[] lines = reader.ReadAndGetLines().Select(int.Parse).ToArray();
			int lastSum = int.MaxValue;
			int increasedCount = 0;
			for(int i = 0; i < lines.Length - 2; i++) {
				int currentSum = lines[i] + lines[i + 1] + lines[i + 2];
				if(currentSum > lastSum)
					increasedCount++;
				lastSum = currentSum;
			}
			this.result = increasedCount;
		}

	}
}