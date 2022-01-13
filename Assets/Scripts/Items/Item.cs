namespace CarGame2D
{
    public enum CarPartType
    {
        Body,
        Wheel,
        Accelerator
    }
    public class Item : IItem
    {        
        public int Id { get; }
        public ItemInfo Info { get; }
        public Item(int id, ItemInfo info)
        {
            Id = id;
            Info = info;
        }

    }
}
