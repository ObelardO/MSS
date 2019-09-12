using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [System.Serializable]
    public class Collection<T> : CollectionItem where T : CollectionItem, new()
    {
        #region Properties

        [SerializeReference]
        public List<T> items = new List<T>();

        public int Count => items.Count;
        public T Last => items[Count - 1];
        public T this[int i] => items[i];
        public T First => items[0];

        #endregion

        #region Unity methods

        private void OnEnable()
        {
            //InitItems();
            //OnInit();
        }

        #endregion

        #region Collection methods

        private void InitItems()
        {
            //if (items == null) items = new List<T>();
            //OnInit();
        }

        //public virtual void OnInit() { }

        public void ForEach(Action<T> forEachCallback)
        {
            items.ToList().ForEach(item => forEachCallback(item));
        }

        public T AddNew()
        {
            Add(new T());
            // (T)Activator.CreateInstance(typeof(T))/* CreateInstance<T>()*/);
            Last.Init(this);
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
            //if (destroyItem) DestroyImmediate(item); TODO  dispose
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

        public virtual T Find(int id)
        {
            return items.Where(i => i.ID == id).FirstOrDefault();
        }

        private bool IndexInvalid(int index)
        {
            return items == null || index < 0 || index > Count - 1;
        }

        #endregion
    }

    public interface ICollectionItem
    {

    }

    [System.Serializable]
    public class CollectionItem : ICollectionItem// : ICollectionItem// : ScriptableObject
    {
        #region Properties

        [SerializeReference/*, HideInInspector*/]
        private ICollectionItem s_Parent;
        public ICollectionItem Parent
        {
            private set => s_Parent = value;
            get => s_Parent;
        }

        public int a;

        [SerializeField/*, HideInInspector*/]
        private int s_ID;

        //[field: SerializeField]
        public int ID //{ private set; get; }
        {
            private set => s_ID = value;
            get => s_ID;
        }

        #endregion

        #region Public methods

        public void Init(ICollectionItem parent)
        {
            Debug.Log("new item! parent: " + parent);
            Parent = parent ?? this;
            ID = base.GetHashCode();
            OnInit();
        }

        public virtual void OnInit() { }

        #endregion
    }
}