using NUnit.Framework;
using FluentAssertions;
using RomanNumeralsNamespace.Models;

namespace RomanNumeralsTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_Handle_Null_Or_Empty()
        {
            var ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt(null));
            Assert.That(ex.Message, Is.EqualTo("NumeralToInt recieved an empty string or null."));
        }

        [Test]
        public void Only_Roman_Numerals_Should_Be_Allowed()
        {
            var ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("JII"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL ERROR: The \"J\" numeral does not exist."));

            ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("xII"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL ERROR: The \"x\" numeral does not exist."));
        }

        [Test]
        public void No_More_Than_4_Repetitions_By_Numeral()
        {
            var ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("MMMMCM"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: No numeral can be repeated more than 4 times."));

            ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("DCCCLXXXXIX"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: No numeral can be repeated more than 4 times."));
        }

        [Test]
        public void Some_Numerals_Cannot_Be_Repeated_More_Than_Once()
        {
            var ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("XXVV"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: The \"V\" numeral cannot be repeated."));

            ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("CLLV"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: The \"L\" numeral cannot be repeated."));
        }

        [Test]
        public void Some_Numerals_Cannot_Be_Used_To_Substract()
        {
            var ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("MLC"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: The \"L\" numeral cannot be used to substract."));

            ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("MMCVL"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: The \"V\" numeral cannot be used to substract."));
        }

        [Test]
        public void No_Double_Numeral_Substraction()
        {
            var ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("IIX"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: Incorrect usage of substracting numerals."));

            ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("IXL"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: Incorrect usage of substracting numerals."));
        }

        [Test]
        public void Repeteable_Numerals_Must_Not_Repeat_Anywhere()
        {
            var ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("XCX"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: Incorrect usage of the repeteable numeral \"X\"."));

            ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("IXXXVII"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: Incorrect usage of the repeteable numeral \"I\"."));
        }

        [Test]
        public void Some_Numerals_Cannot_Substract_Certain_Numerals()
        {
            var ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("IC"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: The \"I\" numeral cannot be used to substract from the \"C\" numeral."));
            
            ex = Assert.Throws<ArgumentException>(() => RomanNumerals.NumeralToInt("MXM"));
            Assert.That(ex.Message, Is.EqualTo("NUMERAL FORMATING ERROR: The \"X\" numeral cannot be used to substract from the \"M\" numeral."));
        }

        [Test]
        public void Must_Return_Right_Number()
        {
            RomanNumerals.NumeralToInt("III").Should().Be(3);
            RomanNumerals.NumeralToInt("IV").Should().Be(4);
            RomanNumerals.NumeralToInt("VII").Should().Be(7);
            RomanNumerals.NumeralToInt("IX").Should().Be(9);
            RomanNumerals.NumeralToInt("XIV").Should().Be(14);
            RomanNumerals.NumeralToInt("DCCCLXXX").Should().Be(880);
            RomanNumerals.NumeralToInt("CMXXXVIII").Should().Be(938);
            RomanNumerals.NumeralToInt("CMXC").Should().Be(990);
            RomanNumerals.NumeralToInt("MCCCXC").Should().Be(1390);
            RomanNumerals.NumeralToInt("MMCDLXXV").Should().Be(2475);
            RomanNumerals.NumeralToInt("MMMDCCXXIV").Should().Be(3724);
            RomanNumerals.NumeralToInt("MMMCMXCIX").Should().Be(3999);
        }

        [Test]
        public void IntToNumeral_Test()
        {
            RomanNumerals.IntToNumeral(3562).Should().Be("MMMCCCCCXXXXXXII");
        }
    }
}