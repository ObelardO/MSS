using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS.Base
{
    [Serializable]
    public abstract class Collection<T> : CollectionItem where T : CollectionItem, new()
    {
        #region Properties

        [SerializeReference] // can't be readonly - editor Undo can't fill readonly fields
        private List<T> items = new List<T>();

        public IReadOnlyList<T> Items => items;
        public int Count => items.Count;
        public T this[int i] => IndexInvalid(i) ? null : items[i];
        public T Last => items.LastOrDefault();
        public T First => items.FirstOrDefault();

        #endregion

        #region Public methods

        public void ForEach(Action<T> callback) => items.ForEach(callback);

        public void ForEachEnabled(Action<T> callback) => items.Where(i => i.Enabled).ToList().ForEach(callback);

        public void Remove(T item)
        {
            if (!items.Contains(item)) return;
            items.Remove(item);
        }

        public T Get(int index) => IndexInvalid(index) ? null : items[index];

        #endregion

        #region Private methods

        protected T Create() => Add(new T());

        protected T Add(T item)
        {
            items.Add(item);
            Last.Init(this);
            return Last;
        }

        private bool IndexInvalid(int index) => items == null || index < 0 || index > Count - 1;
  
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