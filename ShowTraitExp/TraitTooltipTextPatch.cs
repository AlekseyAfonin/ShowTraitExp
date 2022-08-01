using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Localization;

namespace ShowTraitExp;

internal class EncyclopediaTraitItemVMConstructorPatch
{
    [HarmonyPatch(typeof(EncyclopediaTraitItemVM), MethodType.Constructor, typeof(TraitObject), typeof(Hero))]
    private class EncyclopediaTraitItemVMPatch
    {
        private static void Postfix(EncyclopediaTraitItemVM __instance, ref TraitObject traitObj, ref Hero hero)
        {
            if (!hero.Equals(Hero.MainHero)) return;
            
            TextObject lvlText = new("{=OKUTPdaa}Level");
            
            var traitTooltipText = CampaignUIHelper.GetTraitTooltipText(traitObj, __instance.Value);
            var xpValueTotal = Campaign.Current.PlayerTraitDeveloper.GetPropertyValue(traitObj);
            var charModel = Campaign.Current.Models.CharacterDevelopmentModel;
            
            var xpRequiredForPrevLevel = charModel.GetTraitXpRequiredForTraitLevel(traitObj, 
                __instance.Value < 0 ? __instance.Value - 1 : __instance.Value);
            var xpRequiredForNextLevel = charModel.GetTraitXpRequiredForTraitLevel(traitObj, 
                __instance.Value > 0 ? __instance.Value + 1 : __instance.Value);
            
            var prevLvl = $"{lvlText} {__instance.Value - 1}: ↓{xpRequiredForPrevLevel}";
            var nextLvl = $"{lvlText} {__instance.Value + 1}: ↑{xpRequiredForNextLevel}";

            var xpTextBlock = __instance.Value switch
            {
                2 => $"\nXP: {xpValueTotal}\n{prevLvl}",
                -2 => $"\nXP: {xpValueTotal}\n{nextLvl}",
                _ => $"\nXP: {xpValueTotal}\n{prevLvl}\n{nextLvl}"
            };

            __instance.Hint = new HintViewModel(new TextObject("{=!}" + traitTooltipText + xpTextBlock));
        }
    }
}