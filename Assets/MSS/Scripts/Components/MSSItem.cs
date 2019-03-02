using System;
using System.Collections.Generic;
using UnityEngine;

namespace Obel.MSS
{
    [System.Serializable, ExecuteInEditMode, DisallowMultipleComponent, AddComponentMenu("MSS/Item")]
    public class MSSItem : MonoBehaviour
    {
        [SerializeField]
        public /*u*/int dataBaseID = /*u*/int.MaxValue; 
    }
}
