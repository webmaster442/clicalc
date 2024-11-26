using CliCalc.Interfaces;

namespace CliCalc.Engine;
internal static class HashmarkCommandLoader
{
    public static Dictionary<string, IHashMarkCommand> GetCommands()
    {
       var types = typeof(HashmarkCommandLoader).Assembly.GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IHashMarkCommand)) && !t.IsAbstract && !t.IsInterface)
            .ToArray();

        var commands = new Dictionary<string, IHashMarkCommand>();

        foreach (var type in types)
        {
            if (Activator.CreateInstance(type) is IHashMarkCommand instance)
            {
                var name = instance.Name;
                if (!name.StartsWith('#'))
                {
                    name = $"#{name}";
                }
                commands.Add(name, instance);
            }
        }

        return commands;
    }
}
