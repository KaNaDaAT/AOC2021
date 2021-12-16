using Lib;
using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe11A : AufgabeAbstract {

		private int flashes = 0;
		private int rowLength = 0;

		protected override void Runner(Reader reader) {
			int[] map = reader.ReadAll().Replace(Environment.NewLine, "").ToCharArray().Select(x => x - '0').ToArray();
			rowLength = reader.GetContent().IndexOf(Environment.NewLine);
			int steps = 100;

			for(int i = 0; i < steps; i++) {
				for(int index = 0; index < map.Length; index++) {
					map[index] += 1;
				}
				for(int index = 0; index < map.Length; index++) {
					if(map[index] > 9) {
						Flash(ref map, index);
					}
				}
			}
			this.result = flashes;
		}

		public void Flash(ref int[] map, int index) {
			if(map[index] != 0) {
				map[index]++;
			}
			if(map[index] > 9) {
				flashes++;
				map[index] = 0;
				IncreaseAdjcent(ref map, index);
			}
		}

		public void IncreaseAdjcent(ref int[] map, int index) {
			int curx = index % rowLength;
			int cury = index / rowLength;
			for(int row = cury - 1; row <= cury + 1; row++) {
				for(int column = curx - 1; column <= curx + 1; column++) {
					if((row >= 0 && row < map.Length / rowLength) && (column >= 0 && column < rowLength)) {
						Flash(ref map, column + row * rowLength);
					}
				}
			}
		}
	}
}