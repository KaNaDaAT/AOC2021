using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe17A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			string input = reader.ReadAll();
			input = input.Replace("target area: ", "").Replace("x=", "").Replace("y=", "");
			var inputs = input.Split(new string[] { ", ", ".." }, StringSplitOptions.RemoveEmptyEntries);
			int[] Bounds = new int[4]; // (xx xy) (yx yy)
			Bounds[0] = int.Parse(inputs[0]);
			Bounds[1] = int.Parse(inputs[1]);
			Bounds[2] = int.Parse(inputs[2]);
			Bounds[3] = int.Parse(inputs[3]);

			this.result = GetMaxY(ref Bounds);
		}

		private int GetMaxY(ref int[] Bounds) {
			return Bounds[2] * (Bounds[2] + 1) / 2;
		}
	}
}