// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.CommandLineUtils;
using NuGet.Commands;
using NuGet.Common;

namespace NuGet.CommandLine.XPlat
{
    internal static class SignCommand
    {
        internal static void Register(CommandLineApplication app,
                         Func<ILogger> getLogger,
                         Action<LogLevel> setLogLevel,
                         Func<ISignCommandRunner> getCommandRunner)
        {
            app.Command("sign", signCmd =>
            {
                CommandArgument packagePaths = signCmd.Argument(
                    "<packages-path>",
                    Strings.SignCommandPackagePathDescription,
                    multipleValues: false);

                CommandOption outputDirectory = signCmd.Option(
                    "-o|--output",
                    Strings.SignCommandOutputDirectoryDescription,
                    CommandOptionType.SingleValue);

                CommandOption path = signCmd.Option(
                    "--certificate-path",
                    Strings.SignCommandCertificatePathDescription,
                    CommandOptionType.SingleValue);

                CommandOption store = signCmd.Option(
                    "--certificate-store-name",
                    Strings.SignCommandCertificateStoreNameDescription,
                    CommandOptionType.SingleValue);

                CommandOption location = signCmd.Option(
                    "--certificate-store-location",
                    Strings.SignCommandCertificateStoreLocationDescription,
                    CommandOptionType.SingleValue);

                CommandOption subject = signCmd.Option(
                    "--certificate-subject-name",
                    Strings.SignCommandCertificateSubjectNameDescription,
                    CommandOptionType.SingleValue);

                CommandOption fingerPrint = signCmd.Option(
                    "--certificate-fingerprint",
                    Strings.SignCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption password = signCmd.Option(
                    "--certificate-password",
                    Strings.SignCommandCertificatePasswordDescription,
                    CommandOptionType.SingleValue);

                CommandOption algorithm = signCmd.Option(
                    "--hash-algorithm",
                    Strings.SignCommandHashAlgorithmDescription,
                    CommandOptionType.SingleValue);

                CommandOption timestamper = signCmd.Option(
                    "--timestamper",
                    Strings.SignCommandTimestamperDescription,
                    CommandOptionType.SingleValue);

                CommandOption timestamperAlgorithm = signCmd.Option(
                    "--timestamp-hash-algorithm",
                    Strings.SignCommandTimestampHashAlgorithmDescription,
                    CommandOptionType.SingleValue);

                CommandOption overwrite = signCmd.Option(
                    "--overwrite",
                    Strings.SignCommandOverwriteDescription,
                    CommandOptionType.NoValue);

                signCmd.HelpOption(XPlatUtility.HelpOption);

                CommandOption verbosity = signCmd.Option(
                    "-v|--verbosity",
                    Strings.Verbosity_Description,
                    CommandOptionType.SingleValue);

                CommandOption interactive = signCmd.Option(
                    "--interactive",
                    Strings.Verbosity_Description,
                    CommandOptionType.NoValue);

                signCmd.Description = Strings.SignCommandDescription;
            });
        }
    }
}
