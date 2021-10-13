using HarmonyLib;

namespace ForgeBlueprints
{
	internal static class Patches
	{
		[HarmonyPatch(typeof(GearItem), "Awake")]
		private static class ChangeWeightPatch
		{
			internal static void Postfix(GearItem __instance)
			{
				if (__instance.name == "GEAR_Prybar")
				{
					__instance.m_WeightKG = ForgeModSettings.options.prybarWeight;
					__instance.m_Harvest.m_YieldGearUnits[0] = (int)(ForgeModSettings.options.prybarWeight * 4);
					__instance.m_Harvest.m_DurationMinutes = ((int)(ForgeModSettings.options.prybarWeight * 4)) * 20;
				}
				else if (__instance.name == "GEAR_CookingPot")
				{
					__instance.m_WeightKG = ForgeModSettings.options.cookingPotWeight;
					__instance.m_Harvest.m_YieldGearUnits[0] = (int)(ForgeModSettings.options.cookingPotWeight * 4);
					__instance.m_Harvest.m_DurationMinutes = ((int)(ForgeModSettings.options.cookingPotWeight * 4)) * 20;
				}
			}
		}

		[HarmonyPatch(typeof(BlueprintDisplayItem), "Setup")]
		private static class UpdateScrapMetalRequirements
		{
			internal static void Postfix(BlueprintItem bpi)
			{
				if (bpi?.m_CraftedResult?.name == "GEAR_CookingPot")
				{
					bpi.m_RequiredGearUnits[0] = (int)(ForgeModSettings.options.cookingPotWeight * 5 + 1);
				}
				else if (bpi?.m_CraftedResult?.name == "GEAR_Prybar")
				{
					bpi.m_RequiredGearUnits[0] = (int)(ForgeModSettings.options.prybarWeight * 5 + 1);
				}
			}
		}
	}
}
