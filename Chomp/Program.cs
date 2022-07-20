using System;
using System.Windows.Forms;
using Chomp.Properties;

namespace Chomp;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		Settings @default = Settings.Default;
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.Run(new Main());
		@default.Save();
	}
}
