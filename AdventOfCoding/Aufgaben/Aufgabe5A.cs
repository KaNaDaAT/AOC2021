using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe5A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			string[] lines = reader.ReadAndGetLines();
			List<int> coords = new List<int>();
			int maxVal = int.MinValue;
			int minVal = int.MaxValue;

			for(int i = 0; i < lines.Length; i++) {
				var lineVals = lines[i].Replace(" -> ", ",").Split(',').Select(Int32.Parse);
				minVal = (minVal < lineVals.Min() ? minVal : lineVals.Min());
				maxVal = (maxVal > lineVals.Max() ? maxVal : lineVals.Max());
				coords.AddRange(lineVals);
			}
			maxVal++;

			int[] coordsystem = new int[maxVal * maxVal];

			int count = 0;
			for(int i = 0; i < coords.Count; i += 4) {
				int x1 = coords.ElementAt(i), y1 = coords.ElementAt(i + 1);
				int x2 = coords.ElementAt(i + 2), y2 = coords.ElementAt(i + 3);
				if(x1 == x2) {
					for(int ycurrent = Math.Min(y1, y2); ycurrent <= Math.Max(y1, y2); ycurrent++) {
						coordsystem[x1 + ycurrent * maxVal] += 1;
						if(coordsystem[x1 + ycurrent * maxVal] == 2)
							count++;
					}
				} else if(y1 == y2) {
					for(int xcurrent = Math.Min(x1, x2); xcurrent <= Math.Max(x1, x2); xcurrent++) {
						coordsystem[y1 * maxVal + xcurrent] += 1;
						if(coordsystem[y1 * maxVal + xcurrent] == 2)
							count++;
					}
				}
			}
			this.result = count;

			/* Output Region */
			if(!IsDebug())
				return;

			for(int i = 0; i < coordsystem.Length; i++) {
				if(coordsystem[i] == 0) {
					toPrint.Append(".");
				} else {
					toPrint.Append(coordsystem[i].ToString());
				}
				if(i % maxVal == maxVal - 1)
					toPrint.Append(Environment.NewLine);
			}
		}

	}
}