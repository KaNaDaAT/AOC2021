using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe13B : AufgabeAbstract {

		int rowSize = 0;
		int columnSize = 0;

		protected override void Runner(Reader reader) {
			string[] data = reader.ReadAll().Split(Environment.NewLine + Environment.NewLine);
			List<int> coords = data[0].Replace(Environment.NewLine, ",").Split(',').Select(Int32.Parse).ToList();

			for(int i = 0; i < coords.Count; i++) {
				if(i % 2 == 0) {
					rowSize = rowSize > coords[i] ? rowSize : coords[i];
				} else {
					columnSize = columnSize > coords[i] ? columnSize : coords[i];
				}
			}
			rowSize++;
			columnSize++;
			bool[] coordsystem = new bool[rowSize * columnSize];

			for(int i = 0; i < coords.Count; i += 2) {
				coordsystem[coords[i] + coords[i + 1] * rowSize] = true;
			}

			string[] folds = data[1].Replace("fold along ", "").Split(Environment.NewLine);
			for(int i = 0; i < folds.Length; i++) {
				var fold = folds[i].Split('=');
				coordsystem = FoldCoords(coordsystem, fold[0][0], Int32.Parse(fold[1]));
			}

			if(IsDebug()) {
				for(int i = 0; i < coordsystem.Length; i++) {
					if(i % rowSize == 0) {
						toPrint.AppendLine();
					}
					toPrint.Append(coordsystem[i] ? "#" : ".");
				}
			}

			this.result = coordsystem.Count(x => x);
		}

		private bool[] FoldCoords(bool[] coordsystem, char foldAxis, int foldCoord) {
			if(foldAxis == 'y') {
				bool[] coordoutput = new bool[rowSize * foldCoord];
				for(int row = 0; row < foldCoord; row++) {
					for(int column = 0; column < rowSize; column++) {
						int opindex = (columnSize - row - 1) * rowSize + column;
						coordoutput[column + row * rowSize] = coordsystem[column + row * rowSize] || coordsystem[opindex];
					}
				}
				columnSize = foldCoord;
				return coordoutput;
			} else {
				bool[] coordoutput = new bool[columnSize * foldCoord];
				for(int row = 0; row < columnSize; row++) {
					for(int column = 0; column < foldCoord; column++) {
						int opindex = row * rowSize + rowSize - column - 1;
						coordoutput[column + row * foldCoord] = coordsystem[column + row * rowSize] || coordsystem[opindex];
					}
				}
				rowSize = foldCoord;
				return coordoutput;
			}
		}
	}
}