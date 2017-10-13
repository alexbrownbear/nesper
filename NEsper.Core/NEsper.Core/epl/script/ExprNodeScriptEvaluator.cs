///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.epl.expression.core;

namespace com.espertech.esper.epl.script
{
    public interface ExprNodeScriptEvaluator : ExprEvaluator
    {
        Object Evaluate(Object[] lookupValues, ExprEvaluatorContext exprEvaluatorContext);
    }
} // end of namespace