
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
