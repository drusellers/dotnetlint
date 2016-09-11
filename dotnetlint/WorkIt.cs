using System;
using System.Collections.Generic;
using System.Linq;
using dotnetlint.Rules;
using dotnetlint.Sources;

namespace dotnetlint
{
    public static class WorkIt
    {
        public static void Work(IEnumerable<Source> sources,
            IEnumerable<Rule> rules,
            Action<TextAndPath, IEnumerable<RuleViolation>> action)
        {
            foreach (var source in sources)
            {
                foreach (var sourceText in source.Get().Result)
                {
                    action(sourceText, rules.SelectMany(r => r.Check(sourceText.Parse())));
                }
            }
        }
    }
}
