using Lib;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe6A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			string line = reader.ReadAll();

			int[] fishTimer = new int[9];

			for(int i = 0; i < line.Length; i += 2) {
				fishTimer[(int) char.GetNumericValue(line[i])] += 1;
			}

			for(int d = 1; d <= 80; d++) {
				int fishCountAtTimeZero = fishTimer[0];
				for(int i = 1; i < fishTimer.Length; i++) {
					fishTimer[i - 1] = fishTimer[i];
				}
				fishTimer[6] += fishCountAtTimeZero;
				fishTimer[8] = fishCountAtTimeZero;
			}
			this.result = fishTimer.Sum();

			if(this.IsDebug()) {
				Print(line);
			}
		}

		private void Print(string line) {
			int[] fishTimer = new int[9];

			toPrint.AppendLine("Initial state: " + line);
			for(int i = 0; i < line.Length; i += 2) {
				fishTimer[(int) char.GetNumericValue(line[i])]++;
			}

			for(int d = 1; d <= 80; d++) {
				int fishCountAtTimeZero = fishTimer[0];
				for(int i = 1; i < fishTimer.Length; i++) {
					fishTimer[i - 1] = fishTimer[i];
				}
				fishTimer[6] += fishCountAtTimeZero;
				fishTimer[8] = fishCountAtTimeZero;

				toPrint.Append(string.Format("After {0,3:0} days: ", d));
				for(int i = 0; i < fishTimer.Length; i++) {
					toPrint.AppendFormat("{0,6:0} x {1,-2:0}, ", fishTimer[i], i);
				}
				toPrint.Length -= 2;
				toPrint.AppendLine("");
			}
		}
	}
}