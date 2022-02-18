using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StringProcessor.CharacterProcessors;
using StringProcessor.Enums;

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
            Assert.IsNotNull(processor.CharacterProcessors);
            Assert.AreEqual(0, processor.CharacterProcessors.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullListProcessors()
        {
            //Set up
            List<ICharacterProcessor> processors = null;

            //Act
            var _ = new Processor(processors);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_NullArrayProcessors()
        {
            //Set up
            ICharacterProcessor[] processors = null;

            //Act
            var _ = new Processor(processors);
        }

        [TestMethod]
        public void Constructor_EmptyListProcessors()
        {
            //Set up
            var processors = new List<ICharacterProcessor>();

            //Act
            var processor = new Processor(processors);

            //Assert
            Assert.IsNotNull(processor.CharacterProcessors);
            Assert.AreEqual(0, processor.CharacterProcessors.Length);
        }

        [TestMethod]
        public void Constructor_ListContainsNullProcessors_FilteredOut()
        {
            //Set up
            var chrProcessor = (new Mock<ICharacterProcessor>()).Object;
            var processors = new List<ICharacterProcessor> { chrProcessor, null, null };

            //Act
            var processor = new Processor(processors);

            //Assert
            Assert.IsNotNull(processor.CharacterProcessors);
            Assert.AreEqual(1, processor.CharacterProcessors.Length);
            Assert.AreEqual(chrProcessor, processor.CharacterProcessors.Single());
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
            var characterProcessorMock = new Mock<ICharacterProcessor>();
            characterProcessorMock
                .Setup(m => m.ProcessCharacter(It.IsAny<StringBuilder>(), ref It.Ref<char>.IsAny))
                .Returns(ProcessorResult.Continue);

            var processor = new Processor(characterProcessorMock.Object);

            var stringLength = 10;
            var startingString = new string('A', stringLength);

            //Act
            var result = processor.Process(startingString);

            //Assert
            characterProcessorMock.Verify(
                m => m.ProcessCharacter(It.IsAny<StringBuilder>(), ref It.Ref<char>.IsAny),
                Times.Exactly(stringLength)
            );
            Assert.AreEqual(new string('A', stringLength), result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Process_CharacterProcessorClears_EmptyExpectedException()
        {
            //Set up
            var characterProcessorMock = new Mock<ICharacterProcessor>();

            characterProcessorMock
                .Setup(m => m.ProcessCharacter(It.IsAny<StringBuilder>(), ref It.Ref<char>.IsAny))
                .Callback(delegate(StringBuilder currentString, ref char chr) { chr = char.MinValue; })
                .Returns(ProcessorResult.Continue);

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
                    m => m.ProcessCharacter(It.IsAny<StringBuilder>(), ref It.Ref<char>.IsAny),
                    Times.Exactly(stringLength)
                );
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Process_CharacterProcessorStops_EmptyExpectedException()
        {
            //Set up
            var characterProcessorMock = new Mock<ICharacterProcessor>();

            characterProcessorMock
                .Setup(m => m.ProcessCharacter(It.IsAny<StringBuilder>(), ref It.Ref<char>.IsAny))
                .Returns(ProcessorResult.Stop);

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
                    m => m.ProcessCharacter(It.IsAny<StringBuilder>(), ref It.Ref<char>.IsAny),
                    Times.Once
                );
            }
        }

        #endregion
    }
}
