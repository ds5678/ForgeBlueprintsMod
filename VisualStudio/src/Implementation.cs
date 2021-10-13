using MelonLoader;
using UnityEngine;

namespace ForgeBlueprints
{
	public static class BuildInfo
	{
		public const string Name = "ForgeBlueprintsMod"; // Name of the Mod.  (MUST BE SET)
		public const string Description = "A couple more blueprints at the forge."; // Description for the Mod.  (Set as null if none)
		public const string Author = "ds5678"; // Author of the Mod.  (MUST BE SET)
		public const string Company = null; // Company that made the Mod.  (Set as null if none)
		public const string Version = "1.5.0"; // Version of the Mod.  (MUST BE SET)
		public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
	}
	internal class ForgeBlueprintsMod : MelonMod
	{
		public override void OnApplicationStart()
		{
			Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
			ForgeModSettings.options.AddToModSettings("Forge Blueprints Mod");
			ForgeModSettings.options.ChangePrefabWeights();
		}
	}
}
