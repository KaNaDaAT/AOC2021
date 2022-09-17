using Lib;
using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe21A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			int[] sudokuvalues = reader.ReadAll().Split(new string[] { Environment.NewLine, " " }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();

			Grid<int> sudokuField = new Grid<int>(sudokuvalues);
			int[] spots = new int[] { 0, 9, 14, 29, 50, 52, 62 };
			toPrint.AppendLine("11, 1, 12, 9, 1, 14");
			int count = 1;
			foreach(int i in spots) {
				toPrint.Append((sudokuField.GetAdjcent(i, true).Sum() + 6) % 20);
				if(count != spots.Length) {
					toPrint.Append(", ");
				}
				count++;
			}
			this.result = 0;
		}

	}
}