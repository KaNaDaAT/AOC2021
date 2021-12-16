using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCoding.Aufgaben {
	public class Aufgabe4A : AufgabeAbstract {

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

			for(int i = 0; i < draws.Count; i++) {
				foreach(BingoBoard board in bingoBoards) {
					if(board.Select(draws[i])) {
						output = board.CalculateValue(draws[i]);
						i = draws.Count;
						break;
					}
				}
			}

			this.result = output;
		}

	}

	class BingoBoard {
		int[] boardListed = null;
		int rowLength = 0;
		bool[] boardSelection = null;

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
	}
}