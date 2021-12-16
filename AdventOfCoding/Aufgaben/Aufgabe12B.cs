using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe12B : AufgabeAbstract {

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
			HashSet<string> paths = GenerateAllPaths(map, "start", "end");
			this.result = paths.Count;

			/* Output Region */
			if(!IsDebug())
				return;

			foreach(string path in paths.OrderBy(x => x)) {
				this.toPrint.AppendLine(path);
			}
		}
		private int maxDepth = 20;
		private HashSet<string> GenerateAllPaths(Dictionary<string, List<string>> map, string start, string end, string path = "", bool smallCaveMulti = true, int iter = 0) {
			iter++;
			HashSet<string> outList = new HashSet<string>();
			if(iter >= maxDepth)
				return outList;
			if(start == end) {
				outList.Add(path);
				return outList;
			}
			var insmap = map.ToDictionary(p => p.Key, p => p.Value.ToList());
			List<string> startList = new List<string>(insmap[start]);
			path += start + ", ";
			if(start.ToLower().Equals(start)) {
				if(smallCaveMulti && !(start.Equals("start") || start.Equals("end"))) {
					foreach(string destination in startList) {
						outList.UnionWith(GenerateAllPaths(insmap, destination, end, path, false, iter));
					}
				}
				insmap.Remove(start);
				foreach(List<string> destinations in insmap.Values) {
					destinations.Remove(start);
				}
				foreach(string destination in startList) {
					outList.UnionWith(GenerateAllPaths(insmap, destination, end, path, smallCaveMulti, iter));
				}
			} else {
				foreach(string destination in startList) {
					outList.UnionWith(GenerateAllPaths(insmap, destination, end, path, smallCaveMulti, iter));
				}
			}
			/*if(start.ToLower().Equals(start)) {
				if(start.EndsWith("1") || start.Equals("start") || start.Equals("end")) {
					insmap.Remove(start);
					foreach(List<string> destinations in insmap.Values) {
						destinations.Remove(start);
					}
				} else {
					Utils.RenameKey(insmap, start, start + "1");
					foreach(List<string> destinations in insmap.Values) {
						if(destinations.Contains(start))
							destinations.Add(start + "1");
						destinations.Remove(start);
					}
					start = start + "1";
				}
			}*/
			return outList;
		}

	}
}