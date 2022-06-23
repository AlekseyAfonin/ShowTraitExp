using NUnit.Framework;
using System;

namespace ShowTraitExp.Tests;

[TestFixture]
public class ShowTraitExpTests
{
    [Test]
    public void PrintMessage_NullTrait_Throw()
    {
        var ex = Assert.Catch<Exception>(() => ShowTraitExp.PrintMessage(null, 0,0,0));
        
        StringAssert.Contains("Trait is empty", ex.Message);
    }
}