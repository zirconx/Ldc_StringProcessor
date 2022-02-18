using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringProcessor.CharacterProcessors;
using StringProcessor.Enums;

namespace StringProcessor.Test.Unit.CharacterProcessors
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
            var result = processor.ProcessCharacter(currentString, ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual(char.MinValue, chr);
        }


        [TestMethod]
        public void ChrIsNotMatch_NoChange()
        {
            //Set up
            var processor = new DuplicateCharacterRemover();
            var currentString = new StringBuilder("ABCD");
            var chr = 'Z';

            //Act
            var result = processor.ProcessCharacter(currentString, ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual('Z', chr);
        }

        [TestMethod]
        public void ChrIsMin_Continue()
        {
            //Set up
            var processor = new DuplicateCharacterRemover();
            var currentString = new StringBuilder("ABCD");
            var chr = char.MinValue;

            //Act
            var result = processor.ProcessCharacter(currentString, ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual(char.MinValue, chr);
        }

        [TestMethod]
        public void ChrIsMax_Continue()
        {
            //Set up
            var processor = new DuplicateCharacterRemover();
            var currentString = new StringBuilder("ABCD");
            var chr = char.MaxValue;

            //Act
            var result = processor.ProcessCharacter(currentString, ref chr);

            //Assert
            Assert.AreEqual(ProcessorResult.Continue, result);
            Assert.AreEqual(char.MaxValue, chr);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CurrentIsNull_ArgumentException()
        {
            //Set up
            var processor = new DuplicateCharacterRemover();
            var chr = char.MinValue;

            //Act
            var _ = processor.ProcessCharacter(null, ref chr);
        }
    }
}