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
    public class ItemParser : Parser
    {
        private readonly IRepository<ItemDto> _itemRepository;

        internal ItemParser(ILogger logger, IRepository<ItemDto> itemRepository) : base(logger) => _itemRepository = itemRepository;

        public override void Parse(ParseConfiguration configuration, string directory)
        {
            Logger.Info("ITEMS");

            string itemDataPath = Path.Combine(directory, configuration.Data.Item).Replace(@"\", "/");
            if (!File.Exists(itemDataPath))
            {
                Logger.Error($"Can't found {itemDataPath} file");
                return;
            }

            Logger.Info($"Loading items from {itemDataPath}");
            FileContent content = TextReader.FromFile(itemDataPath)
                .SkipEmptyLines()
                .SkipCommentedLines("#")
                .TrimLines()
                .SplitLineContent('\t')
                .GetContent();

            IEnumerable<FileRegion> regions = content.GetRegions("VNUM");

            Logger.Info("Creating all items");
            var items = new List<ItemDto>();
            foreach (FileRegion region in regions)
            {
                FileLine firstLine = region.GetLine(x => x.StartWith("VNUM"));
                FileLine secondLine = region.GetLine(x => x.StartWith("NAME"));

                int vnum = firstLine.GetValue<int>(1);
                string name = secondLine.GetValue(1);

                items.Add(new ItemDto
                {
                    Id = vnum,
                    NameKey = name
                });
            }

            Logger.Info("Saving items to database");
            IEnumerable<ItemDto> result = _itemRepository.SaveAll(items);
            Logger.Info($"{result.Count()} items successfully parsed");
        }
    }
}