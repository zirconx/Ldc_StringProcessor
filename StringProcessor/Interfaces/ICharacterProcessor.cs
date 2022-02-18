using System.Text;
using StringProcessor.Enums;

namespace StringProcessor.CharacterProcessors
{
    public interface ICharacterProcessor
    {
        /// <summary>
        /// Process the given character
        /// </summary>
        /// <param name="currentString">Current result string from processor</param>
        /// <param name="chr">Character to be processed</param>
        /// <returns>Whether processing should continue or stop</returns>
        ProcessorResult ProcessCharacter(StringBuilder currentString, ref char chr);
    }
}
