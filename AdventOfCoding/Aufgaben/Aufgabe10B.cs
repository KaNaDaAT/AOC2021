using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe10B : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			char[] openC = new char[] { '(', '[', '{', '<' };
			char[] closeC = new char[] { ')', ']', '}', '>' };
			StringBuilder input = new StringBuilder(reader.ReadAll());

			long lastLength = 0;
			while(input.Length != lastLength) {
				lastLength = input.Length;
				input = input.Replace("[]", "").Replace("{}", "").Replace("<>", "").Replace("()", "");
			}

			List<string> lines = input.ToString().Split(Environment.NewLine).ToList();
			for(int lineIndex = 0; lineIndex < lines.Count; lineIndex++) {
				if(lines[lineIndex].IndexOfAny(closeC) != -1) {
					lines.Remove(lines[lineIndex]);
					lineIndex--;
				}
			}

			long[] scores = new long[lines.Count];
			for(int lineIndex = 0; lineIndex < lines.Count; lineIndex++) {
				for(int i = lines[lineIndex].Length - 1; i >= 0; i--) {
					int index = Array.IndexOf(openC, lines[lineIndex][i]);
					if(index >= 0) {
						scores[lineIndex] = scores[lineIndex] * 5 + index + 1;
					}
				}
			}
			scores = scores.OrderBy(x => x).ToArray();
			this.result = (scores[(scores.Length - 1) / 2] + scores[scores.Length / 2]) / 2;
		}

	}
}