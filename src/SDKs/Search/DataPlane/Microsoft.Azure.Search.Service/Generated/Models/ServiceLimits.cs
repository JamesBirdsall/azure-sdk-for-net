// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Search.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Represents various service level limits.
    /// </summary>
    public partial class ServiceLimits
    {
        /// <summary>
        /// Initializes a new instance of the ServiceLimits class.
        /// </summary>
        public ServiceLimits()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ServiceLimits class.
        /// </summary>
        /// <param name="maxFieldsPerIndex">The maximum allowed fields per
        /// index.</param>
        public ServiceLimits(int? maxFieldsPerIndex = default(int?))
        {
            MaxFieldsPerIndex = maxFieldsPerIndex;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the maximum allowed fields per index.
        /// </summary>
        [JsonProperty(PropertyName = "maxFieldsPerIndex")]
        public int? MaxFieldsPerIndex { get; set; }

    }
}