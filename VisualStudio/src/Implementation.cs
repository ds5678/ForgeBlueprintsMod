using System.IO;
using MelonLoader;
using UnityEngine;

namespace ForgeBlueprints
{

	internal class ForgeBlueprintsMod : MelonMod
	{
		public override void OnApplicationStart()
		{
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
			Settings.OnLoad();
		}
	}
}
