using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			decimal[] d = new[] { 21M, 18M, 199M, 45742M, 2500M };
			foreach (decimal item in d)
			{
				Console.WriteLine($"{item} : {item.ToWord(NumberExtension.LiteralNumberFormat.Normal)}");
				Console.WriteLine($"{item} : {item.ToWord(NumberExtension.LiteralNumberFormat.Money)}");
				Console.WriteLine($"{item} : {item.ToWord(NumberExtension.LiteralNumberFormat.Recommandation1990)}");
			}
			Console.WriteLine("Appuyez sur une touche pour continuer...");
			Console.ReadKey();
			string text = "";
			string rep = "";
			Console.Clear();
			while (!text.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
			{
				Console.SetCursorPosition(0, 0);
				Console.Write("                                                                                              ");
				Console.SetCursorPosition(0, 0);
				Console.Write("Sasissez un nombre (tapez 'exit' pour terminer) : ");
				text = Console.ReadLine().Trim();
				decimal dec;
				if (decimal.TryParse(text, out dec))
				{
					Console.SetCursorPosition(0, 1);
					Console.Write(new string(' ', rep.Length));
					Console.SetCursorPosition(0, 1);
					rep = $"{dec} : {dec.ToWord(NumberExtension.LiteralNumberFormat.Normal)}";
					Console.WriteLine(rep);
				}
			}
			Console.Clear();
			Console.WriteLine("Appuyez sur une touche pour terminer");
			Console.ReadKey();
		}
	}
}
