using System.Text;
using StringProcessor.Interfaces;

namespace StringProcessor.CharacterReplacement
{
    /// <summary>
    /// <see cref="ICharacterReplacement"/> implementation that replaces '$' with '£'
    /// </summary>
    public class DollarToPound : ICharacterReplacement
    {
        /// <summary>
        /// Replace '$' wth '£'
        /// </summary>
        public char ProcessCharacter(StringBuilder currentString, char chr)
        {
            if (chr == '$')
            {
                return '£';
            }

            return chr;
        }
    }
}
