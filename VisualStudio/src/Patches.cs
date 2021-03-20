using System;
using Harmony;
using UnhollowerBaseLib;
using UnityEngine;

namespace ForgeBlueprints
{
    internal static class Patches
    {
        private const string SCRAP_METAL_NAME = "GEAR_ScrapMetal";
        private const string COOKING_POT_NAME = "GEAR_CookingPot";
        private const string PRYBAR_NAME = "GEAR_Prybar";
        //private const string ARROWHEAD_NAME = "GEAR_ArrowHead";
		//private const string HEAVYHAMMER_NAME = "GEAR_Hammer";

		//Adjust Item Weights
		[HarmonyPatch(typeof(GearItem), "ManualUpdate")]
		private static class ChangeWeightPatch
		{
			internal static void Postfix(GearItem __instance)
			{
				if (__instance.name == PRYBAR_NAME)
				{
					__instance.m_WeightKG = Settings.options.prybarWeight;
					__instance.m_Harvest.m_YieldGearUnits[0] = (int)(Settings.options.prybarWeight * 5);
					__instance.m_Harvest.m_DurationMinutes = ((int)(Settings.options.prybarWeight * 5)) * 15;
				}
				if (__instance.name == COOKING_POT_NAME)
				{
					__instance.m_WeightKG = Settings.options.cookingPotWeight;
					__instance.m_Harvest.m_YieldGearUnits[0] = (int)(Settings.options.cookingPotWeight * 5);
					__instance.m_Harvest.m_DurationMinutes = ((int)(Settings.options.cookingPotWeight * 5)) * 15;
				}
			}
		}

		//Change Item Prefab Weights
		[HarmonyPatch(typeof(GameManager), "Update")]
		private static class ChangePrefabs
		{
			internal static void Postfix()
			{
				GetGearItemPrefab(PRYBAR_NAME).m_WeightKG = Settings.options.prybarWeight;
				GetGearItemPrefab(COOKING_POT_NAME).m_WeightKG = Settings.options.cookingPotWeight;
			}
			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
		}


		[HarmonyPatch(typeof(Panel_Crafting), "ItemPassesFilter")]
		private static class ShowRecipesInToolsSection
		{
			internal static void Postfix(Panel_Crafting __instance, ref bool __result, BlueprintItem bpi)
			{
				if (bpi?.m_CraftedResult?.name == SCRAP_METAL_NAME && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
			}
		}

		//Update Blueprint when Mod Settings change; will probably cause conflicts with mods that add a cooking pot or prybar blueprint
		[HarmonyPatch(typeof(Panel_Crafting), "ItemPassesFilter")]
		private static class UpdateScrapMetalRequirements
		{
			internal static void Postfix(BlueprintItem bpi)
			{
				if (bpi?.m_CraftedResult?.name == COOKING_POT_NAME)
				{
					bpi.m_RequiredGearUnits[0] = (int)(Settings.options.cookingPotWeight * 5 + 1);
				}
				if (bpi?.m_CraftedResult?.name == PRYBAR_NAME )
				{
					bpi.m_RequiredGearUnits[0] = (int)(Settings.options.prybarWeight * 5 + 1);
				}
			}
		}
	}
}
