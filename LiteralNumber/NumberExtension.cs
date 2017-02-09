using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
	public static class NumberExtension
	{
		private static LiteralNumber GetWords(decimal number)
		{
			if (Math.Abs(number) > 999999999999999999M)
			{
				throw new ArgumentException("Nombre trop grand. Non pris en charge :(");
			}
			LiteralNumber ret = new LiteralNumber();
			ret.OriginalNumber = number;
			if (number == 0)
			{
				ret.IntegralPart = _units[0];
				return ret;
			}
			List<string> words = new List<string>();
			bool neg = false;
			if (number < 0)
			{
				neg = true;
				number = Math.Abs(number);
			}
			decimal intPart = Math.Truncate(number);
			if (intPart == 0)
			{
				words.Add(_units[0]);
			}
			decimal decPart = Math.Truncate((number - intPart) * 1000); //on se limite à 3 chiffres après la virgule
			while (decPart % 10 == 0 && decPart > 0)
			{
				decPart /= 10;
			}
			decimal temp = intPart;
			decimal[] parts = new decimal[6]; //6 parties de 3 chiffres = Million de milliards
			int idx = 0;
			while (Math.Truncate(temp / 1000) > 0)
			{
				parts[idx] = temp - (Math.Truncate(temp / 1000) * 1000);
				temp = Math.Truncate(temp / 1000);
				idx++;
			}
			parts[idx] = temp;
			//parcours le tableau à l'envers
			for (int i = idx; i >= 0; i--)
			{
				if (parts[i] == 0)
				{
					if (i == 3)
					{
						words.Add(_separators[i]);
					}
					continue;
				}
				if ((i % 3) == 1 && parts[i] == 1)
				{
					words.Add(_separators[i]);
					continue;
				}
				words.Add(GroupToMoneyWord(parts[i]));
				words.Add(_separators[i] + (parts[i] > 1 && (i % 4 == 2 | i % 4 == 3) ? "s" : ""));
			}
			ret.IntegralPart = string.Join(" ", words.Where(w => !string.IsNullOrWhiteSpace(w)));
			ret.IsNegative = neg;
			if (decPart > 0)
			{
				ret.DecimalPart = GroupToMoneyWord(decPart);
			}
			return ret;
		}
		private static string GroupToMoneyWord(decimal group)
		{
			List<string> words = new List<string>();
			int grp = Convert.ToInt32(group);
			int cent = grp / 100;
			grp = grp - (cent * 100);
			if (cent > 0)
			{
				if (cent > 1)
				{
					words.Add(_units[cent]);
				}
				words.Add((cent > 1 && grp == 0) ? "cents" : "cent");
			}
			if (grp == 0)
				return string.Join(" ", words);
			if (grp <= 16)
			{
				words.Add(_units[grp]);
			}
			if (grp > 16 && grp < 20)
			{
				words.Add(_units[10] + "-" + _units[grp % 10]);
			}
			if (grp > 19 && grp < 70)
			{
				string t = _tenth[grp / 10];
				if (grp % 10 == 1)
				{
					t += " et un";
				}
				if (grp % 10 > 1)
				{
					t += "-" + _units[grp % 10];
				}
				words.Add(t);
			}
			if (grp >= 70 && grp < 77)
			{
				if (grp == 71)
					words.Add(_tenth[6] + " et " + _units[grp - 60]);
				else
					words.Add(_tenth[6] + "-" + _units[grp - 60]);
			}
			if (grp > 76 && grp < 80)
			{
				words.Add(_tenth[6] + "-dix-" + _units[grp - 70]);
			}
			if (grp >= 80 && grp < 100)
			{
				string t = "quatre-vingt";
				if (grp < 97 && grp != 80)
				{
					t += "-" + _units[grp - 80];
				}
				if (grp > 96)
				{
					t += "-dix-" + _units[grp - 90];
				}
				words.Add(t);
			}
			return string.Join(" ", words.Where(w => !string.IsNullOrWhiteSpace(w)));
		}
		private static string[] _units = new[] { "zéro", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf", "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize" };
		private static string[] _tenth = new[] { "", "", "vingt", "trente", "quarante", "cinquante", "soixante" };
		private static string[] _separators = new[] { "", "mille", "million", "milliard", "mille", "million" };
		public static string ToWord(this int number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		public static string ToWord(this short number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		public static string ToWord(this long number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		public static string ToWord(this uint number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		public static string ToWord(this ushort number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		public static string ToWord(this ulong number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		public static string ToWord(this float number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		public static string ToWord(this double number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		public static string ToWord(this decimal number, LiteralNumberFormat format)
		{
			return GetWords(number).ToString(format);
		}
		public static string ToWord(this sbyte number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		public static string ToWord(this byte number, LiteralNumberFormat format)
		{
			return Convert.ToDecimal(number).ToWord(format);
		}
		private class LiteralNumber
		{
			private decimal originalNumber;

			public decimal OriginalNumber
			{
				get { return originalNumber; }
				set
				{
					integralPart = Math.Truncate(value);
					decimalPart = (value - integralPart) * 1000;
					originalNumber = value;
				}
			}

			public bool IsNegative { get; set; }
			public string IntegralPart { get; set; }
			private decimal integralPart;
			private decimal decimalPart;
			public string DecimalPart { get; set; }
			/// <summary>
			/// Retourne le nombre au format litéral normal
			/// </summary>
			/// <returns></returns>
			public override string ToString()
			{
				return ToString(LiteralNumberFormat.Normal);
			}
			/// <summary>
			/// Retourne le nombre au format litéral choisi
			/// </summary>
			/// <param name="format">Format du nombre litéral</param>
			/// <returns></returns>
			public string ToString(LiteralNumberFormat format)
			{
				string ret = (IsNegative ? "moins " : "");
				switch (format)
				{
					case LiteralNumberFormat.Normal:
						ret += IntegralPart + (!string.IsNullOrWhiteSpace(DecimalPart) ? " virgule " + DecimalPart : "");
						break;
					case LiteralNumberFormat.Recommandation1990:
						ret += IntegralPart.Replace(' ', '-') + (!string.IsNullOrWhiteSpace(DecimalPart) ? " virgule " + DecimalPart.Replace(' ', '-') : "");
						break;
					case LiteralNumberFormat.Money:
						ret += string.Format("{0} euro{1}", IntegralPart, integralPart > 0 ? "s" : "") + ((!string.IsNullOrWhiteSpace(DecimalPart) ? string.Format(" et {0} centime{1}", DecimalPart, decimalPart > 0 ? "s" : "") : ""));
						break;
					default:
						break;
				}
				return ret;
			}
		}
		public enum LiteralNumberFormat
		{
			Normal,
			Recommandation1990,
			Money
		}
	}
}
