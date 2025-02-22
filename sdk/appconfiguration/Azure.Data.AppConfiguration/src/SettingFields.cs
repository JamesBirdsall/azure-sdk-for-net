// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using System;

namespace Azure.Data.AppConfiguration
{
    /// <summary>
    /// Fields to retrieve from a configuration setting.
    /// </summary>
    [Flags]
    public enum SettingFields: uint
    {
        /// <summary>
        /// The primary identifier of a configuration setting.
        /// </summary>
        Key = 0x0001,
        /// <summary>
        /// The value used to group configuration settings.
        /// </summary>
        Label = 0x0002,
        /// <summary>
        /// The value of the configuration setting.
        /// </summary>
        Value = 0x0004,
        /// <summary>
        /// The content type of the configuration setting's value.
        /// </summary>
        ContentType = 0x0008,
        /// <summary>
        /// An ETag indicating the state of a configuration setting within a configuration store.
        /// </summary>
        ETag = 0x0010,
        /// <summary>
        /// >The last time a modifying operation was performed on the given configuration setting.
        /// </summary>
        LastModified = 0x0020,
        /// <summary>
        /// A value indicating whether the configuration setting is locked.
        /// </summary>
        Locked = 0x0040,
        /// <summary>
        /// A dictionary of tags that can help identify what a configuration setting may be applicable for.
        /// </summary>
        Tags = 0x0080,
        /// <summary>
        /// Allows for all the properties of a ConfigurationSetting to be retrieved.
        /// </summary>
        All = uint.MaxValue
    }
}
