using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringProcessor.CharacterReplacement;

namespace StringProcessor.Test.Unit.CharacterReplacement
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DuplicateCharacterRemoverTests
    {
        [TestMethod]
        public void ChrIsMatch_SetToEmpty()
        {
            //Set up
            var processor = new DuplicateCharacterRemover();
            var currentString = new StringBuilder("ABCD");
            var chr = 'D';

            //Act
            var result = processor.ProcessCharacter(currentString, chr);

            //Assert
            Assert.AreEqual(char.MinValue, result);
        }


        [TestMethod]
        public void ChrIsNotMatch_NoChange()
        {
            //Set up
            var processor = new DuplicateCharacterRemover();
            var currentString = new StringBuilder("ABCD");
            var chr = 'Z';

            //Act
            var result = processor.ProcessCharacter(currentString, chr);

            //Assert
            Assert.AreEqual('Z', result);
        }

        [TestMethod]
        public void ChrIsMin_Continue()
        {
            //Set up
            var processor = new DuplicateCharacterRemover();
            var currentString = new StringBuilder("ABCD");
            var chr = char.MinValue;

            //Act
            var result = processor.ProcessCharacter(currentString, chr);

            //Assert
            Assert.AreEqual(char.MinValue, result);
        }

        [TestMethod]
        public void ChrIsMax_Continue()
        {
            //Set up
            var processor = new DuplicateCharacterRemover();
            var currentString = new StringBuilder("ABCD");
            var chr = char.MaxValue;

            //Act
            var result = processor.ProcessCharacter(currentString, chr);

            //Assert
            Assert.AreEqual(char.MaxValue, result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CurrentIsNull_ArgumentException()
        {
            //Set up
            var processor = new DuplicateCharacterRemover();
            var chr = char.MinValue;

            //Act
            var _ = processor.ProcessCharacter(null, chr);
        }
    }
}