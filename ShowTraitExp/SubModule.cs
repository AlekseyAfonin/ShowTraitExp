using System.Reflection;
using HarmonyLib;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ShowTraitExp;

public class SubModule : MBSubModuleBase
{
    private Harmony? _harmony;
    private Assembly? _assembly;
    
    protected override void OnSubModuleLoad()
    {
        base.OnSubModuleLoad();
        try
        {
            _harmony = new Harmony("ShowTraitExp");
            _assembly = Assembly.GetExecutingAssembly();
            _harmony.PatchAll(_assembly);
        }
        catch (MBException e)
        {
            InformationManager.DisplayMessage(
                new InformationMessage($"ShowTraitExp loading problem. {e.Message}", Colors.Red));
        }
    }
}