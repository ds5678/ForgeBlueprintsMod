using ModSettings;

namespace ForgeBlueprints
{
    internal class ForgeModSettings : JsonModSettings
    {
        [Section("Cooking Pot")]
        [Name("Weight")]
        [Description("Default is 1 kg. Affects crafting requirements.")]
        [Slider(0.3f, 2f, 18)]
        public float cookingPotWeight = 1f;

        [Section("Prybar")]
        [Name("Weight")]
        [Description("Default is 1 kg. Affects crafting requirements.")]
        [Slider(0.3f, 2f, 18)]
        public float prybarWeight = 1f;
    }
    internal static class Settings
    {
        public static ForgeModSettings options;
        public static void OnLoad()
        {
            options = new ForgeModSettings();
            options.AddToModSettings("Forge Blueprints Mod");
        }
    }
}