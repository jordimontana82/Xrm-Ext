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

using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xrm_Ext.Extensions.Entities;
using Xunit;

namespace Xrm_Ext.Tests
{
    public class TestCompare2
    {
        
        [Fact]
        public void OneNullEntityAndOneNonNullEntityMustBeDifferent()
        {
            Entity e1 = null;
            Entity e2 = new Entity();

            Assert.False(e2.IsEqualTo(e1), "A null entity must not be equal to a non null entity");
        }

        [Fact]
        public void CheckNotEqualLogicalNames()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("");

            Assert.False(e2.IsEqualTo(e1), "Failed: 2 Entities with different logical names were equal");
        }

        [Fact]
        public void CheckEqualLogicalNames()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            Assert.True(e2.IsEqualTo(e1), "Failed: 2 Entities with equal logical names and no attributes weren't equal");
        }

        [Fact]
        public void Two_entities_with_the_same_null_attribute_are_equal()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["name"] = null;
            e2["name"] = null;

            Assert.True(e2.IsEqualTo(e1));
        }

        [Fact]
        public void Two_entities_with_the_same_attribute_not_null_and_null_are_different()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["name"] = "Account name";
            e2["name"] = null;

            Assert.False(e2.IsEqualTo(e1));
        }

        [Fact]
        public void Two_entities_with_the_same_attribute_null_and_not_null_are_different()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["name"] = null;
            e2["name"] = "Anything else man!";

            Assert.False(e2.IsEqualTo(e1));
        }

        [Fact]
        public void CheckDifferentAttributeNumberInThisEntity()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute"] = 1;
            //e2 has no attributes

            Assert.False(e2.IsEqualTo(e1), "Failed: 2 Entities with different number of attributes were equal");
        }

        [Fact]
        public void CheckDifferentAttributeNumberInOtherEntity()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e2["attribute"] = 1;
            //e1 has no attributes

            Assert.False(e2.IsEqualTo(e1), "Failed: 2 Entities with different number of attributes were equal");
        }

        [Fact]
        public void CheckSameAttributesWithDifferentNames()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute"] = 1;
            e2["othername"] = 1;
            //e1 has no attributes

            Assert.False(e2.IsEqualTo(e1), "Failed: 2 Entities with exactly the same number of attributes but different attribute names were equal");
        }

        [Fact]
        public void CheckAttributeValueMatchBool()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = true;
            e2["attribute1"] = true;
            //e1 has no attributes

            Assert.True(e2.IsEqualTo(e1), "Failed: 2 Entities with just one bool attribute with the same name and value for both were different");
        }

        [Fact]
        public void CheckAttributeValueMatchBoolDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = true;
            e2["attribute1"] = false;
            //e1 has no attributes

            Assert.False(e2.IsEqualTo(e1), "Failed: 2 Entities with just one bool attribute with the same name and different value were equal");
        }

        [Fact]
        public void CheckAttributeValueMatchInteger()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1;
            e2["attribute1"] = 1;
            //e1 has no attributes

            Assert.True(e2.IsEqualTo(e1), "Failed: 2 Entities with just one int attribute with the same name and value for both were different");
        }

        [Fact]
        public void CheckAttributeValueMatchIntegerDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1;
            e2["attribute1"] = 1234;
            //e1 has no attributes

            Assert.False(e2.IsEqualTo(e1), "Failed: 2 Entities with just one int attribute with the same name and different value for both were equal");
        }

        [Fact]
        public void CheckAttributeValueMatchFloat()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123f;
            e2["attribute1"] = 1.123f;
            //e1 has no attributes

            Assert.True(e2.IsEqualTo(e1), "Failed: 2 Entities with just one float attribute with the same name and value for both were different");
        }

        [Fact]
        public void CheckAttributeValueMatchFloatDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123f;
            e2["attribute1"] = 1.1234f;
            //e1 has no attributes

            Assert.False(e2.IsEqualTo(e1), "Failed: 2 Entities with just one float attribute with the same name and different value for both were equal");

        }

        [Fact]
        public void CheckAttributeValueMatchDouble()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123456;
            e2["attribute1"] = 1.123456;
            //e1 has no attributes

            Assert.True(e1.IsEqualTo(e2), "2 Entities with just one double attribute with the same name and value for both were different");
        }

        [Fact]
        public void CheckDifferentDoubleAttributeValuesAreDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123456;
            e2["attribute1"] = 13212.234;
            //e1 has no attributes

            Assert.False(e1.IsEqualTo(e2), "2 Entities with just one double attribute with the same name and different value for both were equal");
        }

        [Fact]
        public void CheckAttributeValueMatchDecimal()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123456m;
            e2["attribute1"] = 1.123456m;
            //e1 has no attributes

            Assert.True(e1.IsEqualTo(e2), "2 Entities with just one decimal attribute with the same name and value for both were different");
        }

        [Fact]
        public void CheckDifferentDecimalAttributeValuesAreDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1.123456m;
            e2["attribute1"] = 13212.234m;
            //e1 has no attributes

            Assert.False(e1.IsEqualTo(e2), "2 Entities with just one decimal attribute with the same name and different value for both were equal");
        }

        [Fact]
        public void CheckTwoEntitiesWithOneAttributeWithDifferentDataTypesAreDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1; //int
            e2["attribute1"] = 1.0f; //float
            //e1 has no attributes

            Assert.False(e1.IsEqualTo(e2), "2 Entities with just one int and float attribute with the same value but different types were equal");
        }

        [Fact]
        public void CheckTwoEntitiesWithTwoAttributesWithSameValuesAreEqual()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1; //int
            e1["attribute2"] = 20; //int

            e2["attribute1"] = 1; //int
            e2["attribute2"] = 20; //int

            //e1 has no attributes

            Assert.True(e1.IsEqualTo(e2), "2 Entities with the 2 same attribute values were different");
        }

        [Fact]
        public void CheckTwoEntitiesWithTwoAttributesWithOneDifferentValuesAreDifferent()
        {
            Entity e1 = new Entity("logicalName1");
            Entity e2 = new Entity("logicalName1");

            e1["attribute1"] = 1; //int
            e1["attribute2"] = 3; //int

            e2["attribute1"] = 1; //int
            e2["attribute2"] = 20; //int

            //e1 has no attributes

            Assert.False(e1.IsEqualTo(e2), "2 Entities with the 2 attributes, one different, were equal");
        }

        [Fact]
        public void Two_entities_with_an_entity_reference_attribute_and_anything_else_are_different()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["primarycontactid"] = new EntityReference();
            e2["primarycontactid"] = 1;

            Assert.False(e1.IsEqualTo(e2));
        }

        [Fact]
        public void Two_entities_with_an_integer_attribute_and_anything_else_are_different()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["primarycontactid"] = 1;
            e2["primarycontactid"] = 123.4f;

            Assert.False(e1.IsEqualTo(e2));
        }

        [Fact]
        public void Two_entities_with_a_bool_attribute_and_anything_else_are_different()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["primarycontactid"] = true;
            e2["primarycontactid"] = 123.4f;

            Assert.False(e1.IsEqualTo(e2));
        }

        [Fact]
        public void Two_entities_with_a_float_attribute_and_anything_else_are_different()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["primarycontactid"] = 123.4f;
            e2["primarycontactid"] = false;

            Assert.False(e1.IsEqualTo(e2));
        }

        [Fact]
        public void Two_entities_with_a_string_attribute_and_anything_else_are_different()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["primarycontactid"] = "yeah amazing test";
            e2["primarycontactid"] = 3456789;

            Assert.False(e1.IsEqualTo(e2));
        }

        [Fact]
        public void Two_entities_with_a_double_attribute_and_anything_else_are_different()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["primarycontactid"] = 2345.34;
            e2["primarycontactid"] = "jaaaarl";

            Assert.False(e1.IsEqualTo(e2));
        }

        [Fact]
        public void Two_entities_with_an_optionsetvalue_attribute_and_anything_else_are_different()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            e1["statuscode"] = new OptionSetValue();
            e2["statuscode"] = "other different thing";

            Assert.False(e1.IsEqualTo(e2));
        }

        [Fact]
        public void Two_entities_with_the_same_entity_reference_are_equal()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            var g = Guid.NewGuid();

            e1["primarycontactid"] = new EntityReference() { Id = g };
            e2["primarycontactid"] = new EntityReference() { Id = g };

            Assert.True(e1.IsEqualTo(e2));
        }

        [Fact]
        public void Two_entities_with_the_same_optionsetvalue_are_equal()
        {
            Entity e1 = new Entity("account");
            Entity e2 = new Entity("account");

            var value = 60;

            e1["statuscode"] = new OptionSetValue() { Value = value };
            e2["statuscode"] = new OptionSetValue() { Value = value };

            Assert.True(e1.IsEqualTo(e2));
        }
    }
}
