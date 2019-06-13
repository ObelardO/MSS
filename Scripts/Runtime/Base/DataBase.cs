using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [Serializable]
    public class DBCollection<T> : DBCollectionItem
        where T : DBCollectionItem
    {
        [SerializeField]
        protected List<T> items;

        public int Count => items.Count;
        public T Last => items[Count - 1];
        public T this[int i] => items[i];
        public T First => items[0];

        public virtual void Init()
        {
            if (items == null) items = new List<T>();
            ForEach(item => item.Init(this));
        }

        public void OnEnable()
        {
            Init();
        }

        public void ForEach(Action<T> forEachCallback)
        {
            items.ToList().ForEach(item => forEachCallback(item));
        }

        public T AddNew()
        {
            items.Add(CreateInstance<T>());
            return Last;
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Remove(T item, bool destroyItem = true)
        {
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

        public virtual T Find(object id)
        {
            return null;
        }
    }

    public interface IMSSCollectionItem
    {

    }

    public class DBCollectionItem : ScriptableObject, IMSSCollectionItem
    {
        public DBCollectionItem parent;

        public void Init(DBCollectionItem parent)
        {
            this.parent = parent;
        }
    }

    [Serializable]
    public class DataBase : DBCollection<StateGroup>
    {
        public static DataBase instance;

        public override StateGroup Find(object id)
        {
            foreach (StateGroup item in items)
                if (item.objectID == (int)id) return item;

            return null;
        }
    }
}