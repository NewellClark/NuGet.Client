// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using NuGet.Frameworks;
using NuGet.PackageManagement.VisualStudio;
using NuGet.Protocol.Core.Types;
using NuGet.VisualStudio;
using NuGet.VisualStudio.Internal.Contracts;

namespace NuGet.PackageManagement.UI
{
    internal class PackageLoadContext
    {
        private readonly Task<PackageCollection> _installedPackagesTask;
        private readonly JoinableTask<(PackageCollection, PackageCollection)> _allPackagesTask;

        public IEnumerable<SourceRepository> SourceRepositories { get; }

        public NuGetPackageManager PackageManager { get; }

        public IProjectContextInfo[] Projects { get; }

        // Indicates whether the loader is created by solution package manager.
        public bool IsSolution { get; }

        public IEnumerable<IVsPackageManagerProvider> PackageManagerProviders { get; }

        public PackageSearchMetadataCache CachedPackages { get; set; }

        public INuGetSolutionManagerService SolutionManager { get; }

        internal IServiceBroker ServiceBroker { get; }

        public PackageLoadContext(
            IEnumerable<SourceRepository> sourceRepositories,
            bool isSolution,
            INuGetUIContext uiContext)
        {
            SourceRepositories = sourceRepositories;
            IsSolution = isSolution;
            PackageManager = uiContext.PackageManager;
            Projects = (uiContext.Projects ?? Enumerable.Empty<IProjectContextInfo>()).ToArray();
            PackageManagerProviders = uiContext.PackageManagerProviders;
            SolutionManager = uiContext.SolutionManagerService;
            ServiceBroker = uiContext.ServiceBroker;

            _installedPackagesTask = PackageCollection.FromProjectsAsync(
                ServiceBroker,
                Projects,
                CancellationToken.None);
            _allPackagesTask = NuGetUIThreadHelper.JoinableTaskFactory.RunAsync(() => PackageCollection.FromProjectsIncludeTransitiveAsync(Projects, CancellationToken.None));
        }

        public Task<PackageCollection> GetInstalledPackagesAsync() => _installedPackagesTask;

        public async Task<(PackageCollection, PackageCollection)> GetAllPackagesAsync() => await _allPackagesTask;

        // Returns the list of frameworks that we need to pass to the server during search
        public async Task<IList<string>> GetSupportedFrameworksAsync()
        {
            var frameworks = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (IProjectContextInfo project in Projects)
            {
                IProjectMetadataContextInfo projectMetadata = await project.GetMetadataAsync(
                    ServiceBroker,
                    CancellationToken.None);
                NuGetFramework framework = projectMetadata.TargetFramework;

                if (framework != null)
                {
                    if (framework.IsAny)
                    {
                        // One of the project's target framework is AnyFramework. In this case,
                        // we don't need to pass the framework filter to the server.
                        return new List<string>();
                    }

                    if (framework.IsSpecificFramework)
                    {
                        frameworks.Add(framework.DotNetFrameworkName);
                    }
                }
                else
                {
                    // we also need to process SupportedFrameworks
                    IReadOnlyCollection<NuGetFramework> supportedFrameworks = projectMetadata.SupportedFrameworks;

                    if (supportedFrameworks != null && supportedFrameworks.Count > 0)
                    {
                        foreach (var f in supportedFrameworks)
                        {
                            if (f.IsAny)
                            {
                                return new List<string>();
                            }

                            frameworks.Add(f.DotNetFrameworkName);
                        }
                    }
                }

                // get the target frameworks for Package Reference style projects
                var targetFrameworkInfos = await project.GetTargetFrameworksAsync(CancellationToken.None);
                foreach (var targetFrameworkInfo in targetFrameworkInfos)
                {
                    frameworks.Add(targetFrameworkInfo.FrameworkName.DotNetFrameworkName);
                }
            }

            return frameworks.ToList();
        }
    }
}
