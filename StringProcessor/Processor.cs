using System.Text;
using StringProcessor.Interfaces;

namespace StringProcessor
{
    /// <summary>
    /// Processes strings character by character, given a collection of replacement rules and complete conditions
    /// </summary>
    public class Processor
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// No <see cref="ICharacterReplacement"/> or <see cref="ICompleteCondition"/> will be used in processing.
        /// This will result in the output string being the same as the input string.
        /// </summary>
        public Processor(){}

        /// <summary>
        /// Only apply <see cref="ICharacterReplacement"/> rules to the string processing
        /// </summary>
        public Processor(params ICharacterReplacement[] characterReplacements) : this(characterReplacements, new ICompleteCondition[]{}) { }

        /// <summary>
        /// Only apply <see cref="ICompleteCondition"/> rules to the string processing
        /// </summary>
        public Processor(params ICompleteCondition[] completeConditions) : this(new ICharacterReplacement[]{}, completeConditions) { }

        /// <summary>
        /// Apply a collection of <see cref="ICharacterReplacement"/> and a collection of <see cref="ICompleteCondition"/> to string processing
        /// </summary>
        /// <exception cref="ArgumentException">If either argument is null</exception>
        public Processor(
            IEnumerable<ICharacterReplacement> characterReplacements,
            IEnumerable<ICompleteCondition> completeConditions
        )
        {
            if (characterReplacements == null)
            {
                throw new ArgumentException(nameof(characterReplacements));
            }
            if (completeConditions == null)
            {
                throw new ArgumentException(nameof(completeConditions));
            }

            CharacterReplacements = characterReplacements
                .Where(p => p != null)
                .ToArray();

            CompleteConditions = completeConditions
                .Where(p => p != null)
                .ToArray();
        }

        #endregion

        #region Properties

        public ICharacterReplacement[] CharacterReplacements { get; private set; } = { };
        public ICompleteCondition[] CompleteConditions { get; private set; } = { };

        #endregion

        #region Methods

        /// <summary>
        /// Processes the given <see cref="string"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If value is null or empty</exception>
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

        /// <summary>
        /// Processes the given <see cref="StringReader"/>
        /// </summary>
        /// <param name="sr"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If sr is null</exception>
        /// <exception cref="InvalidOperationException">If the resulting string is null or empty</exception>
        public string Process(StringReader sr)
        {
            if (sr == null)
            {
                throw new ArgumentException(nameof(sr));
            }

            //If there are no character replacement rules, or complete conditions
            //the result will be the input string. Shortcut and return it immediately.
            if (!CharacterReplacements.Any() && !CompleteConditions.Any())
            {
                return sr.ReadToEnd();
            }

            var currentString = new StringBuilder();
            int chrVal;

            //While not 'end of string'
            while ((chrVal = sr.Read()) != -1)
            {
                var chr = (char)chrVal;

                if (CompleteConditions.IsComplete(currentString, chr))
                {
                    break;
                }
                
                chr = CharacterReplacements.ProcessCharacter(currentString, chr);
                if (chr == char.MinValue)
                {
                    continue;
                }

                currentString.Append(chr);
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