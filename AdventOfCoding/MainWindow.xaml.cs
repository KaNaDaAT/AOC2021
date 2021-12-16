using AdventOfCoding.Aufgaben;
using Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdventOfCoding {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>

	public partial class MainWindow : Window {

		private List<Button> AOCButtons = new List<Button>();

		public MainWindow() {
			InitializeComponent();
			UIConsole.WriteLine("AdventOfCoding Started...");
			UIConsole.SwitchToCommandMode();
			UIConsole.SetDispatcher(this.Dispatcher);

			CreateAdventButtons();

			UndefinedCommand commandOpen = new UndefinedCommand();
			commandOpen.Define("open", new Action<string>(CommandOpenFile)); // TODO Besser im Define mitgeben wie viele parameter. x für unendlich
			UIConsole.Instance.commands.Add("open", commandOpen);

			UndefinedCommand commandStart = new UndefinedCommand();
			commandStart.Define("start b-d", new Action<string, bool>(CommandStartAufgabe)); // TODO Besser im Define mitgeben wie viele parameter. x für unendlich
			UIConsole.Instance.commands.Add("start", commandStart);
		}

		private void CreateAdventButtons() {
			int colMod = 0;
			int rowMod = 0;
			for(int row = 0; row < 5; row++) {
				colMod = 0;
				for(int column = 0; column < 10; column++) {
					if(column != 0 && column % 2 == 0)
						colMod++;

					Button button = new Button();
					button.Click += OnAdventButtonClick;

					button.Background = new ImageBrush(new BitmapImage(new Uri("data/tree.png", UriKind.Relative)));
					button.Background.Opacity = 0.85;
					button.BorderThickness = new Thickness(0.5);
					button.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 23, 179, 39));
					button.Foreground = new SolidColorBrush(Color.FromArgb(255, 186, 52, 52));
					button.FontWeight = FontWeights.Bold;
					button.FontSize = 20;
					button.Width = 50;
					button.Height = 50;

					this.AOCButtons.Add(button);

					if(column % 2 == 0) {
						button.Content = (row * 5 + column / 2 + 1) + "A";
					} else {
						button.Content = (row * 5 + column / 2 + 1) + "B";
					}
					Grid.SetColumn(button, column + colMod);
					Grid.SetRow(button, row + rowMod);
					adventCalendar.Children.Add(button);
				}
				rowMod++;
			}
		}

		public void OnAdventButtonClick(object sender, RoutedEventArgs e) {
			Button button = sender as Button;
			if(button != null) {
				UIConsole.SwitchToExecuteMode();
				foreach(var aocbutton in AOCButtons) {
					aocbutton.IsEnabled = false;
				}
				string prog = button.Content.ToString();
				bool IsDebug = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
				bool IsOpenFile = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
				Thread thread = new Thread(() => RunAOC(prog, IsDebug, IsOpenFile));
				thread.Start();
			}
		}

		private void CommandOpenFile(string Aufgabe) {
			Process.Start("notepad.exe", Environment.CurrentDirectory + "/Aufgabendata/" + Aufgabe + ".data");
		}

		private void CommandStartAufgabe(string Aufgabe, bool IsDebug) {
			Type t = Type.GetType("AdventOfCoding.Aufgaben." + Aufgabe);
			if(t == null) {
				UIConsole.WriteLine("Class 'AdventOfCoding.Aufgaben" + Aufgabe + "' not found!");
			} else {
				AufgabeAbstract aufgabe = (AufgabeAbstract) Activator.CreateInstance(t);
				(string output, Stopwatch time) resultData = (null, null);
				if(Debugger.IsAttached) {
					resultData =
							aufgabe.MainMethod(
								new Reader(Environment.CurrentDirectory + "/Aufgabendata/" + Aufgabe + ".data"),
								IsDebug
							);
				} else {
					try {
						resultData =
							aufgabe.MainMethod(
								new Reader(Environment.CurrentDirectory + "/Aufgabendata/" + Aufgabe + ".data"),
								IsDebug
							);
					} catch(Exception exc) {
						UIConsole.WriteLine(exc.StackTrace);
					}
				}
				if(resultData == (null, null)) {
					UIConsole.WriteLine("Not Implemented!");
				} else {
					UIConsole.WriteLine("Output: " + resultData.output);
					UIConsole.WriteLine("Time elapsed: " + (long) (resultData.time.Elapsed.TotalMilliseconds * 1000) + "µs");
				}
			}
		}
			

		public void RunAOC(string prog, bool IsDebug, bool IsOpenFile) {
			if(IsOpenFile) {
				UIConsole.WriteLine("data Aufgabe" + prog);
				CommandOpenFile("Aufgabe" + prog);
			} else {
				UIConsole.WriteLine("start Aufgabe" + prog + (IsDebug ? " -d " : ""));
				CommandStartAufgabe("Aufgabe" + prog, IsDebug);
			}
			this.Dispatcher.Invoke(() => {
				foreach(var aocbutton in AOCButtons) {
					aocbutton.IsEnabled = true;
				}
				UIConsole.SwitchToCommandMode();
			});
		}
	}

}
