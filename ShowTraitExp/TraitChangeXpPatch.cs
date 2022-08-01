using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace ShowTraitExp;

internal class TraitChangeXpPatch
{
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