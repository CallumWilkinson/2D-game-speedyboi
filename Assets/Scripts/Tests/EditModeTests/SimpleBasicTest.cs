using NUnit.Framework;

public class SimpleBasicTest
{
    [Test]
    public void BasicTestAlwaysPasses()
    {
        Assert.Pass("This test should always pass");
    }
    
    [Test]
    public void BasicTestMathWorks()
    {
        Assert.AreEqual(4, 2 + 2);
    }
    
    [Test]
    public void BasicTestStringWorks()
    {
        Assert.AreEqual("Hello", "Hel" + "lo");
    }
}