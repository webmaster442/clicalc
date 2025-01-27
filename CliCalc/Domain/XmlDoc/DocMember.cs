// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------


using System.Xml.Serialization;

namespace CliCalc.Domain.XmlDoc;

[Serializable]
[XmlType(AnonymousType = true)]
public class DocMember
{
    [XmlElement("summary")]
    public string Summary { get; set; }

    [XmlElement("param")]
    public DocParam[] Param { get; set; }

    [XmlElement("returns")]
    public string Returns { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }

    public DocMember()
    {
        Summary = string.Empty;
        Param = Array.Empty<DocParam>();
        Returns = string.Empty;
        Name = string.Empty;
    }
}
