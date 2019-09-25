﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGetPackageManager.Enums
{
    public enum PackageStatus
    {
        NotInstalled = -2,
        UpdateAvailable = -1,
        LastVersionInstalled = 0,
        Pending = 1
    }
}
