namespace DefaultNamespace
{
    public static class Dish
    {
        public static readonly string[] RecipeName = {
            "Stuffed Portobello Mushrooms",
            "Punky Button Burgers",
            "Mushroom Stir-Fry",
        };
        
        public static readonly string[] TextList = {
            "In the darkness, they quietly grow,\nOn forest floors where secrets flow.\nWith portobello caps, big and bold,\nGather them well, for a feast untold.",
            "In the urban jungle, where rebels roam,\nA burger bold, a punk's true home.\nWith button mushrooms, plump and round,\nGather them up, for a burger renowned.",
            "In the undergrowth, where shadows dance,\nFind the fungi, take a chance.\nWith portobello, button, or more to choose,\nGather mushrooms, a handful to infuse.",
        };

        //each recipe is one line
        public static CollectionItem[][] RecipeRequirements =
        {
            new[] { new CollectionItem(MushroomType.Portobello, 2) },
            new[] { new CollectionItem(MushroomType.Button, 3) },
            new[] { new CollectionItem(MushroomType.Portobello, 2), new CollectionItem(MushroomType.Button, 2)},
        };
    }
}