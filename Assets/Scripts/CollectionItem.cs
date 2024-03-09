namespace DefaultNamespace
{
    public class CollectionItem
    {
        public string Name { get; set; }
        public int Amount{ get; set; }
        public CollectionItem(string collectionName, int amount)
        {
            Name = collectionName;
            Amount = amount;
        }
    }
}