// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security;

namespace NuGet.Common
{
    public class EnvironmentVariableWrapper : IEnvironmentVariableReader
    {
        public static IEnvironmentVariableReader Instance { get; } = new EnvironmentVariableWrapper();

        public string GetEnvironmentVariable(string variable)
        {
            try
            {
                return Environment.GetEnvironmentVariable(variable);
            }
            catch (SecurityException ex)
            {
                var msg = "Throw an exception when running GetEnvironmentVariable for variable : " + variable;
                msg += $"\n Action :{ex.Action}  Demanded :{ex.Demanded.GetType()} HResult :{ex.HResult}";
                throw new SecurityException(ex.Message + msg);
            }
        }
    }
}
