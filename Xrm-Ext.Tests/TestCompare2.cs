/*
Xrm-Ext Unit Tests
------------------------------------------------
Copyright (c) 2014 Jordi Montaña, @jordimontana

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE. 
 
*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xrm_Ext.Extensions;

namespace Xrm_Ext.Tests
{
    [TestClass]
    public class TestCompare2
    {
        [TestMethod]
        public void OneNullEntityAndOneNonNullEntityMustBeDifferent()
        {
            Entity e1 = null;
            Entity e2 = new Entity();

            Assert.IsFalse(e2.IsEqualTo(e1), "A null entity must not be equal to a non null entity");
        }

        [TestMethod]
        public void CheckNotEqualLogicalNames()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("");

            Assert.IsFalse(e2.IsEqualTo(e1), "Failed: 2 Entities with different logical names were equal");
        }

        [TestMethod]
        public void CheckEqualLogicalNames()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            Assert.IsTrue(e2.IsEqualTo(e1), "Failed: 2 Entities with equal logical names and no attributes weren't equal");
        }

        [TestMethod]
        public void CheckDifferentAttributeNumberInThisEntity()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute"] = 1;
            //e2 has no attributes

            Assert.IsFalse(e2.IsEqualTo(e1), "Failed: 2 Entities with different number of attributes were equal");
        }

        [TestMethod]
        public void CheckDifferentAttributeNumberInOtherEntity()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e2["attribute"] = 1;
            //e1 has no attributes

            Assert.IsFalse(e2.IsEqualTo(e1), "Failed: 2 Entities with different number of attributes were equal");
        }

        [TestMethod]
        public void CheckSameAttributesWithDifferentNames()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute"] = 1;
            e2["othername"] = 1;
            //e1 has no attributes

            Assert.IsFalse(e2.IsEqualTo(e1), "Failed: 2 Entities with exactly the same number of attributes but different attribute names were equal");
        }

        [TestMethod]
        public void CheckAttributeValueMatchBool()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = true;
            e2["attribute1"] = true;
            //e1 has no attributes

            Assert.IsTrue(e2.IsEqualTo(e1), "Failed: 2 Entities with just one bool attribute with the same name and value for both were different");
        }

        [TestMethod]
        public void CheckAttributeValueMatchBoolDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = true;
            e2["attribute1"] = false;
            //e1 has no attributes

            Assert.IsFalse(e2.IsEqualTo(e1), "Failed: 2 Entities with just one bool attribute with the same name and different value were equal");
        }

        [TestMethod]
        public void CheckAttributeValueMatchInteger()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1;
            e2["attribute1"] = 1;
            //e1 has no attributes

            Assert.IsTrue(e2.IsEqualTo(e1), "Failed: 2 Entities with just one int attribute with the same name and value for both were different");
        }

        [TestMethod]
        public void CheckAttributeValueMatchIntegerDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1;
            e2["attribute1"] = 1234;
            //e1 has no attributes

            Assert.IsFalse(e2.IsEqualTo(e1), "Failed: 2 Entities with just one int attribute with the same name and different value for both were equal");
        }

        [TestMethod]
        public void CheckAttributeValueMatchFloat()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123f;
            e2["attribute1"] = 1.123f;
            //e1 has no attributes

            Assert.IsTrue(e2.IsEqualTo(e1), "Failed: 2 Entities with just one float attribute with the same name and value for both were different");
        }

        [TestMethod]
        public void CheckAttributeValueMatchFloatDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123f;
            e2["attribute1"] = 1.1234f;
            //e1 has no attributes

            Assert.IsFalse(e2.IsEqualTo(e1), "Failed: 2 Entities with just one float attribute with the same name and different value for both were equal");

        }

        [TestMethod]
        public void CheckAttributeValueMatchDouble()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123456;
            e2["attribute1"] = 1.123456;
            //e1 has no attributes

            Assert.IsTrue(e1.IsEqualTo(e2), "2 Entities with just one double attribute with the same name and value for both were different");
        }

        [TestMethod]
        public void CheckDifferentDoubleAttributeValuesAreDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123456;
            e2["attribute1"] = 13212.234;
            //e1 has no attributes

            Assert.IsFalse(e1.IsEqualTo(e2), "2 Entities with just one double attribute with the same name and different value for both were equal");
        }

        [TestMethod]
        public void CheckAttributeValueMatchDecimal()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123456m;
            e2["attribute1"] = 1.123456m;
            //e1 has no attributes

            Assert.IsTrue(e1.IsEqualTo(e2), "2 Entities with just one decimal attribute with the same name and value for both were different");
        }

        [TestMethod]
        public void CheckDifferentDecimalAttributeValuesAreDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123456m;
            e2["attribute1"] = 13212.234m;
            //e1 has no attributes

            Assert.IsFalse(e1.IsEqualTo(e2), "2 Entities with just one decimal attribute with the same name and different value for both were equal");
        }

        [TestMethod]
        public void CheckTwoEntitiesWithOneAttributeWithDifferentDataTypesAreDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1; //int
            e2["attribute1"] = 1.0f; //float
            //e1 has no attributes

            Assert.IsFalse(e1.IsEqualTo(e2), "2 Entities with just one int and float attribute with the same value but different types were equal");
        }

        [TestMethod]
        public void CheckTwoEntitiesWithTwoAttributesWithSameValuesAreEqual()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1; //int
            e1["attribute2"] = 20; //int

            e2["attribute1"] = 1; //int
            e2["attribute2"] = 20; //int

            //e1 has no attributes

            Assert.IsTrue(e1.IsEqualTo(e2), "2 Entities with the 2 same attribute values were different");
        }

        [TestMethod]
        public void CheckTwoEntitiesWithTwoAttributesWithOneDifferentValuesAreDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1; //int
            e1["attribute2"] = 3; //int

            e2["attribute1"] = 1; //int
            e2["attribute2"] = 20; //int

            //e1 has no attributes

            Assert.IsFalse(e1.IsEqualTo(e2), "2 Entities with the 2 attributes, one different, were equal");
        }
    }
}
