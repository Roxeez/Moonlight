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
    public class SkillParser : Parser
    {
        private readonly IRepository<SkillDto> _skillRepository;

        internal SkillParser(ILogger logger, IRepository<SkillDto> skillRepository) : base(logger) => _skillRepository = skillRepository;

        public override void Parse(ParseConfiguration configuration, string directory)
        {
            Logger.Info("SKILLS");

            string skillDataPath = Path.Combine(directory, configuration.Data.Skill).Replace(@"\", "/");
            if (!File.Exists(skillDataPath))
            {
                Logger.Error($"Can't found {skillDataPath} file");
                return;
            }

            Logger.Info($"Loading skills from {skillDataPath}");

            FileContent content = TextReader.FromFile(skillDataPath)
                .SkipEmptyLines()
                .SkipCommentedLines("#")
                .TrimLines()
                .SplitLineContent('\t')
                .GetContent();

            IEnumerable<FileRegion> regions = content.GetRegions("VNUM");

            Logger.Info("Creating all skills");
            var skills = new List<SkillDto>();
            foreach (FileRegion region in regions)
            {
                FileLine firstLine = region.GetLine(x => x.StartWith("VNUM"));
                FileLine secondLine = region.GetLine(x => x.StartWith("NAME"));

                int vnum = firstLine.GetValue<int>(1);
                string name = secondLine.GetValue(1);

                skills.Add(new SkillDto
                {
                    Id = vnum,
                    NameKey = name
                });
            }

            Logger.Info("Saving skills to database");
            IEnumerable<SkillDto> result = _skillRepository.SaveAll(skills);
            Logger.Info($"{result.Count()} skills successfully parsed");
        }
    }
}