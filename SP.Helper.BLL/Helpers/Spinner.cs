using System;

namespace SP.Helper.BLL.Helpers
{
    public static class Spinner
    {
        private static System.ComponentModel.BackgroundWorker spinner = initialiseBackgroundWorker();
        private static int spinnerPosition = 25;
        private static int spinWait = 25;
        private static bool isRunning;

        public static bool IsRunning { get { return isRunning; } }

        private static System.ComponentModel.BackgroundWorker initialiseBackgroundWorker()
        {

            System.ComponentModel.BackgroundWorker obj = new System.ComponentModel.BackgroundWorker();
            obj.WorkerSupportsCancellation = true;
            obj.DoWork += delegate
            {
                spinnerPosition = Console.CursorLeft;
                while (!obj.CancellationPending)
                {
                    char[] spinChars = new char[] { '|', '/', '-', '\\' };
                    foreach (char spinChar in spinChars)
                    {
                        Console.CursorLeft = spinnerPosition;
                        Console.Write("In progress, please wait... " + spinChar);
                        System.Threading.Thread.Sleep(spinWait);
                    }
                }
            };
            return obj;
        }

        public static void Start(int spinWait)
        {
            isRunning = true;
            Spinner.spinWait = spinWait;
            if (!spinner.IsBusy)
                spinner.RunWorkerAsync();
            else throw new InvalidOperationException("Cannot start spinner whilst spinner is already running");
        }

        public static void Start() { Start(25); }

        public static void Stop()
        {
            spinner.CancelAsync();
            while (spinner.IsBusy) System.Threading.Thread.Sleep(100);
            Console.CursorLeft = spinnerPosition;
            isRunning = false;
            ClearCurrentConsoleLine();
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
