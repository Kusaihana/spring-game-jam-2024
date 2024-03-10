using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class MushroomTypeNames
    {
        public static Dictionary<MushroomType, string> myEnumDescriptions = new Dictionary<MushroomType, string>()
        {
            { MushroomType.BloodyFairy, "Bleeding Fairy Helmet Mushroom" },
            { MushroomType.Button, "Button Mushroom" },
            { MushroomType.Portobello, "Portobello Mushroom" }
        };
    }
}