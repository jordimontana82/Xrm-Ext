using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xrm.Sdk;
using Xrm_Ext.Extensions.Entities.CloneHierarchyMessages;
using Xrm_Ext.Extensions.Entities;
using Xunit;
using FakeItEasy;

namespace Xrm_Ext.Tests.Extensions.Entities
{
    /// <summary>
    /// Unit tests to test the CloneHierarchy extension method
    /// </summary>
    public class TestCloneHierarchy
    {
        [Fact]
        public void When_cloning_hierarchy_with_a_null_request_exception_is_thrown()
        {
            CloneHierarchyRequest request = null;
            
            var fakedService = A.Fake<IOrganizationService>();
            var ex = Assert.Throws<InvalidOperationException>(() => fakedService.CloneHierarchy(request));
            Assert.Equal(ex.Message, "The clone hierarchy request must not be null");

        }

        [Fact]
        public void When_cloning_hierarchy_with_a_null_root_entity_exception_is_thrown()
        {
            var request = new CloneHierarchyRequest();

            var fakedService = A.Fake<IOrganizationService>();
            var ex = Assert.Throws<InvalidOperationException>(() => fakedService.CloneHierarchy(request));
            Assert.Equal(ex.Message, "The root entity must not be null");

        }

        [Fact]
        public void When_cloning_hierarchy_with_a_root_entity_with_an_empty_guid_exception_is_thrown()
        {
            var request = new CloneHierarchyRequest();
            request.Root = new Entity("account");

            var fakedService = A.Fake<IOrganizationService>();
            var ex = Assert.Throws<InvalidOperationException>(() => fakedService.CloneHierarchy(request));
            Assert.Equal(ex.Message, "The guid of the root entity must not be an empty guid");

        }

        [Fact]
        public void When_cloning_hierarchy_with_a_root_entity_with_a_null_or_empty_logical_name_exception_is_thrown()
        {
            var request = new CloneHierarchyRequest();
            var fakedService = A.Fake<IOrganizationService>();
            var nullMessage = "The logical name of the root entity must not be empty";

            request.Root = new Entity("") { Id = Guid.NewGuid() };
            var ex = Assert.Throws<InvalidOperationException>(() => fakedService.CloneHierarchy(request));
            Assert.Equal(ex.Message, nullMessage);

            request.Root = new Entity(null) { Id = Guid.NewGuid() };
            ex = Assert.Throws<InvalidOperationException>(() => fakedService.CloneHierarchy(request));
            Assert.Equal(ex.Message, nullMessage);

            request.Root = new Entity("            ") { Id = Guid.NewGuid() };
            ex = Assert.Throws<InvalidOperationException>(() => fakedService.CloneHierarchy(request));
            Assert.Equal(ex.Message, nullMessage);

        }
    }
}
