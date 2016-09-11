using System.Linq;
using dotnetlint.Modes.GitHub;
using dotnetlint.Modes.LocalFileSystem;
using NUnit.Framework;
using Shouldly;

namespace dotnetlint.tests
{
    public class Class1
    {
        [Test]
        public void LintOptions()
        {
            var args = "file -v -h file.txt".Split(' ');
            var o = new LintOptionSet();
            var remaining = o.Parse(args);
            remaining.ShouldContain("file","file.txt");
        }

        [Test]
        public void ListAndFileOptions()
        {
            var args = "-v -h -f=visualstudio file.txt".Split(' ');
            var o = new LintOptionSet();
            var remaining = o.Parse(args);
            remaining.ShouldContain("file.txt");
            var f = new LocalFileSetOptionSet();
            var remaining2 = f.Parse(remaining);
            remaining2.ShouldContain("file.txt");

            o.Format.ShouldNotBeNull();
        }

        [Test]
        public void ListAndFileOptions2()
        {
            var args = "-v -h file.txt".Split(' ');
            var o = new LintOptionSet();
            var remaining = o.Parse(args);
            remaining.ShouldContain("file.txt");
            var f = new LocalFileSetOptionSet();
            var remaining2 = f.Parse(remaining);
            remaining2.ShouldContain("file.txt");

            o.Format.ShouldNotBeNull();
        }

        [Test]
        public void Github()
        {
            var args = "github https://api.github.com/repos/drusellers/dotnetlint/git/blobs/86e84e955650b8df696af6ac7566090afdcf7285".Split(' ');
            var o = new LintOptionSet();
            var remaining = o.Parse(args);
            remaining.ShouldContain("github","https://api.github.com/repos/drusellers/dotnetlint/git/blobs/86e84e955650b8df696af6ac7566090afdcf7285");
            var f = new GitHubOptionSet();
            var remaining2 = f.Parse(remaining);
            remaining2.ShouldContain("github", "https://api.github.com/repos/drusellers/dotnetlint/git/blobs/86e84e955650b8df696af6ac7566090afdcf7285");
            
        }
    }
}
