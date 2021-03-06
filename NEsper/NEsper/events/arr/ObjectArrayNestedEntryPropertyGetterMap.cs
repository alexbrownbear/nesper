///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

using com.espertech.esper.client;
using com.espertech.esper.events.map;

namespace com.espertech.esper.events.arr
{
    using Map = IDictionary<string, object>;

    /// <summary>
    /// A getter that works on EventBean events residing within a Map as an event property.
    /// </summary>
    public class ObjectArrayNestedEntryPropertyGetterMap : ObjectArrayNestedEntryPropertyGetterBase
    {
        private readonly MapEventPropertyGetter mapGetter;

        public ObjectArrayNestedEntryPropertyGetterMap(int propertyIndex, EventType fragmentType, EventAdapterService eventAdapterService, MapEventPropertyGetter mapGetter)
            : base(propertyIndex, fragmentType, eventAdapterService)
        {
            this.mapGetter = mapGetter;
        }

        public override Object HandleNestedValue(Object value)
        {
            if (!(value is Map))
            {
                if (value is EventBean)
                {
                    return mapGetter.Get((EventBean)value);
                }
                return null;
            }
            return mapGetter.GetMap((Map)value);
        }

        public override Object HandleNestedValueFragment(Object value)
        {
            if (!(value is Map))
            {
                if (value is EventBean)
                {
                    return mapGetter.GetFragment((EventBean)value);
                }
                return null;
            }

            // If the map does not contain the key, this is allowed and represented as null
            EventBean eventBean = EventAdapterService.AdapterForTypedMap((Map)value, FragmentType);
            return mapGetter.GetFragment(eventBean);
        }

        public override bool HandleNestedValueExists(Object value)
        {
            if (!(value is Map))
            {
                if (value is EventBean)
                {
                    return mapGetter.IsExistsProperty((EventBean) value);
                }
                return false;
            }
            return mapGetter.IsMapExistsProperty((Map) value);
        }
    }
}
