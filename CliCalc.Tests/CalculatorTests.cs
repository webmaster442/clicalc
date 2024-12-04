using System.Globalization;

using CliCalc.Engine;

using Spectre.Console.Testing;

namespace CliCalc.Tests;

public class CalculatorTests
{
    private Mediator _mediator;
    private CliCalc.Engine.Engine _engine;
    private ResultPresenter _presenter;
    private TestConsole _console;

    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _mediator = new Mediator();
        _engine = new Engine.Engine(_mediator);
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        _mediator.Dispose();
    }

    [TearDown]
    public void TestTeardown()
    {
        _mediator.UnRegister(_presenter);
        _console.Dispose();
    }

    [SetUp]
    public void TestSetup()
    {
        _console = new TestConsole();
        _presenter = new ResultPresenter(_mediator, _console, new CultureInfo("en-us"));
    }

    [TestCaseSource(typeof(TestCases), nameof(TestCases.Expressions))]
    public void Test (string expression, string expected)
    {
        var result = _engine.Evaluate(expression, default).Result;
        _presenter.Display(result);
        Assert.That(_console.Output, Is.EqualTo(expected));
    }
}