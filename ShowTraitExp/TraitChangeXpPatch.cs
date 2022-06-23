using System.Reflection;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;

namespace ShowTraitExp;

public static class TraitChangeXpPatch
{
    private static Harmony _harmony;
    private static Assembly _assembly;

    public static void Patch()
    {
        try
        {
            _harmony = new Harmony("ShowTraitExp");
            _assembly = Assembly.GetExecutingAssembly();
            _harmony.PatchAll(_assembly);
        }
        catch
        {
            throw new MBException("Harmony patch problem");
        }
    }

    public static void Unpatch()
    {
        try
        {
            _harmony.UnpatchAll("ShowTraitExp");
        }
        catch
        {
            throw new MBException("Harmony unpatch problem");
        }
    }

    [HarmonyPatch(typeof(TraitLevelingHelper), "AddPlayerTraitXPAndLogEntry")]
    private class AddPlayerTraitXpAndLogEntryPatch
    {
        private static void Postfix(TraitObject trait, int xpValue, Hero referenceHero)
        {
            var xpValueTotal = Campaign.Current.PlayerTraitDeveloper.GetPropertyValue(trait);
            Campaign.Current.Models.CharacterDevelopmentModel.GetTraitLevelForTraitXp(
                referenceHero, trait, xpValueTotal, out var traitLvl, out var xpAfterChangeValueClamped);
            ShowTraitExp.PrintMessage(trait, xpValue, xpAfterChangeValueClamped, traitLvl);
        }
    }
}