using System.Text;
using StringProcessor.Enums;

namespace StringProcessor.CharacterProcessors
{
    public class DollarToPound : ICharacterProcessor
    {
        public ProcessorResult ProcessCharacter(StringBuilder currentString, ref char chr)
        {
            if (chr == '$')
            {
                chr = '£';
            }

            return ProcessorResult.Continue;
        }
    }
}
