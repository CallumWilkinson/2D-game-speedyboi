using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

public class SimplePlayModeTest
{
    [Test]
    public void BasicPlayModeTest()
    {
        Assert.Pass("This PlayMode test should always pass");
    }
    
    [UnityTest]
    public IEnumerator BasicCoroutineTest()
    {
        yield return null;
        Assert.Pass("This coroutine test should always pass");
    }
}