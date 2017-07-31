using SP.Helper.BLL.Models;
using System;
using System.Linq;

namespace SP.Helper.BLL.Managers
{
    public class CommandManager
    {
        public static int ProcessCommand(string spUrl, string spList, string commandStr)
        {
            var command = ParseCommand(commandStr);
            switch (command.CommandName)
            {
                case "CreateTestData":
                    ListItemManager listManager = new ListItemManager();
                    return listManager.CreateTestData(spUrl, spList, command.Params[0], Convert.ToInt32(command.Params[1]));
                default:
                    return -1;
            }
        }

        private static CommandEntity ParseCommand(string commandStr)
        {
            if (commandStr != null && commandStr.Length > 0)
            {
                var commandParams = commandStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                CommandEntity command = new CommandEntity();
                command.CommandName = commandParams[0];
                command.Params = commandParams.Skip(1).ToArray();
                return command;
            }
            return null;
        }
    }
}