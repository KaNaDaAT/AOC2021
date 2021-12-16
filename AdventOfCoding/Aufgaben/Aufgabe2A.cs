using Lib;
using System;
using System.Diagnostics;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe2A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			int horizontal = 0;
			int depth = 0;

			string[] lines = reader.ReadAndGetLines();
			for(int i = 0; i < lines.Length; i++) {
				string command = lines[i].Substring(0, lines[i].IndexOf(" "));
				int value = int.Parse(lines[i].Substring(lines[i].IndexOf(" ")));
				switch(command) {
					case "forward":
						horizontal += value;
						break;
					case "up":
						depth -= value;
						break;
					case "down":
						depth += value;
						break;
					default:
						UIConsole.WriteLine("Input has Errors!");
						break;
				}
			}

			this.result = horizontal * depth;
		}

	}
}