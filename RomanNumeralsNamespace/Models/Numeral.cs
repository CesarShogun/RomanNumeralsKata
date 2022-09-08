namespace RomanNumeralsNamespace.Models
{
    public class Numeral
    {
        public Numeral(char letter, int value, bool repeteable, bool subtractive)
        {
            Letter = letter;
            Value = value;
            Repeteable = repeteable;
            Subtractive = subtractive;
        }

        public char Letter { get; private set; }
        public int Value { get; private set; }
        public bool Repeteable { get; private set; }
        public bool Subtractive { get; private set; }
    }
}
