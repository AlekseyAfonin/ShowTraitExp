using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ShowTraitExp;

public class SubModule : MBSubModuleBase
{
    protected override void OnSubModuleLoad()
    {
        base.OnSubModuleLoad();
        try
        {
            TraitChangeXpPatch.Patch();
        }
        catch (MBException e)
        {
            InformationManager.DisplayMessage(
                new InformationMessage($"ShowTraitExp loading problem. {e.Message}", Colors.Red));
        }
    }

    protected override void OnSubModuleUnloaded()
    {
        base.OnSubModuleUnloaded();
        try
        {
            TraitChangeXpPatch.Unpatch();
        }
        catch (MBException e)
        {
            InformationManager.DisplayMessage(
                new InformationMessage($"ShowTraitExp unloading problem. {e.Message}", Colors.Red));
        }
    }
}