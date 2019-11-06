namespace Obel.MSS
{
    public interface ICollectionItem
    {
        ICollectionItem Parent { get; }
        int Id { get; }
        string Name { get; }
    }
}