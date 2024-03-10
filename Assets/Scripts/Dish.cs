namespace DefaultNamespace
{
    public static class Dish
    {
        public static readonly string[] TextList = {
            "In forests deep where shadows play,\nGather treasures, don't delay.\nWith mushrooms peach, a handful or two,\nBlend them well, for a dish anew."
        };
        public static readonly string[] RequirementsList = {
            "2 peach mushrooms"
        };

        //each recipe is one line
        public static CollectionItem[][] RecipeRequirements =
        {
            new[] { new CollectionItem(MushroomType.Button, 1), new CollectionItem(MushroomType.BloodyFairy, 2) },
        };
    }
}