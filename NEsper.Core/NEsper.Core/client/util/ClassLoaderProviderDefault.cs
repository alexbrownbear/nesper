///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using com.espertech.esper.compat;

namespace com.espertech.esper.client.util
{
    /// <summary>
    /// Default class loader provider returns the current thread context classloader.
    /// </summary>
    public class ClassLoaderProviderDefault : ClassLoaderProvider
    {
        public const string NAME = "ClassLoaderProvider";

        public static readonly ClassLoaderProviderDefault INSTANCE = new ClassLoaderProviderDefault();

        private ClassLoaderProviderDefault()
        {
        }

        public ClassLoader Classloader()
        {
            return ClassLoaderDefault.GetInstance();
        }
    }
} // end of namespace
