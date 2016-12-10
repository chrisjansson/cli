// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Build.Evaluation;
using Microsoft.DotNet.Cli.CommandLine;
using Microsoft.DotNet.Cli.Utils;
using System.Collections.Generic;

namespace Microsoft.DotNet.Tools.Remove.ProjectToProjectReference
{
    public class RemoveProjectToProjectReferenceCommand
    {
        public static int Run(string projectOrDirectory, string[] args)
        {
            DebugHelper.HandleDebugSwitch(ref args);

            CommandLineApplication app = new CommandLineApplication(throwOnUnexpectedArg: false)
            {
                Name = "dotnet remove <PROJECT> p2p",
                FullName = LocalizableStrings.AppFullName,
                Description = LocalizableStrings.AppDescription,
                HandleRemainingArguments = true,
                ArgumentSeparatorHelpText = LocalizableStrings.AppArgumentSeparatorHelpText
            };

            app.ArgumentHandledByParentCommand(
                "<PROJECT>",
                LocalizableStrings.CmdArgumentDescription);

            app.HelpOption("-h|--help");

            CommandOption frameworkOption = app.Option(
                $"-f|--framework <{LocalizableStrings.CmdFramework}>",
                LocalizableStrings.CmdFrameworkDescription,
                CommandOptionType.SingleValue);

            app.OnExecute(() => {
                if (string.IsNullOrEmpty(projectOrDirectory))
                {
                    throw new GracefulException(CommonLocalizableStrings.RequiredArgumentNotPassed, $"<{LocalizableStrings.ProjectException}>");
                }

                var msbuildProj = MsbuildProject.FromFileOrDirectory(new ProjectCollection(), projectArgument.Value);

                if (app.RemainingArguments.Count == 0)
                {
                    throw new GracefulException(LocalizableStrings.SpecifyAtLeastOneReferenceToRemove);
                }

                List<string> references = app.RemainingArguments;
                
                int numberOfRemovedReferences = msbuildProj.RemoveProjectToProjectReferences(
                    frameworkOption.Value(),
                    references);

                if (numberOfRemovedReferences != 0)
                {
                    msbuildProj.ProjectRootElement.Save();
                }

                return 0;
            });

            try
            {
                return app.Execute(args);
            }
            catch (GracefulException e)
            {
                Reporter.Error.WriteLine(e.Message.Red());
                app.ShowHelp();
                return 1;
            }
        }
    }
}
