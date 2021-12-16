using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe9A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			int[] map = reader.ReadAll().Replace(Environment.NewLine, "").ToCharArray().Select(x => x - '0').ToArray();
			int rowLength = reader.GetContent().IndexOf(Environment.NewLine);

			int count = 0;
			for(int i = 0; i < map.Length; i++) {
				int cur = map[i];
				if(i % rowLength != 0 && cur >= map[i - 1])
					continue;
				if(i % rowLength != rowLength - 1 && cur >= map[i + 1])
					continue;
				if(i >= rowLength && cur >= map[i - rowLength])
					continue;
				if(i < map.Length - rowLength && cur >= map[i + rowLength])
					continue;
				count += 1 + cur;
			}
			this.result = count;
		}

	}
}