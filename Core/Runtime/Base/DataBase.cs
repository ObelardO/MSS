using System;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class Collection<T> : CollectionItem where T : CollectionItem, new()
    {
        #region Properties

        [SerializeReference]
        public /*readonly*/ List<T> items = new List<T>();

        public int Count => items.Count;
        public T Last => Count > 0 ? items[Count - 1] : null;
        public T this[int i] => IndexInvalid(i) ? null : items[i];
        public T First => Count > 0 ? items[0] : null; 

        #endregion

        #region Collection methods

        public void ForEach(Action<T> forEachCallback)
        {
            items.ForEach(item => forEachCallback(item));
        }

        public T AddNew()
        {
            Add(new T());
            return Last;
        }

        public void Add(T item)
        {
            items.Add(item);
            Last.Init(this);
        }

        public void Remove(int index, bool destroyItem = true)
        {
            if (IndexInvalid(index)) return;
            Remove(items[index], destroyItem);
        }

        public void Remove(T item, bool destroyItem = true)
        {
            if (!Contains(item)) return;
            items.Remove(item);
        }

        public void Clear(bool destroyItem = true)
        {
            ForEach(item => Remove(item, destroyItem));
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public T Get(int index)
        {
            if (IndexInvalid(index)) return null;
            return items[index];
        }

        private bool IndexInvalid(int index)
        {
            return items == null || index < 0 || index > Count - 1;
        }

        #endregion
    }

    [Serializable]
    public class CollectionItem : ICollectionItem// : ICollectionItem// : ScriptableObject
    {
        #region Properties

        [field: SerializeField]
        public int ID { private set; get; }

        [field: SerializeField]
        public virtual string Name { set; get; }

        //[field: SerializeField]
        public bool enabled = true;// { private set; get; }

        [SerializeReference]
        private ICollectionItem s_Parent;
        public ICollectionItem Parent// { private set; get; }
        {
            private set => s_Parent = value;
            get => s_Parent;
        }

        #endregion

        #region Public methods

        public void Init(ICollectionItem parent)
        {
            Debug.Log($"[MSS] [DataBase] Registred: {Name} Parent: {parent}");
            Parent = parent;// ?? this;
            ID = base.GetHashCode();
            OnInit();
        }

        public virtual void OnInit() { }

        #endregion
    }
}