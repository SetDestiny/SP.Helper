using System;
using SP.Helper.BLL.Managers;
using SP.Helper.BLL.Helpers;
using System.Linq;

namespace SP.Helper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.Write("Enter spWeb listName > ");
            var spSettings = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (spSettings.Length > 2)
            {
                foreach (var item in spSettings.Skip(2).ToArray())
                {
                    spSettings[1] += string.Format(" {0}", item);
                }
            }
            var commandStr = string.Empty;

            while (commandStr != "close")
            {
                Console.Write(string.Format("[{0}] [{1}] > ", spSettings[0], spSettings[1]));
                commandStr = Console.ReadLine();
                Spinner.Start(100);
                var response = CommandManager.ProcessCommand(spSettings[0], spSettings[1], commandStr);
                Spinner.Stop();

                if (response > 0)
                {
                    Console.WriteLine("Operation done successfully.");
                    Console.WriteLine(string.Format("{0} items was created.", response));
                }
                else
                {
                    Console.WriteLine("Unknown commnad...");
                }

            }
        }
    }
}
