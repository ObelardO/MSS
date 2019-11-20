using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Obel.MSS
{
    public struct StatesData : IComponentData
    {
        public Group Group;
    }
    
    public class StatesSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            //TODO Nope.. only structs i think..
            /*
            Entities.WithAll<Group>().ForEach((Entity states, ref Group data) =>
                {
                    Debug.Log(data.gameObject.name);
                });
            */
        }
    }
}


