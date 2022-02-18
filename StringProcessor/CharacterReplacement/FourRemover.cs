using System.Text;
using StringProcessor.Interfaces;

namespace StringProcessor.CharacterReplacement
{
    /// <summary>
    /// <see cref="ICharacterReplacement"/> implementation that removes the character '4'
    /// </summary>
    public class FourRemover : ICharacterReplacement
    {
        /// <summary>
        /// Returns char.MinValue if the input chr = '4'
        /// </summary>
        public char ProcessCharacter(StringBuilder currentString, char chr)
        {
            if (chr == '4')
            {
                return char.MinValue;
            }

            return chr;
        }
    }
}
