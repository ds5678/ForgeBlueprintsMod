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
        private const string ARROWHEAD_NAME = "GEAR_ArrowHead";
		private const string HEAVYHAMMER_NAME = "GEAR_Hammer";
		private const string SCRAP_METAL_CRAFTING_ICON_NAME = "ico_CraftItem__ScrapMetal";
		private const string COOKING_POT_CRAFTING_ICON_NAME = "ico_CraftItem__CookingPot";
		private const string PRYBAR_CRAFTING_ICON_NAME = "ico_CraftItem__Prybar";

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

		//Cooking Pot blueprint
		[HarmonyPatch(typeof(GameManager), "Awake")]
        private static class AddCookingPotRecipe
        {
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(SCRAP_METAL_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 6 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = GetToolsItemPrefab(HEAVYHAMMER_NAME);
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(0);

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(COOKING_POT_NAME);
				blueprint.m_CraftedResultCount = 1;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.Forge;
				blueprint.m_DurationMinutes = 240;
				blueprint.m_CraftingAudio = "PLAY_CraftingMetal";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
		}

		//Prybar blueprint
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddPrybarRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(SCRAP_METAL_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 6 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = GetToolsItemPrefab(HEAVYHAMMER_NAME);
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(0);

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(PRYBAR_NAME);
				blueprint.m_CraftedResultCount = 1;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.Forge;
				blueprint.m_DurationMinutes = 240;
				blueprint.m_CraftingAudio = "PLAY_CraftingMetal";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
		}

		//Efficient Arrowhead blueprint
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddEfficientArrowheadRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(SCRAP_METAL_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 3 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = GetToolsItemPrefab(HEAVYHAMMER_NAME);
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(0);

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(ARROWHEAD_NAME);
				blueprint.m_CraftedResultCount = 7;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.Forge;
				blueprint.m_DurationMinutes = 180;
				blueprint.m_CraftingAudio = "PLAY_CraftingMetal";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
		}

		//Harvest Arrowheads blueprint
		[HarmonyPatch(typeof(GameManager), "Awake")]
		private static class AddScrapMetalRecipe
		{
			internal static void Postfix()
			{
				BlueprintItem blueprint = GameManager.GetBlueprints().AddComponent<BlueprintItem>();

				// Inputs
				blueprint.m_RequiredGear = new Il2CppReferenceArray<GearItem>(1) { [0] = GetGearItemPrefab(ARROWHEAD_NAME) };
				blueprint.m_RequiredGearUnits = new Il2CppStructArray<int>(1) { [0] = 10 };
				blueprint.m_KeroseneLitersRequired = 0f;
				blueprint.m_GunpowderKGRequired = 0f;
				blueprint.m_RequiredTool = GetToolsItemPrefab(HEAVYHAMMER_NAME);
				blueprint.m_OptionalTools = new Il2CppReferenceArray<ToolsItem>(0);

				// Outputs
				blueprint.m_CraftedResult = GetGearItemPrefab(SCRAP_METAL_NAME);
				blueprint.m_CraftedResultCount = 3;

				// Process
				blueprint.m_Locked = false;
				blueprint.m_AppearsInStoryOnly = false;
				blueprint.m_RequiresLight = true;
				blueprint.m_RequiresLitFire = false;
				blueprint.m_RequiredCraftingLocation = CraftingLocation.Forge;
				blueprint.m_DurationMinutes = 30;
				blueprint.m_CraftingAudio = "PLAY_CraftingMetal";
				blueprint.m_AppliedSkill = SkillType.None;
				blueprint.m_ImprovedSkill = SkillType.None;
			}

			private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
			private static ToolsItem GetToolsItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<ToolsItem>();
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
				if (bpi?.m_CraftedResult?.name == ARROWHEAD_NAME && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
				if (bpi?.m_CraftedResult?.name == COOKING_POT_NAME && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
				if (bpi?.m_CraftedResult?.name == PRYBAR_NAME && __instance.m_CurrentCategory == Panel_Crafting.Category.Tools)
				{
					__result = true;
				}
			}
		}

		[HarmonyPatch(typeof(BlueprintDisplayItem), "Setup")]
		private static class FixRecipeIcons
		{
			internal static void Postfix(BlueprintDisplayItem __instance, BlueprintItem bpi)
			{
				//Scrap Metal Crafting Icon fix
				if (bpi?.m_CraftedResult?.name == SCRAP_METAL_NAME)
				{
					Texture2D scrapMetalTexture = Utils.GetCachedTexture(SCRAP_METAL_CRAFTING_ICON_NAME);
					if (!scrapMetalTexture)
					{
						scrapMetalTexture = ForgeBlueprintsMod.assetBundle.LoadAsset(SCRAP_METAL_CRAFTING_ICON_NAME).Cast<Texture2D>();
						Utils.CacheTexture(SCRAP_METAL_CRAFTING_ICON_NAME, scrapMetalTexture);
					}
					__instance.m_Icon.mTexture = scrapMetalTexture;
				}
				//Prybar Crafting Icon fix
				if (bpi?.m_CraftedResult?.name == PRYBAR_NAME)
				{
					Texture2D prybarTexture = Utils.GetCachedTexture(PRYBAR_CRAFTING_ICON_NAME);
					if (!prybarTexture)
					{
						prybarTexture = ForgeBlueprintsMod.assetBundle.LoadAsset(PRYBAR_CRAFTING_ICON_NAME).Cast<Texture2D>();
						Utils.CacheTexture(PRYBAR_CRAFTING_ICON_NAME, prybarTexture);
					}
					__instance.m_Icon.mTexture = prybarTexture;
				}
				//Cooking Pot Crafting Icon fix
				if (bpi?.m_CraftedResult?.name == COOKING_POT_NAME)
				{
					Texture2D cookingPotTexture = Utils.GetCachedTexture(COOKING_POT_CRAFTING_ICON_NAME);
					if (!cookingPotTexture)
					{
						cookingPotTexture = ForgeBlueprintsMod.assetBundle.LoadAsset(COOKING_POT_CRAFTING_ICON_NAME).Cast<Texture2D>();
						Utils.CacheTexture(COOKING_POT_CRAFTING_ICON_NAME, cookingPotTexture);
					}
					__instance.m_Icon.mTexture = cookingPotTexture;
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
