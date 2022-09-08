using System.Diagnostics.Metrics;

namespace RomanNumeralsNamespace.Models
{
    public static class AllNumerals
    {
        public static readonly List<Numeral> NUMERALS = new()
            {
                new Numeral('I', 1, true, true),
                new Numeral('V', 5, false, false),
                new Numeral('X', 10, true, true),
                new Numeral('L', 50, false, false),
                new Numeral('C', 100, true, true),
                new Numeral('D', 500, false, false),
                new Numeral('M', 1000, true, false)
            };

        public static Numeral GetNumeral(char letter)
        {
            for (int i = 0; i < NUMERALS.Count; i++)
            {
                if (NUMERALS[i].Letter == letter)
                    return NUMERALS[i];
            }

            throw new ArgumentException($"NUMERAL ERROR: The \"{letter}\" numeral does not exist.");
        }

        public static Numeral GetNumeralByValue(int value)
        {
            for (int i = 0; i < NUMERALS.Count; i++)
            {
                if (NUMERALS[i].Value == value)
                    return NUMERALS[i];
            }

            throw new ArgumentException($"NUMERAL ERROR: No numeral exists with a value of \"{value}\".");
        }
    }
}
