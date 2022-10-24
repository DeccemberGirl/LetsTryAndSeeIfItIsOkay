namespace Lab123.Lab3Task
{
    public static class Lab3
    {
        const int MaxNumberOfPeople = 100;

        public static void StartNewDialogForLab3()
        {
            Console.WriteLine("---------- Lab3 ----------");
            Console.WriteLine("----- Yana Lazorenko -----");

            var cancellationRequested = default(bool);
            while (!cancellationRequested)
            {
                Console.WriteLine(CommunicationMessagesLab3.InputPathToFileMessage);
                var pathToInputFile = Console.ReadLine();

                if (string.IsNullOrEmpty(pathToInputFile))
                {
                    Console.WriteLine(ExceptionMessagesLab3.InvalidPathToFileMessage);
                    Console.WriteLine(CommunicationMessagesLab3.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                var linesOfNumbersFromFile = File.ReadAllLines(pathToInputFile);
                int inputNumberOfPeople;
                int numberOfPerson;
                try
                {
                    var firstLineSplitted = linesOfNumbersFromFile[0].Split(" ");
                    inputNumberOfPeople = int.Parse(firstLineSplitted[0]);
                    numberOfPerson = int.Parse(firstLineSplitted[1]);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine(ExceptionMessagesLab3.InvalidPathToFileMessage);
                    Console.WriteLine(CommunicationMessagesLab3.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine(ExceptionMessagesLab3.NotNumberInputMessage);
                    Console.WriteLine(CommunicationMessagesLab3.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }
                catch (OverflowException)
                {
                    Console.WriteLine(ExceptionMessagesLab3.ExceedingLimitsNumberMessage);
                    Console.WriteLine(CommunicationMessagesLab3.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                if (!ValidateLimits(inputNumberOfPeople))
                {
                    Console.WriteLine(string.Format(ExceptionMessagesLab3.ExceedingLimitsNumberMessage,
                        "number of people in the club", MaxNumberOfPeople));
                    Console.WriteLine(CommunicationMessagesLab3.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                if (!ValidateLimits(numberOfPerson, inputNumberOfPeople))
                {
                    Console.WriteLine(string.Format(ExceptionMessagesLab3.ExceedingLimitsNumberMessage,
                        "number of a person", inputNumberOfPeople));
                    Console.WriteLine(CommunicationMessagesLab3.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                var matrix = new List<List<int>>(inputNumberOfPeople);
                try
                {
                    for (var i = 1; i < inputNumberOfPeople + 1; i++)
                    {
                        var oneLine = linesOfNumbersFromFile[i].Trim().Split(" ");
                        matrix.Add(new List<int>(inputNumberOfPeople));
                        for (var j = 0; j < inputNumberOfPeople; j++)
                        {
                            var number = int.Parse(oneLine[j]);
                            if (number != 0 && number != 1)
                            {
                                Console.WriteLine(ExceptionMessagesLab3.MatrixCannotBeBuiltMessage);
                                Console.WriteLine(CommunicationMessagesLab3.TryAgainMessage);
                                cancellationRequested = Console.ReadLine() != "1";
                                break;
                            }

                            matrix[i - 1].Add(number);
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(ExceptionMessagesLab3.MatrixCannotBeBuiltMessage);
                    Console.WriteLine(CommunicationMessagesLab3.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                var friendsList = new List<bool>(new bool[inputNumberOfPeople]);
                CalculateFriends(inputNumberOfPeople, numberOfPerson - 1, matrix, friendsList);
                var numberOfFriends = friendsList.Count(x => x == true) - 1;

                PrepareResults("C:\\Users\\decce\\Desktop\\PC\\4 course\\Crossplatforms\\Lab1\\Lab3Task\\OUTPUT.txt", numberOfFriends);

                Console.WriteLine("\nFile is prepared.");
                break;
            }
        }
        private static bool ValidateLimits(int inputNumber, int maxLimit = 100)
        {
            return inputNumber > 0 && inputNumber < maxLimit + 1;
        }

        private static void CalculateFriends(
            int inputNumberOfPeople,
            int numberOfPerson,
            List<List<int>> matrix,
            List<bool> friendsList)
        {
            if (numberOfPerson >= 0 && !friendsList[numberOfPerson])
            {
                friendsList[numberOfPerson] = true;
                for (var i = 0; i < inputNumberOfPeople; i++)
                {
                    if (matrix[numberOfPerson][i] == 1)
                    {
                        CalculateFriends(inputNumberOfPeople, i, matrix, friendsList);
                    }
                }
            }
        }

        private static void PrepareResults(string filename, int numberOfFriends)
        {
            using (StreamWriter sw = !File.Exists(filename)
                ? File.CreateText(filename)
                : File.AppendText(filename))
            {
                sw.WriteLine(numberOfFriends.ToString());
            }
        }
    }
}
