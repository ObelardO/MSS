using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using UnityEditor;

public class TestSCR : MonoBehaviour
{

    [SerializeField]
    public List<Generic> gl = new List<Generic>();

    // Start is called before the first frame update
    void Start()
    {
        gl.Add(new GenericInt());
        gl.Add(new GenericFloat());

        Debug.Log(gl.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


[Serializable]
public abstract class Generic { }

[Serializable]
public abstract class Generic<T> : Generic
{
    public T value;
}
[Serializable]
public class GenericInt : Generic<int> { }
[Serializable]
public class GenericFloat : Generic<float> { }

