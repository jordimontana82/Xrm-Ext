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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xrm.Sdk;


namespace Xrm_Ext.Extensions.Entities
{
    /// <summary>
    /// This class contains extension methods to compare two Entity records.
    /// </summary>
    public static class Compare2
    {
        /// <summary>
        /// Compares this Entity against another instance, comparing every single attribute
        /// </summary>
        /// <param name="this"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool IsEqualTo(this Entity @this, Entity other)
        {
            if (other == null) return false; //As @this is always defined

            //@this and other not null, check logicalnames
            if (!@this.LogicalName.Equals(other.LogicalName))
                return false;  //Logical names do not match (we were trying to compare records from different entities...

            //Check that all the attributes in @this exist in 'other' with exactly the same values and viceversa
            foreach (var att in @this.Attributes)
            {
                if (other.Attributes.ContainsKey(att.Key))
                {
                    if (!AreValuesEqual(att.Value, other[att.Key]))
                        return false;
                }
                else
                    return false;  //@this has an attribute that other doesn't

            }

            //Same thing the other way around...
            foreach (var att in other.Attributes)
            {
                if (@this.Attributes.ContainsKey(att.Key))
                {
                    if (!AreValuesEqual(att.Value, @this[att.Key]))
                        return false;
                }
                else
                    return false;  //@this has an attribute that other doesn't

            }

            //At this point, all the attributes match!
            return true;
        }

        /// <summary>
        /// Compares two values, and returns true if both had the same value.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool AreValuesEqual(object v1, object v2)
        {
            //If both values are null, then they are actually equal
            if (v1 == null && v2 == null) return true;

            //If one of the values is null (and the other doesn't, then they are different)
            if (v1 == null) return false;
            if (v2 == null) return false;

            //Then, specific comparison depending on the data type:

            if(v1 is EntityReference) {
                    if (!(v2 is EntityReference)) return false; //The other value is not an entity reference, so they are indeed different

                    return (v1 as EntityReference).Id == (v2 as EntityReference).Id;
            }
            else if(v1 is bool) {
                    if (!(v2 is bool)) return false; //The other value is not a bool type, so they are different

                    return (bool)v1 == (bool)v2;
            }
            else if(v1 is OptionSetValue) {
                    if (!(v2 is OptionSetValue)) return false;

                    return (v1 as OptionSetValue).Value == (v2 as OptionSetValue).Value; //value changed if the int values are different
            }
            else if(v1 is float) {
                    if (!(v2 is float)) return false; //The other value is not a float type, so they are different

                    return (float)v1 == (float)v2;
            }
            else if(v1 is double) {
                    if (!(v2 is double)) return false; //The other value is not a float type, so they are different

                    return (double)v1 == (double)v2;
            }
            else {
                    //Comparison using strings
                    return string.Equals(v1.ToString(), v2.ToString()); //value changed if the values are different

            }
        }
    }
}
