using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dotnetlint.Configuration;
using dotnetlint.Rules;
using dotnetlint.Sources;

namespace dotnetlint.Outputs
{
    public class TeamCityFormat : OutputFormat
    {
        public void Write(TextWriter output,
            TextAndPath text,
            IEnumerable<RuleViolation> violations)
        {
            //TODO: Need a header/footer concept
            var reportName = "dotnetlint Violations";
            output.WriteLine($@"##teamcity[testSuiteStarted name='{reportName}']");

            

            output.WriteLine($@"##teamcity[testStarted name='{reportName}: {escapeTeamCityString(text.Path)}']");


            if (violations.Any(v => v.Disposition == RuleDispostion.Error))
            {
                var errorList = violations.Select(v => v.Message);
                output.WriteLine(
                    $"##teamcity[testFailed name='{reportName}: {escapeTeamCityString(text.Path)}' message='{escapeTeamCityString(string.Join(Environment.NewLine, errorList))}']");
            }
            else if (violations.Any(v => v.Disposition == RuleDispostion.Warning))
            {
                var warningsList = violations.Select(v => v.Message);

                output.WriteLine(
                    $"##teamcity[testStdOut name='{reportName}: {escapeTeamCityString(text.Path)} out='warning: {escapeTeamCityString(string.Join(Environment.NewLine, warningsList))}']");
            }

            output.WriteLine($"##teamcity[testFinished name='{reportName}: {escapeTeamCityString(text.Path)}']");
        
            output.WriteLine($"##teamcity[testSuiteFinished name='{reportName}']");
        }

        string escapeTeamCityString(string input)
        {
            return input;
        }
    }
}
