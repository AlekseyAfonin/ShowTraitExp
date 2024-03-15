using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace ShowTraitExp;

internal class TraitChangeXpPatch
{
    [HarmonyPatch(typeof(TraitLevelingHelper), "AddPlayerTraitXPAndLogEntry")]
    private class AddPlayerTraitXpAndLogEntryPatch
    {
        private static void Postfix(TraitObject trait, int xpValue, Hero referenceHero)
        {
            try
            {
                var xpValueTotal = Campaign.Current.PlayerTraitDeveloper.GetPropertyValue(trait);
                Campaign.Current.Models.CharacterDevelopmentModel.GetTraitLevelForTraitXp(
                    referenceHero, trait, xpValueTotal, out var traitLvl, out var xpAfterChangeValueClamped);
                ShowTraitExp.PrintMessage(trait, xpValue, xpAfterChangeValueClamped, traitLvl);
            }
            catch(MBException e)
            {
                InformationManager.DisplayMessage(
                    new InformationMessage($"ShowTraitExp AddPlayerTraitXpAndLogEntryPatch exception: {e.Message}", Colors.Red));
            }
        }
    }
}