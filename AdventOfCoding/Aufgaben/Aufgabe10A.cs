using Lib;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe10A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			StringBuilder input = new StringBuilder(reader.ReadAll());
			int count = 0;

			long lastLength = 0;
			while(input.Length != lastLength) {
				lastLength = input.Length;
				input = input.Replace("[]", "").Replace("{}", "").Replace("<>", "").Replace("()", "");
			}
			char[] bad = new char[] { ')', ']', '}', '>' };
			int[] points = new int[] { 3, 57, 1197, 25137 };
			int index;
			foreach(string line in input.ToString().Split(Environment.NewLine)) {
				for(int i = 0; i < line.Length; i++) {
					index = Array.IndexOf(bad, line[i]);
					if(index >= 0) {
						count += points[index];
						break;
					}
				}
			}
			this.result = count;


			/* Output Region */
			if(!IsDebug())
				return;
		}

	}
}