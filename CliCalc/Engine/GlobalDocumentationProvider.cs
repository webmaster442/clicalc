using System.Diagnostics;
using System.Xml.Serialization;

using CliCalc.Domain;
using CliCalc.Domain.XmlDoc;
using CliCalc.DomainServices;
using CliCalc.Interfaces;

namespace CliCalc.Engine;

internal class GlobalDocumentationProvider : IRequestable<IReadOnlyDictionary<string, string>>
{
    private readonly XmlDoc _documentation;
    private readonly Dictionary<string, IHashMarkCommand> _hashMarkCommands;

    public GlobalDocumentationProvider(IMediator mediator, Dictionary<string, IHashMarkCommand> hashMarkCommands)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(XmlDoc), new XmlRootAttribute("doc"));
        var file = Path.Combine(AppContext.BaseDirectory, "CliCalc.Functions.xml");
        using (var stream = File.OpenRead(file))
        {
            _documentation = serializer.Deserialize(stream) as XmlDoc
                ?? new XmlDoc();
        }
        mediator.Register(this);
        _hashMarkCommands = hashMarkCommands;
    }

    bool IRequestableBase.CanServe(string dataSetName)
        => dataSetName == MessageTypes.DataSets.GlobalDocumentation
        || dataSetName == MessageTypes.DataSets.HasmarksDocumentation;

    IReadOnlyDictionary<string, string> IRequestable<IReadOnlyDictionary<string, string>>.OnRequest(string dataSet)
    {
        return dataSet switch
        {
            MessageTypes.DataSets.GlobalDocumentation 
                => _documentation.Members
                            .Where(m => m.Name.Contains("CliCalc.Functions.Global."))
                            .Where(m => m.IsMethod() || m.IsProperty())
                            .OrderBy(m => m.GetName())
                            .ToDictionary(m => m.GetName(), m => m.GetDocumentation()),
            MessageTypes.DataSets.HasmarksDocumentation
                => _hashMarkCommands
                    .OrderBy(c => c.Key)
                    .ToDictionary(c => c.Key, c => c.Value.Description),
            _ 
                => throw new UnreachableException(),
        };
    }
}
