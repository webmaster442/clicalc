﻿// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliCalc.Domain;

internal class Configuration
{
    public Dictionary<string, double> Constants { get; set; } = new();
    public Dictionary<string, string> Variables { get; set; } = new();
}
