
namespace CarGame2D
{
    //учитывать категории предметов (окна, колёса, и т.д.)
    public interface IItem
    {
        int Id { get; }

        ItemInfo Info {get; }
    }
}
