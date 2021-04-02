using System;
using Harmony;
using UnhollowerBaseLib;
using UnityEngine;

namespace ForgeBlueprints
{
    internal static class Patches
    {
		[HarmonyPatch(typeof(GearItem), "ManualUpdate")]
		private static class ChangeWeightPatch
		{
			internal static void Postfix(GearItem __instance)
			{
				if (__instance.name == "GEAR_Prybar")
				{
					__instance.m_WeightKG = Settings.options.prybarWeight;
					__instance.m_Harvest.m_YieldGearUnits[0] = (int)(Settings.options.prybarWeight * 5);
					__instance.m_Harvest.m_DurationMinutes = ((int)(Settings.options.prybarWeight * 5)) * 15;
				}
				if (__instance.name == "GEAR_CookingPot")
				{
					__instance.m_WeightKG = Settings.options.cookingPotWeight;
					__instance.m_Harvest.m_YieldGearUnits[0] = (int)(Settings.options.cookingPotWeight * 5);
					__instance.m_Harvest.m_DurationMinutes = ((int)(Settings.options.cookingPotWeight * 5)) * 15;
				}
			}
		}

		[HarmonyPatch(typeof(Panel_Crafting), "ItemPassesFilter")]
		private static class ShowRecipesInToolsSection
		{
			internal static void Postfix(Panel_Crafting __instance, ref bool __result, BlueprintItem bpi)
			{
				if (bpi?.m_CraftedResult?.name == "GEAR_ScrapMetal" && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
			}
		}

		[HarmonyPatch(typeof(Panel_Crafting), "ItemPassesFilter")]
		private static class UpdateScrapMetalRequirements
		{
			internal static void Postfix(BlueprintItem bpi)
			{
				if (bpi?.m_CraftedResult?.name == "GEAR_CookingPot")
				{
					bpi.m_RequiredGearUnits[0] = (int)(Settings.options.cookingPotWeight * 5 + 1);
				}
				if (bpi?.m_CraftedResult?.name == "GEAR_Prybar")
				{
					bpi.m_RequiredGearUnits[0] = (int)(Settings.options.prybarWeight * 5 + 1);
				}
			}
		}
	}
}
