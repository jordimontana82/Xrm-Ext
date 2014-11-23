/*The MIT License (MIT)

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
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xrm_Ext.Extensions.Metadata.Relationships
{
    /// <summary>
    /// This class adds extension methods to the organization service 
    /// to create one to many relationships
    /// </summary>
    public static class OneToMany
    {
        /// <summary>
        /// Creates a OneToMany relationship using the provided info
        /// </summary>
        /// <param name="this"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static CreateOneToManyResponse CreateOneToManyRelationship(this IOrganizationService @this,
                                                        MinimalRelationshipInformation info)
        {

            if(info == null)
                throw new InvalidOperationException("Relationship information must be not null");

            if (string.IsNullOrWhiteSpace(info.ReferencedEntityLogicalName) ||
                string.IsNullOrWhiteSpace(info.ReferencingEntityLogicalName))
                throw new InvalidOperationException("The referenced and referencing entities can't be empty");

            CreateOneToManyRequest createOneToManyRelationshipRequest =
                            new CreateOneToManyRequest
                        {
                            OneToManyRelationship =
                            new OneToManyRelationshipMetadata
                            {
                                ReferencedEntity = info.ReferencedEntityLogicalName,
                                ReferencingEntity = info.ReferencingEntityLogicalName,
                                SchemaName = info.GetOneToManyRelationshipSchemaName(),
                                AssociatedMenuConfiguration = new AssociatedMenuConfiguration
                                {
                                    Behavior = AssociatedMenuBehavior.UseCollectionName,
                                    Group = AssociatedMenuGroup.Details,
                                    //Label = new Label("Account", 1033),
                                    Order = 10000
                                },
                                CascadeConfiguration = new CascadeConfiguration
                                {
                                    Assign = CascadeType.NoCascade,
                                    Delete = CascadeType.RemoveLink,
                                    Merge = CascadeType.NoCascade,
                                    Reparent = CascadeType.NoCascade,
                                    Share = CascadeType.NoCascade,
                                    Unshare = CascadeType.NoCascade
                                }
                            },
                            Lookup = new LookupAttributeMetadata
                            {
                                SchemaName = info.LookupFieldName,
                                DisplayName = new Label(info.LookupDisplayName, 1033),
                                RequiredLevel = new AttributeRequiredLevelManagedProperty(AttributeRequiredLevel.None),
                                Description = new Label("Created trhough helper", 1033)
                            }
                        };

            CreateOneToManyResponse response = (CreateOneToManyResponse) @this.Execute(createOneToManyRelationshipRequest);
            return response;
        }
    }
}
