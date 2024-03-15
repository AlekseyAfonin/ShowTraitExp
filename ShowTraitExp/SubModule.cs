using HarmonyLib;
using System.Reflection;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;


namespace ShowTraitExp
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                var _harmony = new Harmony("ShowTraitExp");
                var _assembly = Assembly.GetExecutingAssembly();
                _harmony.PatchAll(_assembly);
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
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();
        }
    }
}