﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Master;
public class AppSetting : AuditableWithBaseEntity<int>
{
    /// <summary>
    /// Gets or sets the ReferenceKey
    /// </summary>
    public string ReferenceKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Value
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the Type
    /// </summary>
    public string Type { get; set; } = string.Empty;
}
