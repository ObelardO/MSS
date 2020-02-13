using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS.Data
{
    [Serializable]
    public abstract class Collection<T> : CollectionItem where T : CollectionItem, new()
    {
        #region Properties

        [SerializeReference]
        private List<T> _items = new List<T>();

        public IReadOnlyList<T> Items => _items;
        public IReadOnlyList<T> EnabledItems => _items.Where(i => i.Enabled).ToList();
        public int Count => _items.Count;
        public T this[int i] => Get(i);
        public T Last => _items.LastOrDefault();
        public T First => _items.FirstOrDefault();

        #endregion

        #region Public methods

        public void ForEach(Action<T> callback) => _items.ForEach( callback);

        public void ForEachEnabled(Action<T> callback) => EnabledItems.ToList().ForEach(callback);

        public void ForEachRemove(Action<T> callback)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                callback(_items[i]);
                Remove(_items[i]);
            }
        }

        public T Get(int index) => IndexInvalid(index) ? null : _items[index];

        #endregion

        #region Private methods

        protected T Create() => Add(new T());

        protected T Add(T item)
        {
            _items.Add(item);
            Last.Init(this);
            return Last;
        }

        protected bool Remove(T item)
        {
            if (!_items.Contains(item)) return false;
            return _items.Remove(item);
        }

        protected 

        private bool IndexInvalid(int index) => _items == null || index < 0 || index > Count - 1;
  
        #endregion
    }

    [Serializable]
    public abstract class CollectionItem : ICollectionItem
    {
        #region Properties

        [field: SerializeField]
        public int Id { private set; get; }

        [field: SerializeField]
        public virtual string Name { set; get; }

        [field: SerializeField]
        public virtual bool Enabled { set; get; }

        [SerializeReference]
        private ICollectionItem _parent;
        public ICollectionItem Parent
        {
            private set => _parent = value;
            get => _parent;
        }

        #endregion

        #region Public methods

        public void Init(ICollectionItem parent)
        {
            if (_parent != null) return;
            Debug.Log($"[MSS] [Data] Registered: {Name} Parent: {parent}");
            Parent = parent;
            Id = base.GetHashCode();
            Enabled = true;
            OnInit();
        }

        public virtual void OnInit() { }

        #endregion
    }
}