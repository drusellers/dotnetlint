using System;
using System.Collections.Generic;
using dotnetlint.Rules;
using dotnetlint.Sources;

namespace dotnetlint
{
    public static class WorkIt
    {
        public static void Work(IEnumerable<Source> sources,
            IEnumerable<Rule> rules,
            Action<TextAndPath, RuleViolation> action)
        {
            foreach (var source in sources)
            {
                foreach (var sourceText in source.Get().Result)
                {
                    foreach (var rule in rules)
                    {
                        foreach (var v in rule.Check(sourceText.Parse()))
                        {
                            action(sourceText, v);
                        }
                    }
                }
            }
        }
    }
}
