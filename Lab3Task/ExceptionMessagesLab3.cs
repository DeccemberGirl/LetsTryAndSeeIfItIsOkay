namespace CrossplatformsTasks.Lab3Task
{
    public static class ExceptionMessagesLab3
    {
        public const string InvalidPathToFileMessage = "\nYou specified the wrong path to your file with the input data or the file does not exist.";
        public const string NotNumberInputMessage = "\nInput value in the file was not a number.";
        public const string ExceedingLimitsNumberMessage = "\nInput value for {0} in the file exceeds the limits: only numbers from 1 to {1} are allowed.";
        public const string MatrixCannotBeBuiltMessage = "\nMatrix cannot be built - some of the values in the file are not numbers in (0; 1).";
    }
}
