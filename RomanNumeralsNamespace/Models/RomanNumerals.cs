namespace RomanNumeralsNamespace.Models
{
    public static class RomanNumerals
    {
        //--------------- PUBLIC METHODS ---------------
        public static int NumeralToInt(string number)
        {
            if (number == "" || number == null)
                throw new ArgumentException($"NumeralToInt recieved an empty string or null.");

            var numeralNumber = StringToNumeral(number);
            try
            {
                CheckFormat(numeralNumber);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            
            var totalValue = 0;
            Numeral prevNumeral = null;

            for (int i = numeralNumber.Count - 1; i >= 0; i--)
            {
                //We will be checking the numerals from right to left
                if (prevNumeral != null)
                {
                    if (numeralNumber[i].Subtractive && prevNumeral.Value > numeralNumber[i].Value)
                        //this means that the actual numeral is in a substraction position, like "IV"
                        totalValue -= numeralNumber[i].Value;
                    else
                        totalValue += numeralNumber[i].Value;
                }
                else
                    totalValue += numeralNumber[i].Value;

                prevNumeral = numeralNumber[i];
            }

            return totalValue;
        }

        public static string IntToNumeral(int number)
        {
            if (number >= 4000) throw new ArgumentException($"Numbers equal or greater than 4000 are not allowed");

            Numeral[] multiples = { AllNumerals.GetNumeral('M'), AllNumerals.GetNumeral('C'), AllNumerals.GetNumeral('X') };
            int multiplesOfTen = number / 10;
            int multiplesProgression = 0;
            int remainder = number % 10;
            string numeral = "";

            for (var i = 0; i < multiples.Length; i++)
            {
                if (multiplesOfTen >= multiples[i].Value / 10)
                {
                    for (var j = 0; j < multiplesOfTen / (multiples[i].Value / 10); j++)
                    {
                        numeral = numeral + multiples[i].Letter;
                        multiplesProgression += multiples[i].Value / 10;
                    }

                    multiplesOfTen -= multiplesProgression;
                    multiplesProgression = 0;
                }
            }

            if (remainder > 0)
                for (var i = 0; i < remainder; i++)
                    numeral = numeral + "I";

            return numeral;
        }

        //--------------- PRIVATE METHODS ---------------
        //private static 
        
        private static void CheckFormat(List<Numeral> number)
        {
            //Check for more letter repetitions than allowed. For example, "VV" or "XXXXX"
            var repeated = number.GroupBy(num => num);

            foreach (var nrp in repeated)
            {
                if (!nrp.Key.Repeteable && nrp.Count() > 1)
                    throw new ArgumentException($"NUMERAL FORMATING ERROR: The \"{nrp.Key.Letter}\" numeral cannot be repeated.");

                if (nrp.Key.Repeteable && nrp.Count() > 4)
                    throw new ArgumentException($"NUMERAL FORMATING ERROR: No numeral can be repeated more than 4 times.");
            }

            for (var i = 0; i < number.Count; i++)
            {
                if (i + 3 < number.Count && number[i].Equals(number[i + 1]) && number[i].Equals(number[i + 2]) && number[i].Equals(number[i + 3])) //Same numeral more than 3 times in a row
                    throw new ArgumentException($"NUMERAL FORMATING ERROR: No numeral can be used more than 3 times in a row.");

                if (i + 1 < number.Count && number[i + 1].Value > number[i].Value)
                {
                    //This identified a numeral intended to substract, like "IV". Next, let's check if it's correct
                    if (!number[i].Subtractive)
                        throw new ArgumentException($"NUMERAL FORMATING ERROR: The \"{number[i].Letter}\" numeral cannot be used to substract.");
                    else if (number[i + 1].Value > number[i].Value * 10)
                        throw new ArgumentException($"NUMERAL FORMATING ERROR: The \"{number[i].Letter}\" numeral cannot be used to substract from the \"{number[i + 1].Letter}\" numeral.");

                    if (i - 1 >= 0 && number[i - 1].Value < number[i + 1].Value) //this checks if there are more than one substracting numeral, like "IIV" or "IXL"
                        throw new ArgumentException($"NUMERAL FORMATING ERROR: Incorrect usage of substracting numerals.");
                }

                if (i + 1 < number.Count)
                {
                    int nextOccurrenceIndex = number.IndexOf(number[i], i + 1);
                    if (nextOccurrenceIndex - i > 3) //this checks if two repeteable numerals are too far appart, like "XCCLX"
                        throw new ArgumentException($"NUMERAL FORMATING ERROR: Incorrect usage of the repeteable numeral \"{number[i].Letter}\".");

                    if (number.IndexOf(number[i], i + 1) - i > 1 && number[i].Value < number[i + 1].Value && number[nextOccurrenceIndex].Value < number[nextOccurrenceIndex - 1].Value)
                        //the numerals can be 2 or 3 positions far appart, in some cases correctly, like "CMXC", but in come cases not, like "XCLX" or "XCX"
                        throw new ArgumentException($"NUMERAL FORMATING ERROR: Incorrect usage of the repeteable numeral \"{number[i].Letter}\".");
                }
            }
        }

        private static List<Numeral> StringToNumeral(string number)
        {
            var numeralNumber = new List<Numeral>();
            for (int i = 0; i < number.Length; i++)
            {
                numeralNumber.Add(AllNumerals.GetNumeral(number[i]));
            }

            return numeralNumber;
        }
    }
}
