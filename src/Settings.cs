using ModSettings;

namespace ForgeBlueprints
{
    internal class ForgeModSettings : JsonModSettings
    {
        [Section("Cooking Pot")]
        [Name("Weight")]
        [Description("Default is 1 kg.")]
        [Slider(0f, 2f, 21)]
        public float cookingPotWeight = 1.5f;

        [Name("Scrap Metal Required")]
        [Description("The number of scrap metal required to craft a cooking pot.")]
        [Slider(1, 10)]
        public int cookingPotScrapMetal = 6;

        [Section("Prybar")]
        [Name("Weight")]
        [Description("Default is 1 kg.")]
        [Slider(0f, 2f, 21)]
        public float prybarWeight = 1f;

        [Name("Scrap Metal Required")]
        [Description("The number of scrap metal required to craft a prybar.")]
        [Slider(1, 10)]
        public int prybarScrapMetal = 6;
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