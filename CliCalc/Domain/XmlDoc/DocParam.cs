// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------


using System.Xml.Serialization;

namespace CliCalc.Domain.XmlDoc;

[Serializable]
[XmlType(AnonymousType = true)]
public class DocParam
{
    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    [XmlText()]
    public string Value { get; set; }

    public DocParam()
    {
        Name = string.Empty;
        Value = string.Empty;
    }
}