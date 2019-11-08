using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obel.MSS;
using Obel.MSS.Modules.Tweens;

public class StatesTester : MonoBehaviour
{
    public States States;

    // Start is called before the first frame update
    void Start()
    {
        States.Group.CreateState().CreateTween<TweenGraphic>().Capture();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
