using CommandLine;
using Newtonsoft.Json;

namespace Moonlight.Toolkit.Commands
{
    [Verb("parse", HelpText = "Command used for parsing values to db")]
    public class ParseCommand : ICommand
    {
        [Option('i', "input", HelpText = "Path to parse.json files", Required = true)]
        public string InputPath { get; set; }
    }

    public class ParseConfiguration
    {
        [JsonRequired]
        public string Maps { get; set; }

        [JsonRequired]
        public string Lang { get; set; }

        [JsonRequired]
        public ParseData Data { get; set; }
    }

    public class ParseData
    {
        [JsonRequired]
        public string Item { get; set; }

        [JsonRequired]
        public string Monster { get; set; }

        [JsonRequired]
        public string Skill { get; set; }

        [JsonRequired]
        public string Map { get; set; }
    }
}