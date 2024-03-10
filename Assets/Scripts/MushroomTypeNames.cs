using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class MushroomTypeNames
    {
        public static Dictionary<MushroomType, string> myEnumDescriptions = new Dictionary<MushroomType, string>()
        {
            { MushroomType.BloodyFairy, "Bloody Fairy" },
            { MushroomType.Button, "Button" }
        };
    }
}