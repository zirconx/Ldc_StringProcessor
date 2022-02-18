using System.Text;
using StringProcessor.Enums;

namespace StringProcessor.CharacterProcessors
{
    public class StopAt15Length : ICharacterProcessor
    {
        public ProcessorResult ProcessCharacter(StringBuilder currentString, ref char chr)
        {
            if (currentString == null)
            {
                throw new ArgumentException(nameof(currentString));
            }

            if (currentString.Length == 15)
            {
                return ProcessorResult.Stop;
            }

            return ProcessorResult.Continue;
        }
    }
}
