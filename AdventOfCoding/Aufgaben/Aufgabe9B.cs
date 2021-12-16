using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe9B : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			int[] map = reader.ReadAll().Replace(Environment.NewLine, "").ToCharArray().Select(x => x - '0').ToArray();
			int rowLength = reader.GetContent().IndexOf(Environment.NewLine);
			var mins = new List<int>();
			var basinOrgins = new List<Point>();

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
				mins.Add(cur);
				basinOrgins.Add(new Point() { X = i % rowLength, Y = i / rowLength });
			}

			var basins = new List<HashSet<Point>>();
			foreach(var basin in basinOrgins) {
				var visited = new HashSet<Point>();
				var active = new Queue<Point>();
				active.Enqueue(basin);

				while(active.Any()) {
					var currentTile = active.Dequeue();
					visited.Add(currentTile);
					var x = currentTile.X;
					var y = currentTile.Y;
					int i = x + y * rowLength;

					//checkLeft
					if(x != 0 && 9 != map[i - 1]) {
						var p = new Point(x - 1, y);
						if(!visited.Contains(p)) {
							active.Enqueue(p);
						}
					}
					//checkRight
					if(x != rowLength - 1 && 9 != map[i + 1]) {
						var p = new Point(x + 1, y);
						if(!visited.Contains(p)) {
							active.Enqueue(p);
						}
					}
					//checkTop
					if(y != 0 && 9 != map[i - rowLength]) {
						var p = new Point(x, y - 1);
						if(!visited.Contains(p)) {
							active.Enqueue(p);
						}
					}
					//checkBot
					if(y != map.Length / rowLength - 1 && 9 != map[i + rowLength]) {
						var p = new Point(x, y + 1);
						if(!visited.Contains(p)) {
							active.Enqueue(p);
						}
					}
				}
				basins.Add(visited);
			}
			this.result = basins.OrderByDescending(x => x.Count).Take(3).Aggregate(1, (accum, x) => accum * x.Count);
		}
	}
}