using System.Text;
using StringProcessor.Interfaces;

namespace StringProcessor.CharacterReplacement
{
    /// <summary>
    /// <see cref="ICharacterReplacement"/> implementation that removes the character '_'
    /// </summary>
    public class UnderscoreRemover : ICharacterReplacement
    {
        /// <summary>
        /// Returns char.MinValue if the input chr = '_'
        /// </summary>
        public char ProcessCharacter(StringBuilder currentString, char chr)
        {
            if (chr == '_')
            {
                return char.MinValue;
            }

            return chr;
        }
    }
}
