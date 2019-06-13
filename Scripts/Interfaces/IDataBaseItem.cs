using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS.Editor
{
    public interface IDataBaseEditor<T> where T : DBCollectionItem
    {
        T Add();

        void Remove(T item);
    }
}
