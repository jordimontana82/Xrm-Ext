using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;

namespace Xrm_Ext.Extensions.Entities.CloneHierarchyMessages
{
    /// <summary>
    /// A clone hierachy request expects the entity name and guid of the entity used as the root of the hierachy
    /// </summary>
    public class CloneHierarchyRequest: OrganizationRequest
    {
        /// <summary>
        /// The root of the hierarchy to be cloned
        /// </summary>
        public Entity Root { get; set; } 
    }

}
