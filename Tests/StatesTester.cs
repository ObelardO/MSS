using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obel.MSS;
using Obel.MSS.Modules.Tweens;
using Obel.MSS.Data;

public class StatesTester : MonoBehaviour
{
    public States States;

    public void Select(int id)
    {
        States.Group[id]?.Apply();
    }

    // Start is called before the first frame update
    void Start()
    {
        States.Group.CreateState().CreateTween<TweenRotation>().Capture();

        States.Group.Last.CreateTween<Tween>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
