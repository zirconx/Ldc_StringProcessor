using System.Text;
using StringProcessor.CharacterProcessors;
using StringProcessor.Enums;

namespace StringProcessor
{
    internal static class Extensions
    {
        public static ProcessorResult ProcessCharacter(
            this ICharacterProcessor[] characterProcessors,
            StringBuilder currentString, 
            ref char chr
        )
        {
            foreach (var characterProcessor in characterProcessors)
            {
                var res = characterProcessor.ProcessCharacter(currentString, ref chr);
                if (res == ProcessorResult.Stop)
                {
                    return res;
                }
            }

            return ProcessorResult.Continue;
        }
    }
}
