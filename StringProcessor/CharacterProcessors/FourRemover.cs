using System.Text;
using StringProcessor.Enums;

namespace StringProcessor.CharacterProcessors
{
    public class FourRemover : ICharacterProcessor
    {
        public ProcessorResult ProcessCharacter(StringBuilder currentString, ref char chr)
        {
            if (chr == '4')
            {
                chr = char.MinValue;
            }

            return ProcessorResult.Continue;
        }
    }
}
