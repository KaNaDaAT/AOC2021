using Lib;
using System;
using System.Diagnostics;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe2B : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			int horizontal = 0;
			int depth = 0;
			int aim = 0;

			string[] lines = reader.ReadAndGetLines();
			for(int i = 0; i < lines.Length; i++) {
				string command = lines[i].Substring(0, lines[i].IndexOf(" "));
				int value = int.Parse(lines[i].Substring(lines[i].IndexOf(" ")));
				switch(command) {
					case "forward":
						horizontal += value;
						depth += value * aim;
						break;
					case "up":
						aim -= value;
						break;
					case "down":
						aim += value;
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