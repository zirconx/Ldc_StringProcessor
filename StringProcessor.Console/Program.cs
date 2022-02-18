using StringProcessor.CharacterReplacement;
using StringProcessor.CompleteConditions;
using StringProcessor.Interfaces;

namespace StringProcessor.Console
{
    /// <summary>
    /// Simple console invoker of the StringProcessor, processing the string from the exercise.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            const string inputString = "AAAc91%cWwWkLq$1ci3_848v3d__K";
            System.Console.WriteLine($"Processing {inputString}");
            var processor = CreateProcessor();

            var result = processor.Process(inputString);
            System.Console.WriteLine($"Result {result}");

            System.Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }

        private static Processor CreateProcessor()
        {
            return new Processor(CreateCharacterReplacements(), CreateCompleteConditions());
        }

        private static IEnumerable<ICharacterReplacement> CreateCharacterReplacements()
        {
            yield return new DollarToPound();
            yield return new DuplicateCharacterRemover();
            yield return new FourRemover();
            yield return new UnderscoreRemover();
        }

        private static IEnumerable<ICompleteCondition> CreateCompleteConditions()
        {
            yield return new StopAt15Length();
        }
    }
}