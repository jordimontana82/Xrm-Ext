using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Xrm_Ext.Extensions.Metadata.Relationships;
using FakeItEasy;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;

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
        public void When_a_one_to_many_relationship_is_created_passing_a_null_referencingentity_exception_is_thrown()
        {
            MinimalRelationshipInformation relationshipInfo = new MinimalRelationshipInformation();
            relationshipInfo.ReferencedEntityLogicalName = "account";
            var service = A.Fake<IOrganizationService>();

            var exception = Assert.Throws<InvalidOperationException>(() => service.CreateOneToManyRelationship(relationshipInfo));
            Assert.True(exception.Message.Equals("The referenced and referencing entities can't be empty"));
        }

        [Fact]
        public void When_a_one_to_many_relationship_is_created_passing_a_null_referencedentity_exception_is_thrown()
        {
            MinimalRelationshipInformation relationshipInfo = new MinimalRelationshipInformation();
            relationshipInfo.ReferencingEntityLogicalName = "account";
            var service = A.Fake<IOrganizationService>();

            var exception = Assert.Throws<InvalidOperationException>(() => service.CreateOneToManyRelationship(relationshipInfo));
            Assert.True(exception.Message.Equals("The referenced and referencing entities can't be empty"));
        }
        

        [Fact]
        public void When_a_one_to_many_relationship_is_created_without_entity_names_exception_is_thrown()
        {
            var relationshipInfo = new MinimalRelationshipInformation();
            var service = A.Fake<IOrganizationService>();

            var exception = Assert.Throws<InvalidOperationException>(() => service.CreateOneToManyRelationship(relationshipInfo));
            Assert.True(exception.Message.Equals("The referenced and referencing entities can't be empty"));

        }

        [Fact]
        public void When_a_one_to_many_relationship_is_create_with_valid_relationship_info_request_is_sent()
        {
            var relationshipInfo = new MinimalRelationshipInformation();
            relationshipInfo.ReferencingEntityLogicalName = "contact";
            relationshipInfo.ReferencedEntityLogicalName = "account";

            var request = new CreateOneToManyRequest();
            var fakeResponse = new CreateOneToManyResponse();

            var service = A.Fake<IOrganizationService>();
            //Fake response
            A.CallTo(() => service.Execute(request)).WithAnyArguments().Returns(fakeResponse);

            var response = service.CreateOneToManyRelationship(relationshipInfo);
            Assert.Equal(fakeResponse, response);
        }

        [Fact]
        public void The_default_lookup_field_name_property_is_the_default_publisher_plus_referenced_entity_plus_id()
        {
            var relationshipInfo = new MinimalRelationshipInformation();
            relationshipInfo.ReferencedEntityLogicalName = "account";

            Assert.True(relationshipInfo.GetLookupFieldName().Equals("new_accountid"));
        }

        [Fact]
        public void The_default_lookup_field_name_property_is_the_the_specified_publisher_plus_referenced_entity_plus_id()
        {
            var relationshipInfo = new MinimalRelationshipInformation();
            relationshipInfo.ReferencedEntityLogicalName = "account";
            relationshipInfo.PublisherPrefix = "custom_";

            Assert.True(relationshipInfo.GetLookupFieldName().Equals("custom_accountid"));
        }

        [Fact]
        public void A_non_default_lookup_field_name_it_is_returned()
        {
            var relationshipInfo = new MinimalRelationshipInformation();
            relationshipInfo.ReferencedEntityLogicalName = "account";
            relationshipInfo.PublisherPrefix = "custom_";
            relationshipInfo.LookupFieldName = "new_accountid";

            Assert.True(relationshipInfo.GetLookupFieldName().Equals("new_accountid"));
        }

        [Fact]
        public void The_relationship_schema_name_is_the_publisher_plus_the_referenced_entity_plus_the_referencing_entity()
        {
            var relationshipInfo = new MinimalRelationshipInformation();
            relationshipInfo.ReferencedEntityLogicalName = "account";
            relationshipInfo.ReferencingEntityLogicalName = "contact";
            relationshipInfo.PublisherPrefix = "custom_";

            Assert.True(relationshipInfo.GetOneToManyRelationshipSchemaName().Equals("custom_account_contact"));
        }
    }
}
