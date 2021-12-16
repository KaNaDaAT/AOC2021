using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe14B : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			string data = reader.ReadAll();
			Dictionary<string, long> template = new Dictionary<string, long>();
			Dictionary<char, long> count = new Dictionary<char, long>();
			var temp = data.Substring(0, data.IndexOf(Environment.NewLine));
			data = data.Substring(data.IndexOf(Environment.NewLine + Environment.NewLine) + (2 * Environment.NewLine.Length));
			for(int i = 0; i < temp.Length - 1; i++) {
				string key = temp.Substring(i, 2);
				template.AddToDictionary(key);
				count.AddToDictionary(key[0]);
			}
			count.AddToDictionary(temp[temp.Length - 1]);

			string[] lines = data.Replace(" -> ", Environment.NewLine).Split(Environment.NewLine).ToArray();
			for(int i = 0; i < 40; i++) {
				Dictionary<string, long> tempdict = new Dictionary<string, long>();
				foreach(string key in template.Keys) {
					tempdict.Add(key, 0);
				}
				for(int j = 0; j < lines.Length; j += 2) {
					if(template.ContainsKey(lines[j])) {
						tempdict.AddToDictionary(lines[j][0] + lines[j + 1], template[lines[j]]);
						tempdict.AddToDictionary(lines[j + 1] + lines[j][1], template[lines[j]]);
						count.AddToDictionary(lines[j + 1][0], template[lines[j]]);
					}
				}
				template = tempdict;
			}
			this.result = count.Values.Max() - count.Values.Min();
		}
	}
}