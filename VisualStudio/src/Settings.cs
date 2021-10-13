using ModSettings;
using UnityEngine;

namespace ForgeBlueprints
{
	internal class ForgeModSettings : JsonModSettings
	{
		public static ForgeModSettings options= new ForgeModSettings();

		[Section("Cooking Pot")]
		[Name("Weight")]
		[Description("Default is 1 kg. Affects crafting requirements and harvesting yields. Changes take effect on scene change.")]
		[Slider(0.3f, 2f, 18)]
		public float cookingPotWeight = 1f;

		[Section("Prybar")]
		[Name("Weight")]
		[Description("Default is 1 kg. Affects crafting requirements and harvesting yields. Changes take effect on scene change.")]
		[Slider(0.3f, 2f, 18)]
		public float prybarWeight = 1f;

		protected override void OnConfirm()
		{
			base.OnConfirm();
			ChangePrefabWeights();
		}
		internal void ChangePrefabWeights()
		{
			GetGearItemPrefab("GEAR_Prybar").m_WeightKG = prybarWeight;
			GetGearItemPrefab("GEAR_CookingPot").m_WeightKG = cookingPotWeight;
		}
		private static GearItem GetGearItemPrefab(string name) => Resources.Load(name).Cast<GameObject>().GetComponent<GearItem>();
	}
}
