using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe12A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			string[] lines = reader.ReadAndGetLines();
			Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

			for(int i = 0; i < lines.Length; i++) {
				string x = lines[i].Split('-')[0];
				string y = lines[i].Split('-')[1];
				if(map.ContainsKey(x)) {
					map[x].Add(y);
				} else {
					map.Add(x, new List<string>() { y });
				}
				if(map.ContainsKey(y)) {
					map[y].Add(x);
				} else {
					map.Add(y, new List<string>() { x });
				}
			}
			this.result = GenerateAllPaths(map, "start", "end");

			/* Output Region */
			if(!IfDebugStopTimer())
				return;

			foreach(string key in map.Keys) {
				this.toPrint.Append(key + ": ");
				foreach(string element in map[key]) {
					this.toPrint.Append(element + ", ");
				}
				this.toPrint.AppendLine("");
			}
		}

		private int maxDepth = 20;
		private int GenerateAllPaths(Dictionary<string, List<string>> map, string start, string end, int iter = 0) {
			iter++;
			if(iter >= maxDepth)
				return 0;
			if(start == end)
				return 1;
			var insmap = map.ToDictionary(p => p.Key, p => p.Value.ToList());
			int count = 0;
			List<string> startList = new List<string>(insmap[start]);
			if(start.ToLower().Equals(start)) {
				insmap.Remove(start);
				foreach(List<string> destinations in insmap.Values) {
					destinations.Remove(start);
				}
			}
			foreach(string destination in startList) {
				count += GenerateAllPaths(insmap, destination, end, iter);
			}
			return count;
		}
	}
}