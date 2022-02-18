using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringProcessor.CharacterReplacement;

namespace StringProcessor.Test.Unit.CharacterReplacement
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
            var result = processor.ProcessCharacter(new StringBuilder(), chr);

            //Assert
            Assert.AreEqual(char.MinValue, result);
        }


        [TestMethod]
        public void ChrIsNotMatch_NoChange()
        {
            //Set up
            var processor = new UnderscoreRemover();
            var chr = 'A';

            //Act
            var result = processor.ProcessCharacter(new StringBuilder(), chr);

            //Assert
            Assert.AreEqual('A', result);
        }

        [TestMethod]
        public void ChrIsMin_Continue()
        {
            //Set up
            var processor = new UnderscoreRemover();
            var chr = char.MinValue;

            //Act
            var result = processor.ProcessCharacter(new StringBuilder(), chr);

            //Assert
            Assert.AreEqual(char.MinValue, result);
        }

        [TestMethod]
        public void ChrIsMax_Continue()
        {
            //Set up
            var processor = new UnderscoreRemover();
            var chr = char.MaxValue;

            //Act
            var result = processor.ProcessCharacter(new StringBuilder(), chr);

            //Assert
            Assert.AreEqual(char.MaxValue, result);
        }

        [TestMethod]
        public void CurrentIsNull_Continue()
        {
            //Set up
            var processor = new UnderscoreRemover();
            var chr = char.MinValue;

            //Act
            var result = processor.ProcessCharacter(null, chr);

            //Assert
            Assert.AreEqual(char.MinValue, result);
        }
    }
}