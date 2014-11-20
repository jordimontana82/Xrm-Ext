using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Xrm_Ext.Extensions.Metadata.Relationships;
using FakeItEasy;
using Microsoft.Xrm.Sdk;

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

        [Fact]
        public void When_a_one_to_many_relationship_is_created_passing_a_null_relationship_info_exception_is_thrown()
        {
            MinimalRelationshipInformation relationshipInfo = null;
            var service = A.Fake<IOrganizationService>();

            var exception = Assert.Throws<InvalidOperationException>(() => service.CreateOneToManyRelationship(relationshipInfo));
            Assert.True(exception.Message.Equals("Relationship information must be not null"));
        }
        

        [Fact]
        public void When_a_one_to_many_relationship_is_created_without_entity_names_exception_is_thrown()
        {
            var relationshipInfo = new MinimalRelationshipInformation();
            var service = A.Fake<IOrganizationService>();

            var exception = Assert.Throws<InvalidOperationException>(() => service.CreateOneToManyRelationship(relationshipInfo));
            Assert.True(exception.Message.Equals("The referenced and referencing entities can't be empty"));

        }
    }
}
