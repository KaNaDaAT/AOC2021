using Lib;
using System.Diagnostics;
using System.Text;

namespace AdventOfCoding.Aufgaben {
	public abstract class AufgabeAbstract {

		protected object result;
		protected StringBuilder toPrint = new StringBuilder();
		private bool isDebugMode;
		private Stopwatch stopwatch;

		public (string output, Stopwatch sw) MainMethod(Reader reader, bool isDebugMode = false) {
			this.isDebugMode = isDebugMode;
			reader.ReadAndGetLines();
			stopwatch = new Stopwatch();

			stopwatch.Start();
			Runner(reader);
			stopwatch.Stop();

			if(result == null) {
				return (null, null);
			}

			if(IsDebug()) {
				PrintOutput();
			}

			return (result.ToString(), stopwatch);
		}

		protected abstract void Runner(Reader reader);

		public void PrintOutput() {
			if(toPrint.Length > 0 && isDebugMode) {
				UIConsole.WriteLine(toPrint.ToString());
			}
		}

		public bool IsDebug() {
			return isDebugMode;
		}

		public bool IfDebugStopTimer() {
			if(isDebugMode) {
				stopwatch.Stop();
			}
			return isDebugMode;
		}

	}
}