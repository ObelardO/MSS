using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Obel.MSS.Base
{
    [Serializable]
    public class Collection<T> : CollectionItem where T : CollectionItem, new()
    {
        #region Properties

        [SerializeReference] // can't be readonly - editor Undo can't fill readonly fields
        private List<T> items = new List<T>();

        public IReadOnlyList<T> Items => items;
        public int Count => items.Count;
        public T Last => items.LastOrDefault();
        public T this[int i] => IndexInvalid(i) ? null : items[i];
        public T First => items.FirstOrDefault();

        #endregion

        #region Collection methods

        public T Create()
        {
            Add(new T());
            return Last;
        }

        public void Add(T item)
        {
            items.Add(item);
            Last.Init(this);
        }
        /*
        public void Remove(int index, bool destroyItem = true)
        {
            if (IndexInvalid(index)) return;
            Remove(items[index], destroyItem);
        }
        */
        public void Remove(T item, bool destroyItem = true)
        {
            if (!items.Contains(item)) return;
            items.Remove(item);
        }
        /*
        public void Clear(bool destroyItem = true)
        {
            items.ForEach(item => Remove(item, destroyItem));
        }
        */
        public T Get(int index)
        {
            return IndexInvalid(index) ? null : items[index];
        }

        private bool IndexInvalid(int index)
        {
            return items == null || index < 0 || index > Count - 1;
        }

        #endregion
    }

    [Serializable]
    public class CollectionItem : ICollectionItem
    {
        #region Properties

        [field: SerializeField]
        public int Id { private set; get; }

        [field: SerializeField]
        public virtual string Name { set; get; }

        public bool Enabled = true;

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
            Debug.Log($"[MSS] [Data] Registered: {Name} Parent: {parent}");
            Parent = parent;
            Id = base.GetHashCode();
            OnInit();
        }

        public virtual void OnInit() { }

        #endregion
    }
}