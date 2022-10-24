namespace Lab123.Lab2Task
{
    public static class Lab2
    {
        public static void StartNewDialogForLab2()
        {
            Console.WriteLine("---------- Lab2 ----------");
            Console.WriteLine("----- Yana Lazorenko -----");

            var cancellationRequested = default(bool);
            while (!cancellationRequested)
            {
                Console.WriteLine(CommunicationMessagesLab2.InputPathToFileMessage);
                var pathToInputFile = Console.ReadLine();

                if (string.IsNullOrEmpty(pathToInputFile))
                {
                    Console.WriteLine(ExceptionMessagesLab2.InvalidPathToFileMessage);
                    Console.WriteLine(CommunicationMessagesLab2.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                var linesOfNumbersFromFile = File.ReadAllLines(pathToInputFile);
                int inputNumberOfStairs;
                try
                {
                    inputNumberOfStairs = int.Parse(linesOfNumbersFromFile[0]);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine(ExceptionMessagesLab2.InvalidPathToFileMessage);
                    Console.WriteLine(CommunicationMessagesLab2.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine(ExceptionMessagesLab2.NotNumberInputMessage);
                    Console.WriteLine(CommunicationMessagesLab2.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }
                catch (OverflowException)
                {
                    Console.WriteLine(ExceptionMessagesLab2.ExceedingLimitsNumberMessage);
                    Console.WriteLine(CommunicationMessagesLab2.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                if (!ValidateLimits(inputNumberOfStairs))
                {
                    Console.WriteLine(ExceptionMessagesLab2.ExceedingLimitsNumberMessage);
                    Console.WriteLine(CommunicationMessagesLab2.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                var numbersWrittenOnStairs = linesOfNumbersFromFile[1].Split(' ')
                    .Where(x => !string.IsNullOrWhiteSpace(x) && int.TryParse(x, out _))
                    .Select(x => int.Parse(x));

                if (numbersWrittenOnStairs.Count() != inputNumberOfStairs)
                {
                    Console.WriteLine(ExceptionMessagesLab2.DifferentNumberOfStairsMessage);
                    Console.WriteLine(CommunicationMessagesLab2.TryAgainMessage);
                    cancellationRequested = Console.ReadLine() != "1";
                    continue;
                }

                CalculateSteps(inputNumberOfStairs, numbersWrittenOnStairs);

                Console.WriteLine("\nFile is prepared.");
                break;
            }
        }
        private static bool ValidateLimits(int inputNumber)
        {
            return inputNumber > 1 && inputNumber < 101;
        }

        private static void CalculateSteps(int inputNumber, IEnumerable<int> numbersWrittenOnStairs)
        {
            var numbersWrittenOnStairsList = numbersWrittenOnStairs.ToList();

            // Let`s calculate from the end because we must step on the last stair
            // and there`s no constraints on the order of calculations in the task.
            numbersWrittenOnStairsList.Reverse();
            var resultList = new List<int>(inputNumber) { inputNumber };
            var sum = numbersWrittenOnStairsList[0];

            var biggerNumberIndex = 0;
            for (var i = 1; i < inputNumber - 1; i++)
            {
                biggerNumberIndex = i;
                if (numbersWrittenOnStairsList[i] < numbersWrittenOnStairsList[i + 1])
                {
                    biggerNumberIndex = i + 1;
                    i++;
                }
             
                resultList.Add(inputNumber - biggerNumberIndex);
                sum += numbersWrittenOnStairsList[biggerNumberIndex];
            }

            if (biggerNumberIndex == inputNumber - 2 
                && numbersWrittenOnStairsList[inputNumber - 1] > 0)
            {
                resultList.Add(1);
                sum += numbersWrittenOnStairsList[inputNumber - 1];
            }
            
            PrepareResults("C:\\Users\\decce\\Desktop\\PC\\4 course\\Crossplatforms\\Lab1\\Lab2Task\\OUTPUT.txt", sum, resultList);
        }

        private static void PrepareResults(string filename, int sum, List<int> stairs)
        {
            stairs.Reverse();

            using (StreamWriter sw = !File.Exists(filename)
                ? File.CreateText(filename)
                : File.AppendText(filename))
            {
                sw.WriteLine(sum.ToString());
                foreach (var stair in stairs)
                {
                    sw.Write(stair.ToString() + " ");
                }
            }
        }
    }
}
