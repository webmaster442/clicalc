using CliCalc.Domain;
using CliCalc.Interfaces;

namespace CliCalc.Engine;

internal sealed class API : IAPI
{
    private readonly IMediator _mediator;

    public API(IMediator mediator)
    {
        _mediator = mediator;
    }

    public void Exit()
    {
        Environment.Exit(0);
    }

    public void Reset()
    {
        _mediator.Notify(new MessageType.ResetMessage());
    }
}
