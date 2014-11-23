using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xrm_Ext.Extensions.Entities.CloneHierarchyMessages
{
    /// <summary>
    /// Response returned by a call to 
    /// </summary>
    public class CloneHierarchyResponse: OrganizationResponse
    {
        /// <summary>
        /// The root of the cloned hierarchy
        /// </summary>
        public Entity Root { get; set; }
    }
}
