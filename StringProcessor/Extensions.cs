using System.Text;
using StringProcessor.Interfaces;

namespace StringProcessor
{
    internal static class Extensions
    {
        public static char ProcessCharacter(
            this ICharacterReplacement[] characterProcessors,
            StringBuilder currentString, 
            char chr
        )
        {
            foreach (var characterProcessor in characterProcessors)
            {
                chr = characterProcessor.ProcessCharacter(currentString, chr);
                if (chr == char.MinValue)
                {
                    break;
                }
            }

            return chr;
        }

        public static bool IsComplete(
            this ICompleteCondition[] characterProcessors,
            StringBuilder currentString,
            char chr
        )
        {
            return characterProcessors.Any(p => p.IsComplete(currentString, chr));
        }
    }
}
