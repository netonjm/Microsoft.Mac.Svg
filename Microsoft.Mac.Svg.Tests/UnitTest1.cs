using Microsoft.Mac.Svg.Operations;

namespace Microsoft.Mac.Svg.Tests;

[TestFixture]
public class OperationTests
{
    [SetUp]
    public void Setup()
    {
       
    }

    [Test]
    public void Translate()
    {
       
        //translate
        var tranlateOperation = (Translate)OperationBuilder.Build("translate(10 12)");
        Assert.AreEqual(10, tranlateOperation.X);
        Assert.AreEqual(12, tranlateOperation.Y);
    }
}
