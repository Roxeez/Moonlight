using System.Collections.Generic;

namespace Moonlight.Utility.Reader
{
    public class FileContent : FileRegion
    {
        public FileContent(IEnumerable<FileLine> lines) : base(lines)
        {
        }
    }
}