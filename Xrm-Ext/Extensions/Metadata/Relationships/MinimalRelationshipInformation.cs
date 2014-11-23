using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xrm_Ext.Extensions.Metadata.Relationships
{
    /// <summary>
    /// The minimum set of information needed to create a relationship
    /// </summary>
    public class MinimalRelationshipInformation
    {
        /// <summary>
        /// The referenced entity logical name
        /// </summary>
        public string ReferencedEntityLogicalName { get; set; }

        /// <summary>
        /// The referencing entity logical name
        /// </summary>
        public string ReferencingEntityLogicalName { get; set; }

        /// <summary>
        /// In a One-To-Many or Many-To-One Relationship, the logical name of the lookup field
        /// </summary>
        public string LookupFieldName { get; set; }

        /// <summary>
        /// In a One-To-Many or Many-To-One relationship, the display name of the lookup field
        /// </summary>
        public string LookupDisplayName { get; set; }

        /// <summary>
        /// Default publisher prefix
        /// </summary>
        public string PublisherPrefix { get; set; }

        public MinimalRelationshipInformation()
        {
            //Set default values
            PublisherPrefix = "new_";
        }

        public string GetLookupFieldName()
        {
            if (string.IsNullOrWhiteSpace(LookupFieldName))
                return PublisherPrefix + ReferencedEntityLogicalName + "id";

            return LookupFieldName;
        }

        public string GetOneToManyRelationshipSchemaName()
        {
            return PublisherPrefix + ReferencedEntityLogicalName + "_" + ReferencingEntityLogicalName;
        }
    }
}
