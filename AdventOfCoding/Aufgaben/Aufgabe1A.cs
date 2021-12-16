using Lib;
using System;
using System.Diagnostics;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe1A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			int lastNumber = int.MaxValue;
			int increasedCount = 0;
			foreach(string currentLine in reader.ReadAndGetLines()) {
				int currentNumber = int.Parse(currentLine);
				if(currentNumber > lastNumber)
					increasedCount++;
				lastNumber = currentNumber;
			}

			this.result = increasedCount;
		}

	}
}