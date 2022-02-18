using System.Text;

namespace StringProcessor.Interfaces
{
    /// <summary>
    /// Interface to apply character replacement rules
    /// </summary>
    public interface ICharacterReplacement
    {
        /// <summary>
        /// Process the given character
        /// </summary>
        /// <param name="currentString">Current result string from processor</param>
        /// <param name="chr">Character to be processed</param>
        /// <returns>Updated/Altered character</returns>

        char ProcessCharacter(StringBuilder currentString, char chr);
    }
}
