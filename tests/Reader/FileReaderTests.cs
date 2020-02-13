using Moonlight.Parser.Reader;
using NFluent;
using Xunit;

namespace Moonlight.Tests.Reader
{
    public class FileReaderTests
    {
        [Fact]
        public void Reader_Should_Remove_Commented_Or_Empty_Lines_And_Trim_And_Split_Content()
        {
            const string text = @"    My first bla bli blue line
            My second blablabla line            
My third pif paf pof line    
# My fourth line";

            FileContent content = TextReader.FromString(text)
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .TrimLines()
                .SplitLineContent(' ')
                .GetContent();

            Check.That(content.Lines).CountIs(3);

            foreach (FileLine line in content.Lines)
            {
                Check.That(line.StartWith("My")).IsTrue();
                Check.That(line.EndWith("line")).IsTrue();
            }
        }
    }
}