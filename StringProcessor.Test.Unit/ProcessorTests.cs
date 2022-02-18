using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StringProcessor.Interfaces;

// ReSharper disable CollectionNeverUpdated.Local
// ReSharper disable UnusedParameter.Local
#pragma warning disable CS8620
#pragma warning disable CS8625
#pragma warning disable CS8600
#pragma warning disable CS8604

namespace StringProcessor.Test.Unit
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProcessorTests
    {
        #region Constructor tests

        [TestMethod]
        public void Constructor_EmptyNoProcessors()
        {
            //Act
            var processor = new Processor();

            //Assert
            Assert.IsNotNull(processor.CharacterReplacements);
            Assert.AreEqual(0, processor.CharacterReplacements.Length);


            Assert.IsNotNull(processor.CompleteConditions);
            Assert.AreEqual(0, processor.CompleteConditions.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullLists_ArgumentException()
        {
            //Set up
            ICharacterReplacement[] processors = null;
            ICompleteCondition[] complete = null;

            //Act
            var _ = new Processor(processors, complete);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullProcessorList_ArgumentException()
        {
            //Set up
            ICharacterReplacement[] processors = null;

            //Act
            var _ = new Processor(processors);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullCompleteLists_ArgumentException()
        {
            //Set up
            ICompleteCondition[] complete = null;

            //Act
            var _ = new Processor(complete);
        }
        
        [TestMethod]
        public void Constructor_EmptyListProcessors()
        {
            //Set up
            var processors = new List<ICharacterReplacement>();
            var complete = new List<ICompleteCondition>();

            //Act
            var processor = new Processor(processors, complete);

            //Assert
            Assert.IsNotNull(processor.CharacterReplacements);
            Assert.AreEqual(0, processor.CharacterReplacements.Length);

            Assert.IsNotNull(processor.CompleteConditions);
            Assert.AreEqual(0, processor.CompleteConditions.Length);
        }

        [TestMethod]
        public void Constructor_ProcessorListContainsNullProcessors_FilteredOut()
        {
            //Set up
            var characterProcessor = (new Mock<ICharacterReplacement>()).Object;
            var processors = new [] { characterProcessor, null, null };

            //Act
            var processor = new Processor(processors);

            //Assert
            Assert.IsNotNull(processor.CharacterReplacements);
            Assert.AreEqual(1, processor.CharacterReplacements.Length);
            Assert.AreEqual(characterProcessor, processor.CharacterReplacements.Single());
        }

        [TestMethod]
        public void Constructor_CompleteListContainsNullProcessors_FilteredOut()
        {
            //Set up
            var completeCondition = (new Mock<ICompleteCondition>()).Object;
            var completeConditions = new [] { completeCondition, null, null };

            //Act
            var processor = new Processor(completeConditions);

            //Assert
            Assert.IsNotNull(processor.CompleteConditions);
            Assert.AreEqual(1, processor.CompleteConditions.Length);
            Assert.AreEqual(completeCondition, processor.CompleteConditions.Single());
        }

        #endregion

        #region Process tests

        #region Argument tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_StringNull_ExpectedArgumentException()
        {
            //Set up
            var processor = new Processor();

            //Act
            var _ = processor.Process((string)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_StringEmpty_ExpectedArgumentException()
        {
            //Set up
            var processor = new Processor();

            //Act
            var _ = processor.Process(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Process_StringReaderNull_ExpectedArgumentException()
        {
            //Set up
            var processor = new Processor();

            //Act
            var _ = processor.Process((StringReader)null);
        }
        
        #endregion

        [TestMethod]
        public void Process_CallsCharacterProcessors()
        {
            //Set up
            var characterProcessorMock = new Mock<ICharacterReplacement>();
            characterProcessorMock
                .Setup(m => m.ProcessCharacter(It.IsAny<StringBuilder>(), It.IsAny<char>()))
                .Returns((StringBuilder sb, char c) => c);

            var processor = new Processor(characterProcessorMock.Object);

            var stringLength = 10;
            var startingString = new string('A', stringLength);

            //Act
            var result = processor.Process(startingString);

            //Assert
            characterProcessorMock.Verify(
                m => m.ProcessCharacter(It.IsAny<StringBuilder>(), It.IsAny<char>()),
                Times.Exactly(stringLength)
            );
            Assert.AreEqual(new string('A', stringLength), result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Process_CharacterProcessorClears_EmptyExpectedException()
        {
            //Set up
            var characterProcessorMock = new Mock<ICharacterReplacement>();

            characterProcessorMock
                .Setup(m => m.ProcessCharacter(It.IsAny<StringBuilder>(), It.IsAny<char>()))
                .Returns((StringBuilder sb, char c) => char.MinValue);

            var processor = new Processor(characterProcessorMock.Object);

            var stringLength = 10;
            var startingString = new string('A', stringLength);

            try
            {
                //Act
                var _ = processor.Process(startingString);
            }
            finally
            {
                //Assert
                characterProcessorMock.Verify(
                    m => m.ProcessCharacter(It.IsAny<StringBuilder>(), It.IsAny<char>()),
                    Times.Exactly(stringLength)
                );
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Process_CharacterProcessorStops_EmptyExpectedException()
        {
            //Set up
            var completeConditionMock = new Mock<ICompleteCondition>();

            completeConditionMock
                .Setup(m => m.IsComplete(It.IsAny<StringBuilder>(), It.IsAny<char>()))
                .Returns(true);

            var processor = new Processor(completeConditionMock.Object);

            var stringLength = 10;
            var startingString = new string('A', stringLength);

            try
            {
                //Act
                var _ = processor.Process(startingString);
            }
            finally
            {
                //Assert
                completeConditionMock.Verify(
                    m => m.IsComplete(It.IsAny<StringBuilder>(), It.IsAny<char>()),
                    Times.Once
                );
            }
        }

        #endregion
    }
}
