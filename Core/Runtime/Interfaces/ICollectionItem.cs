namespace Obel.MSS.Data
{
    public interface ICollectionItem
    {
        ICollectionItem Parent { get; }
        int Id { get; }
        string Name { get; }
    }
}