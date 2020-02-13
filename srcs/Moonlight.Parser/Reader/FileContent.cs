using System.Collections.Generic;

namespace Moonlight.Parser.Reader
{
    public class FileContent : FileRegion
    {
        public FileContent(IEnumerable<FileLine> lines) : base(lines)
        {
        }
    }
}