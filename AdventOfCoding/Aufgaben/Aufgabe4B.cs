using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe4B : AufgabeAbstract {

		protected override void Runner(Reader reader) {
			int output = 0;
			string data = reader.GetContent();
			List<int> draws = data.Substring(0, data.IndexOf('\n')).Split(',').Select(Int32.Parse).ToList();
			data = data.Substring(data.IndexOf(Environment.NewLine) + Environment.NewLine.Length * 2);
			List<string> bingosStr = data.Split(Environment.NewLine + Environment.NewLine).ToList();

			List<BingoBoard> bingoBoards = new List<BingoBoard>();
			for(int i = 0; i < bingosStr.Count; i++) {
				bingoBoards.Add(new BingoBoard(bingosStr.ElementAt(i)));
			}

			BingoBoard lastWonBoard = null;
			int lastWonBoardDraw = 0;
			for(int i = 0; i < draws.Count; i++) {
				for(int j = 0; j < bingoBoards.Count; j++) {
					if(bingoBoards[j].Select(draws[i])) {
						lastWonBoard = bingoBoards[j].Clone();
						lastWonBoardDraw = draws[i];
						bingoBoards.Remove(bingoBoards[j]);
						j--;
					}
				}
			}
			this.result = lastWonBoard.CalculateValue(lastWonBoardDraw);
		}

		class BingoBoard {
			int rowLength = 0;
			int[] boardListed = null;
			bool[] boardSelection = null;

			private BingoBoard() { }

			public BingoBoard(string boardStr) {
				string[] str = Regex.Split(boardStr, @"[\s\n\r]+").Where(x => !string.IsNullOrEmpty(x)).ToArray();
				boardListed = str.Select(Int32.Parse).ToArray();
				boardSelection = new bool[boardListed.Length];
				rowLength = boardStr.Split(Environment.NewLine).Length;
			}

			public bool Select(int number) {
				for(int i = 0; i < boardListed.Length; i++) {
					if(boardListed[i] == number) {
						boardSelection[i] = true;
					}
				}

				for(int i = 0; i < rowLength; i++) {
					int count = 0;
					for(int j = 0; j < boardListed.Length / rowLength; j++) {
						if(boardSelection[i * boardListed.Length / rowLength + j]) {
							count++;
						}
					}
					if(count == boardListed.Length / rowLength) {
						return true;
					}
				}

				for(int i = 0; i < boardListed.Length / rowLength; i++) {
					int count = 0;
					for(int j = 0; j < rowLength; j++) {
						if(boardSelection[i + j * rowLength]) {
							count++;
						}
					}
					if(count == rowLength) {
						return true;
					}
				}

				return false;
			}

			public int CalculateValue(int calledNumber) {
				int sum = 0;
				for(int i = 0; i < boardListed.Length; i++) {
					if(!boardSelection[i]) {
						sum += boardListed[i];
					}
				}
				return sum * calledNumber;
			}

			public BingoBoard Clone() {
				BingoBoard board = new BingoBoard();
				board.rowLength = this.rowLength;
				board.boardListed = this.boardListed;
				board.boardSelection = this.boardSelection;
				return board;
			}
		}
	}

}