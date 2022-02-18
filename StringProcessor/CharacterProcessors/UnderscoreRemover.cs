using System.Text;
using StringProcessor.Enums;

namespace StringProcessor.CharacterProcessors
{
    public class UnderscoreRemover : ICharacterProcessor
    {
        public ProcessorResult ProcessCharacter(StringBuilder currentString, ref char chr)
        {
            if (chr == '_')
            {
                chr = char.MinValue;
            }

            return ProcessorResult.Continue;
        }
    }
}
