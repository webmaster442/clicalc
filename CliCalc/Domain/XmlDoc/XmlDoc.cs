// --------------------------------------------------------------------------
// Copyright (c) 2024-2025 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
// --------------------------------------------------------------------------


using System.Xml.Serialization;

namespace CliCalc.Domain.XmlDoc;

[Serializable]
[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", IsNullable = false)]
public class XmlDoc
{
    [XmlElement("assembly")]
    public DocAssemlby Assembly { get; set; }

    [XmlArray("members")]
    [XmlArrayItem("member", IsNullable = false)]
    public DocMember[] Members { get; set; }

    public XmlDoc()
    {
        Members = Array.Empty<DocMember>();
        Assembly = new DocAssemlby();
    }
}
