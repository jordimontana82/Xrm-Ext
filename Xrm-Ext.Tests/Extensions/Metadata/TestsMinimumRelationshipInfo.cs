using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Xrm_Ext.Extensions.Metadata.Relationships;

namespace Xrm_Ext.Tests.Extensions.Metadata
{
    public class TestsMinimumRelationshipInfo
    {
        [Fact]
        public void When_default_publisher_is_not_provided_return_default()
        {
            var relationshipInfo = new MinimalRelationshipInformation();
            Assert.True(relationshipInfo.PublisherPrefix.Equals("new_"));
        }

        [Fact]
        public void When_default_publisher_is_provided_return_that_one()
        {
            var relationshipInfo = new MinimalRelationshipInformation()
            {
                PublisherPrefix = "custom_"
            };
            Assert.True(relationshipInfo.PublisherPrefix.Equals("custom_"));
        }
    }
}
