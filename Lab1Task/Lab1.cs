using System.Numerics;

namespace CrossplatformsTasks.Lab1Task
{
    public static class Lab1
    {
        public static void StartNewDialogForLab1()
        {
            Console.WriteLine("---------- Lab1 ----------");
            Console.WriteLine("----- Yana Lazorenko -----");

            var cancellationRequested = default(bool);
            while (!cancellationRequested)
            {
                Console.WriteLine(CommunicationMessagesLab1.InputPathToFileMessage);
                var pathToInputFile = Console.ReadLine();

                if (string.IsNullOrEmpty(pathToInputFile))
                {
                    Console.WriteLine(ExceptionMessagesLab1.InvalidPathToFileMessage);
                    Console.WriteLine(CommunicationMessagesLab1.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                int inputNumber;
                try
                {
                    inputNumber = int.Parse(File.ReadAllText(pathToInputFile));
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine(ExceptionMessagesLab1.InvalidPathToFileMessage);
                    Console.WriteLine(CommunicationMessagesLab1.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine(ExceptionMessagesLab1.NotNumberInputMessage);
                    Console.WriteLine(CommunicationMessagesLab1.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }
                catch (OverflowException)
                {
                    Console.WriteLine(ExceptionMessagesLab1.ExceedingLimitsNumberMessage);
                    Console.WriteLine(CommunicationMessagesLab1.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                if (!ValidateLimits(inputNumber))
                {
                    Console.WriteLine(ExceptionMessagesLab1.ExceedingLimitsNumberMessage);
                    Console.WriteLine(CommunicationMessagesLab1.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                var numberOfPossibleRoadBuildings = CalculateRoadBuildingsBetweenCentres(inputNumber);

                Console.WriteLine($"\nNumber of possible road buildings between {inputNumber} centres is {numberOfPossibleRoadBuildings}.");
                break;
            }
        }

        private static bool ValidateLimits(int inputNumber)
        {
            return inputNumber > 1 && inputNumber < 101;
        }

        private static BigInteger CalculateRoadBuildingsBetweenCentres(int inputNumber)
        {
            return BigInteger.Pow(3, inputNumber * (inputNumber - 1) / 2);
        }
    }
}
