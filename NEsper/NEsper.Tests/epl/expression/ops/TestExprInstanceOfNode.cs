///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

using System;

using com.espertech.esper.epl.expression.core;
using com.espertech.esper.epl.expression.funcs;
using com.espertech.esper.epl.expression.ops;
using com.espertech.esper.supportunit.bean;
using com.espertech.esper.supportunit.epl;
using com.espertech.esper.util.support;

using NUnit.Framework;

namespace com.espertech.esper.epl.expression
{
    [TestFixture]
    public class TestExprInstanceOfNode 
    {
        private ExprInstanceofNode[] _isNodes;
    
        [SetUp]
        public void SetUp()
        {
            _isNodes = new ExprInstanceofNode[5];
    
            _isNodes[0] = new ExprInstanceofNode(new String[] {"long"});
            _isNodes[0].AddChildNode(new SupportExprNode(1l, typeof(long)));
    
            _isNodes[1] = new ExprInstanceofNode(new String[] {typeof(SupportBean).FullName, "int", "string"});
            _isNodes[1].AddChildNode(new SupportExprNode("", typeof(string)));
    
            _isNodes[2] = new ExprInstanceofNode(new String[] {"string"});
            _isNodes[2].AddChildNode(new SupportExprNode(null, typeof(Boolean)));
    
            _isNodes[3] = new ExprInstanceofNode(new String[] {"string", "char"});
            _isNodes[3].AddChildNode(new SupportExprNode(new SupportBean(), typeof(Object)));
    
            _isNodes[4] = new ExprInstanceofNode(new String[] {"int", "float", typeof(SupportBean).FullName});
            _isNodes[4].AddChildNode(new SupportExprNode(new SupportBean(), typeof(Object)));
        }
    
        [Test]
        public void TestGetType()
        {
            for (int i = 0; i < _isNodes.Length; i++)
            {
                _isNodes[i].Validate(SupportExprValidationContextFactory.MakeEmpty());
                Assert.AreEqual(typeof(bool?), _isNodes[i].ReturnType);
            }
        }
    
        [Test]
        public void TestValidate()
        {
            ExprInstanceofNode isNode = new ExprInstanceofNode(new String[0]);
            isNode.AddChildNode(new SupportExprNode(1));
    
            // Test too few nodes under this node
            try
            {
                isNode.Validate(SupportExprValidationContextFactory.MakeEmpty());
                Assert.Fail();
            }
            catch (ExprValidationException ex)
            {
                // Expected
            }
    
            // Test node result type not fitting
            isNode.AddChildNode(new SupportExprNode("s"));
            try
            {
                isNode.Validate(SupportExprValidationContextFactory.MakeEmpty());
                Assert.Fail();
            }
            catch (ExprValidationException ex)
            {
                // Expected
            }
        }
    
        [Test]
        public void TestEvaluate()
        {
            for (int i = 0; i < _isNodes.Length; i++)
            {
                _isNodes[i].Validate(SupportExprValidationContextFactory.MakeEmpty());
            }
    
            Assert.AreEqual(true, _isNodes[0].Evaluate(new EvaluateParams(null, false, null)));
            Assert.AreEqual(true, _isNodes[1].Evaluate(new EvaluateParams(null, false, null)));
            Assert.AreEqual(false, _isNodes[2].Evaluate(new EvaluateParams(null, false, null)));
            Assert.AreEqual(false, _isNodes[3].Evaluate(new EvaluateParams(null, false, null)));
            Assert.AreEqual(true, _isNodes[4].Evaluate(new EvaluateParams(null, false, null)));
        }
    
        [Test]
        public void TestEquals()
        {
            Assert.IsFalse(_isNodes[0].EqualsNode(new ExprEqualsNodeImpl(true, false)));
            Assert.IsFalse(_isNodes[0].EqualsNode(_isNodes[1]));
            Assert.IsTrue(_isNodes[0].EqualsNode(_isNodes[0]));
        }
    
        [Test]
        public void TestToExpressionString()
        {
            Assert.AreEqual("instanceof(\"\"," + typeof(SupportBean).FullName + ",int,string)", _isNodes[1].ToExpressionStringMinPrecedenceSafe());
        }
    }
}
