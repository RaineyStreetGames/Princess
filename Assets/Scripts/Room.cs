using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    private Light[] lights;
    void Start()
    {
        lights = gameObject.GetComponentsInChildren<Light>();
        Reset();
    }

    void Reset()
    {
        foreach (var light in lights)
        {
            light.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
