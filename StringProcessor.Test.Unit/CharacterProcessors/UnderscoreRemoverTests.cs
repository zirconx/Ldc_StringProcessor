using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringProcessor.CharacterProcessors;
using StringProcessor.Enums;

namespace StringProcessor.Test.Unit.CharacterProcessors
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UnderscoreRemoverTests
    {
        [TestMethod]
        public void ChrIsMatch_CharIsMin()
        {
            //Set up
            var processor = new UnderscoreRemover();
            var chr = '_';

            //Act
            var result = processor.ProcessCharacter(new StringBuilder(), ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual(char.MinValue, chr);
        }


        [TestMethod]
        public void ChrIsNotMatch_NoChange()
        {
            //Set up
            var processor = new UnderscoreRemover();
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
            var processor = new UnderscoreRemover();
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
            var processor = new UnderscoreRemover();
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
            var processor = new UnderscoreRemover();
            var chr = char.MinValue;

            //Act
            var result = processor.ProcessCharacter(null, ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual(char.MinValue, chr);
        }
    }
}