using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Xrm_Ext.Extensions.Entities;

namespace Xrm_Ext.Tests.Extensions.Entities
{
    public class TestsClone
    {
        [Fact]
        public void A_cloned_entity_with_one_attribute_should_be_equal_to_the_original()
        {
            var originalEntity = new Entity("account");
            originalEntity["name"] = "Super account";

            var cloned = originalEntity.Clone();
            Assert.True(originalEntity.IsEqualTo(cloned));
        }

        [Fact]
        public void A_cloned_entity_does_not_contain_the_primary_attribute()
        {
            var originalEntity = new Entity("account");
            originalEntity["name"] = "Super account";
            originalEntity["accountid"] = Guid.NewGuid();

            var cloned = originalEntity.Clone();

            Assert.False(cloned.Attributes.ContainsKey("accountid"));
        }
    }
}
