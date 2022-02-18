using System.Text;
using StringProcessor.CharacterProcessors;
using StringProcessor.Enums;

namespace StringProcessor
{
    public class Processor
    {
        #region Constructors

        public Processor(params ICharacterProcessor[] characterProcessors)
        {
            if (characterProcessors == null)
            {
                throw new ArgumentException(nameof(characterProcessors));
            }

            CharacterProcessors = characterProcessors
                .Where(p => p != null)
                .ToArray();
        }

        public Processor(IEnumerable<ICharacterProcessor> characterProcessors) : 
            this(characterProcessors?.ToArray())
        {
        }

        #endregion

        #region Properties
        public ICharacterProcessor[] CharacterProcessors { get; private set; }
        #endregion

        #region Methods
        public string Process(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(nameof(value));
            }
            
            using (var sr = new StringReader(value))
            {
                return Process(sr);
            }
        }

        public string Process(StringReader sr)
        {
            if (sr == null)
            {
                throw new ArgumentException(nameof(sr));
            }

            var currentString = new StringBuilder();
            int chrVal;

            //While not 'end of string'
            while ((chrVal = sr.Read()) != -1)
            {
                var chr = (char)chrVal;

                var characterProcessorResult = CharacterProcessors.ProcessCharacter(currentString, ref chr);

                //If any character processor calls for a stop, stop processing the string
                if (characterProcessorResult == ProcessorResult.Stop)
                {
                    break;
                }

                if (chr != char.MinValue)
                {
                    currentString.Append(chr);
                }
            }

            var result = currentString.ToString();

            if (string.IsNullOrEmpty(result))
            {
                throw new InvalidOperationException("Result is a null or empty string");
            }
            return result;
        }
        
        #endregion
    }
}