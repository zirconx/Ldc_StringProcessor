using System.Text;
using StringProcessor.Interfaces;

namespace StringProcessor.CharacterReplacement
{
    /// <summary>
    /// <see cref="ICharacterReplacement"/> implementation that removed duplicate characters
    /// </summary>
    public class DuplicateCharacterRemover : ICharacterReplacement
    {
        /// <summary>
        /// If the last character in the currentString matches chr, returns char.MinValue
        /// </summary>
        /// <exception cref="ArgumentException">If currentString is null</exception>
        public char ProcessCharacter(StringBuilder currentString, char chr)
        {
            if (currentString == null)
            {
                throw new ArgumentException(nameof(currentString));
            }

            if (currentString.Length > 0 && currentString[^1] == chr)
            {
                return char.MinValue;
            }

            return chr;
        }
    }
}
