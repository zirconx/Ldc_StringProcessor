using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringProcessor.CharacterProcessors;
using StringProcessor.Enums;

namespace StringProcessor.Test.Unit.CharacterProcessors
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DollarToPoundTests
    {
        [TestMethod]
        public void ChrIsMatch_CharIsPound()
        {
            //Set up
            var processor = new DollarToPound();
            var chr = '$';

            //Act
            var result = processor.ProcessCharacter(new StringBuilder(), ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual('£', chr);
        }


        [TestMethod]
        public void ChrIsNotMatch_NoChange()
        {
            //Set up
            var processor = new DollarToPound();
            var chr = 'A';

            //Act
            var result = processor.ProcessCharacter(new StringBuilder(), ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual('A', chr);
        }

        [TestMethod]
        public void ChrIsMin_Continue()
        {
            //Set up
            var processor = new DollarToPound();
            var chr = char.MinValue;

            //Act
            var result = processor.ProcessCharacter(new StringBuilder(), ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual(char.MinValue, chr);
        }

        [TestMethod]
        public void ChrIsMax_Continue()
        {
            //Set up
            var processor = new DollarToPound();
            var chr = char.MaxValue;

            //Act
            var result = processor.ProcessCharacter(new StringBuilder(), ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual(char.MaxValue, chr);
        }

        [TestMethod]
        public void CurrentIsNull_Continue()
        {
            //Set up
            var processor = new DollarToPound();
            var chr = char.MinValue;

            //Act
            var result = processor.ProcessCharacter(null, ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual(char.MinValue, chr);
        }
    }
}