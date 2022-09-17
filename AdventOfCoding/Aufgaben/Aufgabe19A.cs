using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace AdventOfCoding.Aufgaben {
	public class Aufgabe19A : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			List<HashSet<(int x, int y, int z)>> scanners = new List<HashSet<(int x, int y, int z)>>();
			string[] lines = reader.ReadAndGetLines();

			int scannerDepth = -1;
			foreach(string line in lines) {
				if(line.Length == 0)
					continue;
				if(line.StartsWith("---")) {
					scannerDepth++;
					scanners.Add(new HashSet<(int x, int y, int z)>());
					continue;
				} else {
					int[] lineValues = line.Split(",").Select(Int32.Parse).ToArray();
					scanners[scannerDepth].Add((lineValues[0], lineValues[1], lineValues[2]));
				}
			}

			HashSet<(int x, int y, int z)> set = Align(scanners[0], scanners[1]);
			if(!IsDebug())
				return;
			toPrint.AppendLine(set.Count + "");
			foreach(var element in set) {
				toPrint.AppendLine(element.x + ", " + element.y + ", " + element.z);
			}

			this.result = 0;
		}

		public HashSet<(int x, int y, int z)> Align(
			HashSet<(int x, int y, int z)> compareWith,
			HashSet<(int x, int y, int z)> compare
		) {
			foreach(var beaconc in compare) {
				var rotatedBeaconC = GetRotations(beaconc).ToArray();
				int[] correctAtRot = new int[24];
				int indexer = 0;
				foreach((int x, int y, int z) rotated in rotatedBeaconC) {
					HashSet<(int x, int y, int z)> outPut = new HashSet<(int x, int y, int z)>();
					foreach((int x, int y, int z) beaconcw in compareWith) {
						HashSet<(int x, int y, int z)>  transformed = new HashSet<(int x, int y, int z)>();
						(int x, int y, int z) delta = (
							beaconcw.x + rotated.x,
							beaconcw.y - rotated.y,
							beaconcw.z + rotated.z
						);
						foreach((int x, int y, int z) ri in rotatedBeaconC) {
							transformed.Add((ri.x + delta.x, ri.y + delta.y, ri.z + delta.z));
						}
						if(transformed.Intersect(compareWith).Count(x => true) > 0) {
							foreach(var t in transformed.Intersect(compareWith)) {
								outPut.Add(t);
							}
							correctAtRot[indexer]++;
						}
					}
					if(correctAtRot[indexer] >= 12) {
						return outPut;
					}
					indexer++;
				}
			}
			return null;
		}

		public HashSet<(int x, int y, int z)> GetRotations((int x, int y, int z) position) {
			HashSet<(int x, int y, int z)> rots = new HashSet<(int x, int y, int z)>();
			for(int i = 0; i < 3; i++) {
				position = (position.y, position.z, position.x);
				rots.Add(position);
				(int x, int y, int z) tempPos1 = (-position.x, -position.y, position.z);
				(int x, int y, int z) tempPos2 = position;
				rots.Add(tempPos1);
				for(int j = 0; j < 4; j++) {
					tempPos1 = (tempPos1.x, -tempPos1.z, tempPos1.y);
					tempPos2 = (tempPos2.x, -tempPos2.z, tempPos2.y);
					rots.Add(tempPos1);
					rots.Add(tempPos2);
				}
			}
			return rots;
		}

	}
}