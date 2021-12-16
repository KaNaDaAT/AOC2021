using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe8B : AufgabeAbstract {

		Stopwatch sw = new Stopwatch();

		protected override void Runner(Reader reader) {
			string[] lines = reader.ReadAndGetLines();
			string[] patterns = new string[10];

			long count = 0;
			for(int i = 0; i < lines.Length; i++) {
				string[] leftSide = lines[i].Split(" | ")[0].Split(' ');
				for(int j = 0; j < leftSide.Length; j++) {
					leftSide[j] = new string(leftSide[j].OrderBy(x => x).ToArray());
				}

				patterns[1] = leftSide.Single(x => x.Length == 2);
				patterns[4] = leftSide.Single(x => x.Length == 4);
				patterns[7] = leftSide.Single(x => x.Length == 3);
				patterns[8] = leftSide.Single(x => x.Length == 7);

				patterns[9] = leftSide.Single(x => x.Length == 6 && x.Except(patterns[7]).Except(patterns[4]).Count() == 1);
				patterns[6] = leftSide.Single(x => x.Length == 6 && patterns[1].Except(x).Count() == 1 && x != patterns[9]);
				patterns[0] = leftSide.Single(x => x.Length == 6 && x != patterns[6] && x != patterns[9]);

				char leftDown = patterns[8].Except(patterns[9]).Single();
				char rightUp = patterns[8].Except(patterns[6]).Single();

				patterns[5] = leftSide.Single(x => x.Length == 5 && !x.Contains(leftDown) && !x.Contains(rightUp));
				patterns[2] = leftSide.Single(x => x.Length == 5 && x.Contains(leftDown) && x.Contains(rightUp));
				patterns[3] = leftSide.Single(x => x.Length == 5 && x != patterns[5] && x != patterns[2]);


				string[] rightSide = lines[i].Split(" | ")[1].Split(' ');
				for(int j = 0; j < rightSide.Length; j++) {
					count += (int) Math.Pow(10, rightSide.Length - j - 1)
						* Array.IndexOf(patterns, new string(rightSide[j].OrderBy(x => x).ToArray()));
				}
			}
			this.result = count;
		}

		/*protected override void Runner(Reader reader) {
			string[] lines = reader.ReadAndGetLines();
			char[] charCount = new char[7];
			for(int ci = 0; ci < charCount.Length; ci++) {
				lines[0] = lines[0].Replace((char) ('a' + ci), lines[0].Split(" | ")[0].Count(x => x == 'a' + ci).ToString()[0]);
			}
			string[] leftSide = lines[0].Split(" | ")[0].Split(' ').Select(x => new string(x.OrderBy(c => c).ToArray())).ToArray();
			int[] ls = leftSide.Select(int.Parse).ToArray();

			int count = 0;
			for(int i = 0; i < lines.Length; i++) {
				string[] rightSide = lines[0].Split(" | ")[1].Split(' ').Select(x => new string(x.OrderBy(c => c).ToArray())).ToArray();
				int[] rs = rightSide.Select(int.Parse).OrderBy(x => x).ToArray();
				for(int j = 0; j < rs.Length; j++) {
					count += (int) Math.Pow(10, rs.Length - j - 1) * Array.IndexOf(ls, rs[j]);
				}
			}

			this.result = count;
		}*/
	}
}