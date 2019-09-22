﻿using Catel;
using Catel.IoC;
using NuGet.ProjectManagement;
using NuGetPackageManager.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGetPackageManager.Providers
{
    public class NuGetProjectContextProvider : INuGetProjectContextProvider
    {
        ITypeFactory _typeFactory;

        public NuGetProjectContextProvider(ITypeFactory typeFactory)
        {
            Argument.IsNotNull(() => typeFactory);

            _typeFactory = typeFactory;
        }

        public INuGetProjectContext GetProjectContext(FileConflictAction fileConflictAction)
        {
            var projectContext = _typeFactory.CreateInstanceWithParametersAndAutoCompletion<NuGetProjectContext>(fileConflictAction);

            return projectContext;
        }
    }
}
