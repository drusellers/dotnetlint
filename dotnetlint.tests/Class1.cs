using System.Linq;
using NUnit.Framework;
using Shouldly;

namespace dotnetlint.tests
{
    public class Class1
    {
        [Test]
        public void X()
        {
            var args = "file -v -h file.txt".Split(' ');
            var o = new LintOptionSet();
            var remaining = o.Parse(args.Skip(1));
            remaining.ShouldContain("file.txt");
        }
    }
}
