using System.Text;
using StringProcessor.Interfaces;

namespace StringProcessor.CompleteConditions
{
    /// <summary>
    /// <see cref="ICompleteCondition"/> implementation that indicates processing
    /// is complete when the current string has a length of 15 characters.
    /// </summary>
    public class StopAt15Length : ICompleteCondition
    {
        /// <summary>
        /// Returns true when the currentString has a length of 15 characters
        /// </summary>
        /// <exception cref="ArgumentException">If currentString is null</exception>
        public bool IsComplete(StringBuilder currentString, char chr)
        {
            if (currentString == null)
            {
                throw new ArgumentException(nameof(currentString));
            }

            return currentString.Length == 15;
        }
    }
}
