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
                CommandOption outputDirectory = signCmd.Option(
                    "-o|--output",
                    Strings.Verbosity_Description,
                    CommandOptionType.SingleValue);

                CommandOption path = signCmd.Option(
                    "--certificate-path",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption store = signCmd.Option(
                    "--certificate-store-name",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption location = signCmd.Option(
                    "--certificate-store-location",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption subject = signCmd.Option(
                    "--certificate-subject-name",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption fingerPrint = signCmd.Option(
                    "--certificate-fingerprint",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption password = signCmd.Option(
                    "--certificate-password",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption algorithm = signCmd.Option(
                    "--hash-algorithm",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption timestamper = signCmd.Option(
                    "--timestamper",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption timestamperAlgorithm = signCmd.Option(
                    "--timestamp-hash-algorithm",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);

                CommandOption overwrite = signCmd.Option(
                    "--overwrite",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.NoValue);

                signCmd.HelpOption(XPlatUtility.HelpOption);

                CommandOption verbosity = signCmd.Option(
                    "-v|--verbosity",
                    Strings.Verbosity_Description,
                    CommandOptionType.SingleValue);

                CommandOption interactive = signCmd.Option(
                    "--interactive",
                    Strings.Verbosity_Description,
                    CommandOptionType.SingleValue);

                CommandOption config = signCmd.Option(
                    "--configfile",
                    Strings.VerifyCommandCertificateFingerprintDescription,
                    CommandOptionType.SingleValue);
            });
        }
    }
}