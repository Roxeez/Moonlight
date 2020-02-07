using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moonlight.Core.Logging;
using Moonlight.Database.DAL;
using Moonlight.Database.Dto;
using Moonlight.Toolkit.Commands;
using Moonlight.Utility.Reader;
using TextReader = Moonlight.Utility.Reader.TextReader;

namespace Moonlight.Toolkit.Parsing
{
    public class MonsterParser : Parser
    {
        private readonly IRepository<MonsterDto> _monsterRepository;

        internal MonsterParser(ILogger logger, IRepository<MonsterDto> monsterRepository) : base(logger) => _monsterRepository = monsterRepository;

        public override void Parse(ParseConfiguration configuration, string directory)
        {
            Logger.Info("MONSTERS");

            string monsterDataPath = Path.Combine(directory, configuration.Data.Monster).Replace(@"\", "/");
            if (!File.Exists(monsterDataPath))
            {
                Logger.Error($"Can't found {monsterDataPath} file");
                return;
            }

            Logger.Info($"Loading monsters from {monsterDataPath}");

            FileContent content = TextReader.FromFile(monsterDataPath)
                .SkipEmptyLines()
                .SkipCommentedLines("#")
                .TrimLines()
                .SplitLineContent('\t')
                .GetContent();

            IEnumerable<FileRegion> regions = content.GetRegions("VNUM");

            Logger.Info("Creating all monsters");
            var monsters = new List<MonsterDto>();
            foreach (FileRegion region in regions)
            {
                FileLine vnumLine = region.GetLine(x => x.StartWith("VNUM"));
                FileLine nameLine = region.GetLine(x => x.StartWith("NAME"));
                FileLine levelLine = region.GetLine(x => x.StartWith("LEVEL"));

                int vnum = vnumLine.GetValue<int>(1);
                string name = nameLine.GetValue(1);
                int level = levelLine.GetValue<int>(1);

                monsters.Add(new MonsterDto
                {
                    Id = vnum,
                    NameKey = name,
                    Level = level
                });
            }

            Logger.Info("Saving monsters to database");
            IEnumerable<MonsterDto> result = _monsterRepository.SaveAll(monsters);
            Logger.Info($"{result.Count()} monsters successfully parsed");
        }
    }
}