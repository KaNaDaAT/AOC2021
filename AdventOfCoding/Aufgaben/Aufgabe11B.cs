using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe11B : AufgabeAbstract {

		private int rowLength = 0;

		protected override void Runner(Reader reader) {
			List<int> map = reader.ReadAll().Replace(Environment.NewLine, "").ToCharArray().Select(x => x - '0').ToList();
			rowLength = reader.GetContent().IndexOf(Environment.NewLine);
			int steps = int.MaxValue;

			int i;
			for(i = 0; i < steps && map.Any(x => x != 0); i++) {
				map = map.Select(x => ++x).ToList();
				for(int index = 0; index < map.Count; index++) {
					if(map[index] > 9) {
						map[index] = 0;
						IncreaseAdjcent(ref map, index);
					}
				}
			}
			this.result = i;
		}

		public void IncreaseAdjcent(ref List<int> map, int index) {
			int curx = index % rowLength;
			int cury = index / rowLength;
			for(int row = cury - 1; row <= cury + 1; row++) {
				for(int column = curx - 1; column <= curx + 1; column++) {
					if((row >= 0 && row < map.Count / rowLength) && (column >= 0 && column < rowLength)) {
						int i = column + row * rowLength;
						if(map[i] != 0) {
							map[i]++;
						}
						if(map[i] > 9) {
							map[i] = 0;
							IncreaseAdjcent(ref map, i);
						}
					}
				}
			}
		}
	}
}