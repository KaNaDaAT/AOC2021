using Lib;
using System;
using System.Diagnostics;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe17B : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			string input = reader.ReadAll();
			input = input.Replace("target area: ", "").Replace("x=", "").Replace("y=", "");
			var inputs = input.Split(new string[] { ", ", ".." }, StringSplitOptions.RemoveEmptyEntries);
			int[] Bounds = new int[4]; // (xx xy) (yx yy)
			Bounds[0] = int.Parse(inputs[0]);
			Bounds[1] = int.Parse(inputs[1]);
			Bounds[2] = int.Parse(inputs[2]);
			Bounds[3] = int.Parse(inputs[3]);

			this.result = GetCount(ref Bounds);
		}

		private int GetCount(ref int[] Bounds) {
			int maxY = Bounds[2] * (Bounds[2] + 1) / 2;
			int count = 0;
			for(int x = 0; x < Bounds[1] * 2; x++) {
				for(int y = Bounds[2]; y < Math.Sqrt(maxY - Bounds[2]); y++) {
					int velocityX = x;
					int velocityY = y;
					int curX = velocityX, curY = velocityY;
					while(curX <= Bounds[1] && curY >= Bounds[2]) {
						if(curX >= Bounds[0] && curX <= Bounds[1] && curY >= Bounds[2] && curY <= Bounds[3]) {
							count++;
							break;
						}
						velocityX = velocityX <= 0 ? 0 : velocityX - 1;
						velocityY--;
						curX += velocityX;
						curY += velocityY;
					}
				}
			}
			return count;
		}

	}
}