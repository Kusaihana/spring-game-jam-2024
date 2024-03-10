namespace DefaultNamespace
{
    public class CollectionItem
    {
        public MushroomType Id { get; set; }
        public int Amount{ get; set; }

        public CollectionItem(MushroomType collectionId, int amount)
        {
            Id = collectionId;
            Amount = amount;
        }
    }
}