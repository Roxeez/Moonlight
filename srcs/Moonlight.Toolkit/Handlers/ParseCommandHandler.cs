using System;
using System.Collections.Generic;
using System.IO;
using Moonlight.Core.Logging;
using Moonlight.Toolkit.Commands;
using Moonlight.Toolkit.Parsing;
using Newtonsoft.Json;

namespace Moonlight.Toolkit.Handlers
{
    internal class ParseCommandHandler : CommandHandler<ParseCommand>
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<Parsing.Parser> _parsers;

        public ParseCommandHandler(ILogger logger, IEnumerable<Parsing.Parser> parsers)
        {
            _logger = logger;
            _parsers = parsers;
        }

        protected override bool Handle(ParseCommand command)
        {
            if (!File.Exists(command.InputPath))
            {
                _logger.Error("Incorrect input path");
                return false;
            }

            string path = Path.GetDirectoryName(command.InputPath);
            if (path == null)
            {
                _logger.Error("Can't get parent directory");
                return false;
            }

            _logger.Info("===================================================");
            _logger.Info("                      PARSING");
            _logger.Info("===================================================");
            _logger.Info("SETUP");
            _logger.Info($"Deserializing {Path.GetFileName(command.InputPath)}...");
            ParseConfiguration configuration = JsonConvert.DeserializeObject<ParseConfiguration>(File.ReadAllText(command.InputPath));
            if (configuration == null)
            {
                _logger.Error($"Failed to deserialize {Path.GetFileName(command.InputPath)}");
                return false;
            }

            DateTime start = DateTime.Now;
            foreach (Parsing.Parser parser in _parsers)
            {
                _logger.Info("---------------------------------------------------");
                parser.Parse(configuration, path);
            }

            _logger.Info("===================================================");
            TimeSpan timeElapsed = DateTime.Now - start;
            _logger.Info($"Parsing finished in {(timeElapsed.TotalMinutes >= 1 ? timeElapsed.Minutes + "mn " : string.Empty)}{timeElapsed.Seconds}s");
            _logger.Info("===================================================");
            return true;
        }
    }
}