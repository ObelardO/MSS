using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class Collection<T> : CollectionItem where T : CollectionItem
    {
        #region Properties

        [SerializeField]
        public List<T> items;

        public int Count => items.Count;
        public T Last => items[Count - 1];
        public T this[int i] => items[i];
        public T First => items[0];

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            InitItems();
        }

        #endregion

        #region Collection methods

        private void InitItems()
        {
            if (items == null) items = new List<T>();
            OnInit();
        }

        public virtual void OnInit() { }

        public void ForEach(Action<T> forEachCallback)
        {
            items.ToList().ForEach(item => forEachCallback(item));
        }

        public T AddNew()
        {
            Add(CreateInstance<T>());
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
            if (destroyItem) DestroyImmediate(item);
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

        public virtual T Find(object id)
        {
            return null;
        }

        private bool IndexInvalid(int index)
        {
            return items == null || index < 0 || index > Count - 1;
        }

        #endregion
    }

    public class CollectionItem : ScriptableObject, ICollectionItem
    {
        #region Properties

        [SerializeField, HideInInspector]
        private CollectionItem s_Parent;
        public CollectionItem Parent
        {
            private set { s_Parent = value; }
            get { return s_Parent; }
        }

        [SerializeField, HideInInspector]
        private int s_ID;
        public int ID
        {
            private set { s_ID = value; }
            get { return s_ID; }
        }

        #endregion

        public void Init(CollectionItem parent)
        {
            Parent = parent ?? (this);
            ID = base.GetHashCode();
        }
    }
}