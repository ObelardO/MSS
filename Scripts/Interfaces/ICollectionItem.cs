namespace Obel.MSS
{
    public interface ICollectionItem
    {
        #region Properties

        CollectionItem Parent { get; }
        int ID { get; }

        void Init(CollectionItem parent);

        #endregion
    }
}
