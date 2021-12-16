using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe3B : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			List<string> lines1 = reader.ReadAndGetLines().ToList();
			List<string> lines2 = reader.ReadAndGetLines().ToList();

			for(int j = 0; j < lines1[0].Length && lines1.Count > 1; j++) {
				int count = 0;
				for(int i = 0; i < lines1.Count; i++) {
					count += (lines1[i][j] == '1' ? 1 : -1);
				}
				char mostCommon = (count >= 0 ? '1' : '0');
				lines1 = lines1.Where<string>(x => x[j] == mostCommon).ToList();
			}
			int O2 = Convert.ToInt32(lines1[0], 2);

			for(int j = 0; j < lines2[0].Length && lines2.Count > 1; j++) {
				int count = 0;
				for(int i = 0; i < lines2.Count; i++) {
					count += (lines2[i][j] == '1' ? 1 : -1);
				}
				char leastCommon = (count < 0 ? '1' : '0');
				lines2 = lines2.Where<string>(x => x[j] == leastCommon).ToList();
			}
			int CO2 = Convert.ToInt32(lines2[0], 2);
			
			this.result = O2 * CO2;
		}

	}
}