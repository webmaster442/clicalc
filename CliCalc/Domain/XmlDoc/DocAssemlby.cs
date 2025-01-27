// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------


using System.Xml.Serialization;

namespace CliCalc.Domain.XmlDoc;

[Serializable]
[XmlType(AnonymousType = true)]
public class DocAssemlby
{
    [XmlElement("name")]
    public string Name { get; set; }

    public DocAssemlby()
    {
        Name = string.Empty;
    }
}
