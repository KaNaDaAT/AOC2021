using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Graph = System.Collections.Generic.Dictionary<(int, int), AdventOfCoding.Aufgaben.AOCNode>;


namespace AdventOfCoding.Aufgaben {

	public class Aufgabe15A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			int rowSize = reader.ReadAndGetLines()[0].Length;
			Dictionary<(int x, int y), AOCNode> nodes = new Dictionary<(int x, int y), AOCNode>();
			int y = 0;
			foreach(var line in reader.ReadAndGetLines()) {
				for(int x = 0; x < line.Length; x++) {
					nodes[(x, y)] = new AOCNode(x, y, line[x] - '0');
				}
				y += 1;
			}

			this.result = Dijkstra(nodes, nodes[(rowSize - 1, rowSize - 1)], rowSize);
		}

		int elementCount = 0;
		int Dijkstra(Dictionary<(int x, int y), AOCNode> graph, AOCNode finish, int rowSize) {
			graph[(0, 0)].Distance = 0;
			var priorityDict = new Dictionary<int, List<AOCNode>>();
			priorityDict.Add(0, new List<AOCNode>());
			priorityDict[0].Add(graph[(0, 0)]);
			int maxPriority = 0;
			elementCount++;

			while(elementCount != 0) {
				var curAOCNode = GetAOCNodeWithPriority(priorityDict);
				if(!curAOCNode.Visited) {
					curAOCNode.Visited = true;

					if(curAOCNode.Equals(finish)) {
						return finish.Distance;
					}

					foreach(var neighbor in curAOCNode.GetAdjacent(graph, rowSize, rowSize)) {
						int dist = curAOCNode.Distance + neighbor.Cost;
						if(dist < neighbor.Distance) {
							neighbor.Distance = dist;
						}
						if(neighbor.Distance != int.MaxValue) {
							for(int i = maxPriority + 1; i <= neighbor.Distance; i++) {
								priorityDict.Add(i, new List<AOCNode>());
							}
							if(maxPriority < neighbor.Distance) {
								maxPriority = neighbor.Distance;
							}
							priorityDict[neighbor.Distance].Add(neighbor);
							highestPriority = highestPriority <= neighbor.Distance ? highestPriority : neighbor.Distance;
							elementCount++;
						}
					}
				}
			}
			return finish.Distance;
		}

		int highestPriority = 0;
		private AOCNode GetAOCNodeWithPriority(Dictionary<int, List<AOCNode>> dict) {
			for(int i = highestPriority; i < dict.Count; i++) {
				if(dict[i].Count != 0) {
					AOCNode AOCNode = dict[i][0];
					dict[i].RemoveAt(0);
					if(dict[i].Count == 0) {
						highestPriority = 0;
						for(int j = i + 1; j < dict.Count; j++) {
							if(dict[j].Count != 0) {
								highestPriority = j;
								break;
							}
						}
					}
					elementCount--;
					return AOCNode;
				}
			}
			return null;
		}
	}
}