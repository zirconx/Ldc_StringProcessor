using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringProcessor.CompleteConditions;

namespace StringProcessor.Test.Unit.CompleteConditions
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class StopAt15LengthTests
    {
        [TestMethod]
        public void LengthIs15_StopResult()
        {
            //Set up
            var processor = new StopAt15Length();
            var current = new StringBuilder(new string('A', 15));
            var chr = char.MinValue;

            //Act
            var result = processor.IsComplete(current, chr);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LengthUnder15_ContinueResult()
        {
            //Set up
            var processor = new StopAt15Length();
            var current = new StringBuilder(new string('A', 14));
            var chr = char.MinValue;

            //Act
            var result = processor.IsComplete(current, chr);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LengthZero_ContinueResult()
        {
            //Set up
            var processor = new StopAt15Length();
            var current = new StringBuilder();
            var chr = char.MinValue;

            //Act
            var result = processor.IsComplete(current, chr);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CurrentIsNull_ArgumentException()
        {
            //Set up
            var processor = new StopAt15Length();
            var chr = char.MinValue;

            //Act
            var _ = processor.IsComplete(null, chr);
        }
    }
}