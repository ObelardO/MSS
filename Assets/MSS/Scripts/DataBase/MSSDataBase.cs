using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Obel.MSS
{
    /*
        DataBase <- Collection, Item
        - StateGroupData <- Collection, Item
          - StateData <- Collection, Item
            - TweenData <- Item
    */

    [Serializable]
    public class MSSDataBaseCollection<T> : MSSDataBaseCollectionItem
        where T : MSSDataBaseCollectionItem
    {
        [SerializeField]
        protected List<T> items;

        public int Count => items.Count;
        public T Last => items[Count - 1];
        public T this[int i] => items[i];

        public void ForEach(Action<T> forEachCallback)
        {
            for (int i = Count - 1; i > -1; i--) forEachCallback(items[i]);
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

    public interface IMSSDataBaseCollectionItem
    {

    }

    public class MSSDataBaseCollectionItem : ScriptableObject, IMSSDataBaseCollectionItem
    {

    }

    [Serializable]
    public class MSSDataBase : MSSDataBaseCollection<MSSStateGroupData>
    {
        public void OnEnable()
        {
            if (items == null)
                items = new List<MSSStateGroupData>();
        }

        public override MSSStateGroupData Find(object id)
        {
            foreach (MSSStateGroupData item in items)
                if (item.objectID == (int)id) return item;

            return null;
        }
    }
}
