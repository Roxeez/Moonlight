using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moonlight.Core.Enums;
using Moonlight.Core.Logging;
using Moonlight.Database.DAL;
using Moonlight.Database.Dto;
using Moonlight.Toolkit.Commands;
using Moonlight.Translation;
using Moonlight.Utility.Reader;
using TextReader = Moonlight.Utility.Reader.TextReader;

namespace Moonlight.Toolkit.Parsing
{
    public class LangParser : Parser
    {
        private readonly IStringRepository<TranslationDto> _translationRepository;

        internal LangParser(ILogger logger, IStringRepository<TranslationDto> translationRepository) : base(logger) => _translationRepository = translationRepository;

        public override void Parse(ParseConfiguration configuration, string directory)
        {
            Logger.Info("LANGS");

            string langDirectory = Path.Combine(directory, configuration.Lang).Replace(@"\", "/");
            if (!Directory.Exists(langDirectory))
            {
                Logger.Error($"Can't found {langDirectory} directory");
                return;
            }

            var translations = new List<TranslationDto>();
            string[] directories = Directory.GetDirectories(langDirectory);
            foreach (string specificLangDirectory in directories)
            {
                string languageName = Path.GetFileName(specificLangDirectory);
                if (languageName == null)
                {
                    continue;
                }

                if (!Enum.TryParse(languageName.ToUpper(), out Language language))
                {
                    Logger.Warn($"Undefined language {languageName}, skipping it.");
                    continue;
                }

                Logger.Info($"Creating {language} translations");
                foreach (string file in Directory.GetFiles(specificLangDirectory))
                {
                    string rootKeyName = Path.GetFileName(file);
                    if (!Enum.TryParse(rootKeyName.ToUpper(), out RootKey rootKey))
                    {
                        Logger.Warn($"Undefined root key {rootKeyName}, skipping it");
                        continue;
                    }

                    FileContent content = TextReader.FromFile(file)
                        .SkipCommentedLines("#")
                        .SkipEmptyLines()
                        .TrimLines()
                        .SplitLineContent('\t')
                        .GetContent();

                    foreach (FileLine line in content.Lines)
                    {
                        string key = line.GetFirstValue();
                        string value = line.GetLastValue();

                        translations.Add(new TranslationDto
                        {
                            Id = $"{language}:{rootKey}:{key}".ToLower(),
                            Value = value
                        });
                    }
                }
            }

            Logger.Info("Saving all translations to database (can take 1-2mn)");
            IEnumerable<TranslationDto> result = _translationRepository.SaveAll(translations);
            Logger.Info($"{result.Count()} translations successfully parsed");
        }
    }
}