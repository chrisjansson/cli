﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.DotNet.Internal.ProjectModel.Compilation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal class LibraryExport
    {
        /// <summary>
        /// Gets the library that produced this export
        /// </summary>
        public LibraryDescription Library { get; }

        /// <summary>
        /// Gets a list of MSIL binaries required to run
        /// </summary>
        public IEnumerable<LibraryAssetGroup> RuntimeAssemblyGroups { get; }

        /// <summary>
        /// Non assembly runtime assets.
        /// </summary>
        public IEnumerable<LibraryAsset> RuntimeAssets { get; }

        /// <summary>
        /// Gets a list of native binaries required to run
        /// </summary>
        public IEnumerable<LibraryAssetGroup> NativeLibraryGroups { get; }

        /// <summary>
        /// Gets a list of fully-qualified paths to MSIL metadata references
        /// </summary>
        public IEnumerable<LibraryAsset> CompilationAssemblies { get; }

        /// <summary>
        /// Get a list of embedded resource files provided by this export.
        /// </summary>
        public IEnumerable<LibraryAsset> EmbeddedResources { get; }

        /// <summary>
        /// Gets a list of fully-qualified paths to source code file references
        /// </summary>
        public IEnumerable<LibraryAsset> SourceReferences { get; }

        /// <summary>
        /// Get a list of analyzers provided by this export.
        /// </summary>
        public IEnumerable<AnalyzerReference> AnalyzerReferences { get; }

        /// <summary>
        /// Get a list of resource assemblies provided by this export.
        /// </summary>
        public IEnumerable<LibraryResourceAssembly> ResourceAssemblies { get; }

        public LibraryExport(LibraryDescription library,
                             IEnumerable<LibraryAsset> compileAssemblies,
                             IEnumerable<LibraryAsset> sourceReferences,
                             IEnumerable<LibraryAssetGroup> runtimeAssemblyGroups,
                             IEnumerable<LibraryAsset> runtimeAssets,
                             IEnumerable<LibraryAssetGroup> nativeLibraryGroups,
                             IEnumerable<LibraryAsset> embeddedResources,
                             IEnumerable<AnalyzerReference> analyzers,
                             IEnumerable<LibraryResourceAssembly> resourceAssemblies)
        {
            Library = library;
            CompilationAssemblies = compileAssemblies;
            SourceReferences = sourceReferences;
            RuntimeAssemblyGroups = runtimeAssemblyGroups;
            RuntimeAssets = runtimeAssets;
            NativeLibraryGroups = nativeLibraryGroups;
            EmbeddedResources = embeddedResources;
            AnalyzerReferences = analyzers;
            ResourceAssemblies = resourceAssemblies;
        }

        private string DebuggerDisplay => Library.Identity.ToString();
    }
}
