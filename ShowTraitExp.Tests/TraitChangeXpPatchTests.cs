using NUnit.Framework;
using TaleWorlds.Core;

namespace ShowTraitExp.Tests;

[TestFixture]
public class TraitChangeXpPatchTests
{
    [Test]
    public void Patch_BadResult_Exception()
    {
        var ex = Assert.Catch<MBException>(TraitChangeXpPatch.Patch);
        
        StringAssert.Contains("Harmony patch problem", ex?.Message);
    }
}