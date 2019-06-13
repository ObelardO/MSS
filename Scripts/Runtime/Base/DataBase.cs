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
        public List<T> items;

        public int Count => items.Count;
        public T Last => items[Count - 1];
        public T this[int i] => items[i];
        public T First => items[0];

        private void InitItems()
        {
            if (items == null) items = new List<T>();
            OnInit();
        }

        public virtual void OnInit() { }

        private void OnEnable()
        {
            InitItems();
        }

        public void ForEach(Action<T> forEachCallback)
        {
            items.ToList().ForEach(item => forEachCallback(item));
        }

        public T AddNew()
        {
            items.Add(CreateInstance<T>());
            Last.Init(this, items.Count);
            return Last;
        }

        public void Add(T item)
        {
            items.Add(item);
            Last.Init(this, items.Count);
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

        public T Get(int id)
        {
            if (id < 0 || id > Count - 1) return null;

            return items[id];
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
        [SerializeField] private DBCollectionItem _parent;
        public DBCollectionItem parent
        {
            private set { _parent = value; }
            get { return _parent; }
        }

        [SerializeField] private int _id;
        public int id
        {
            private set { _id = value; }
            get { return _id;  }
        }

        public void Init(DBCollectionItem parent, int id)
        {
            Debug.Log("INITED: " + name);

            this.parent = parent;
            this.id = id;
        }
    }

    [Serializable]
    public class DataBase : DBCollection<StatesGroup>
    {
        public static DataBase instance;

        public override StatesGroup Find(object id)
        {
            foreach (StatesGroup item in items)
                if (item.objectID == (int)id) return item;

            return null;
        }
    }
}