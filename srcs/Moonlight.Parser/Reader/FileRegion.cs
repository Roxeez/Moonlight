using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moonlight.Parser.Reader
{
    public class FileRegion
    {
        protected FileRegion(IEnumerable<FileLine> lines) => Lines = lines;

        public IEnumerable<FileLine> Lines { get; }

        public IEnumerable<FileRegion> GetRegions(string regionSeparator)
        {
            FileLine[] lines = Lines.ToArray();

            var regions = new List<List<FileLine>>();
            var region = new List<FileLine>();
            foreach (FileLine line in lines)
            {
                if (line.StartWith(regionSeparator))
                {
                    region = new List<FileLine>();
                    regions.Add(region);
                }

                region.Add(line);
            }

            return regions.Select(x => new FileRegion(x));
        }

        public IEnumerable<FileLine> GetLines(Predicate<FileLine> predicate) => Lines.Where(predicate.Invoke);
        public FileLine GetLine(Predicate<FileLine> predicate) => Lines.FirstOrDefault(predicate.Invoke);

        public string AsString()
        {
            var sb = new StringBuilder();
            foreach (FileLine line in Lines)
            {
                sb.Append(line.AsString()).Append("\r");
            }

            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}