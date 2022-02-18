using System.Text;
using StringProcessor.Enums;

namespace StringProcessor.CharacterProcessors
{
    public class DuplicateCharacterRemover : ICharacterProcessor
    {
        public ProcessorResult ProcessCharacter(StringBuilder currentString, ref char chr)
        {
            if (currentString == null)
            {
                throw new ArgumentException(nameof(currentString));
            }

            if (currentString.Length > 0 && currentString[^1] == chr)
            {
                chr = char.MinValue;
            }

            return ProcessorResult.Continue;
        }
    }
}
