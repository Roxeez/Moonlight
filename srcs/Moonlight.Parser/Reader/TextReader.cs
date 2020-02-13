using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Moonlight.Parser.Reader
{
    public class TextReader
    {
        private readonly string[] _content;
        private readonly List<Predicate<string>> _skipConditions;
        private char _separator;

        private bool _trim;

        private TextReader(string[] content)
        {
            _content = content;
            _skipConditions = new List<Predicate<string>>();
        }

        public static TextReader FromString(string content) => new TextReader(content.Split('\r', '\n'));
        public static TextReader FromFile(string path) => new TextReader(File.ReadAllLines(path));

        public TextReader SkipEmptyLines() => SkipLines(string.IsNullOrEmpty);

        public TextReader SkipCommentedLines(string commentTag) => SkipLines(x => x.StartsWith(commentTag));

        public TextReader TrimLines()
        {
            _trim = true;
            return this;
        }

        public TextReader SplitLineContent(char separator)
        {
            _separator = separator;
            return this;
        }

        public TextReader SkipLines(Predicate<string> predicate)
        {
            _skipConditions.Add(predicate);
            return this;
        }

        public FileContent GetContent()
        {
            var lines = new List<FileLine>();
            foreach (string line in _content)
            {
                if (_skipConditions.Any(x => x.Invoke(line)))
                {
                    continue;
                }

                string content = line;

                if (_trim)
                {
                    content = content.Trim();
                }

                lines.Add(new FileLine(content.Split(_separator), _separator));
            }

            return new FileContent(lines);
        }
    }
}