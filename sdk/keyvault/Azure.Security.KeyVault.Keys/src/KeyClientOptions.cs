﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using Azure.Core.Pipeline;
using System;

namespace Azure.Security.KeyVault.Keys
{
    /// <summary>
    /// Options that allow to configure the management of the request sent to Key Vault
    /// </summary>
    public class KeyClientOptions : ClientOptions
    {
        /// <summary>
        /// The latest service version supported by this client library.
        /// For more information, see
        /// <see href="https://docs.microsoft.com/en-us/rest/api/keyvault/key-vault-versions"/>
        /// </summary>
        internal const ServiceVersion LatestVersion = ServiceVersion.V7_0;

        /// <summary>
        /// The versions of Azure Key Vault supported by this client
        /// library.
        /// </summary>
        public enum ServiceVersion
        {
#pragma warning disable CA1707 // Identifiers should not contain underscores
            /// <summary>
            /// The Key Vault API version 7.0.
            /// </summary>
            V7_0 = 0
#pragma warning restore CA1707 // Identifiers should not contain underscores
        }

        /// <summary>
        /// Gets the <see cref="ServiceVersion"/> of the service API used when
        /// making requests. For more information, see
        /// <see href="https://docs.microsoft.com/en-us/rest/api/keyvault/key-vault-versions"/>
        /// </summary>
        public ServiceVersion Version { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyClientOptions"/>
        /// class.
        /// </summary>
        /// <param name="version">
        /// The <see cref="ServiceVersion"/> of the service API used when
        /// making requests.
        /// </param>
        public KeyClientOptions(ServiceVersion version = ServiceVersion.V7_0)
        {
            Version = version;
        }

        internal string GetVersionString()
        {
            string version = string.Empty;

            switch (Version)
            {
                case ServiceVersion.V7_0:
                    version = "7.0";
                    break;

                default:
                    throw new ArgumentException(Version.ToString());
            }

            return version;
        }
    }
}
