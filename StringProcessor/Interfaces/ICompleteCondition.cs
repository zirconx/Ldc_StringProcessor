using System.Text;

namespace StringProcessor.Interfaces
{
    /// <summary>
    /// Interface to stop character processing
    /// </summary>
    public interface ICompleteCondition
    {
        /// <summary>
        /// Process the given character
        /// </summary>
        /// <param name="currentString">Current result string from processor</param>
        /// <param name="chr">Next character to be processed</param>
        /// <returns>Whether to stop processing further characters</returns>
        bool IsComplete(StringBuilder currentString, char chr);
    }
}
