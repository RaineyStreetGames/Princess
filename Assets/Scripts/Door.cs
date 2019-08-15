using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    Open,
    Hidden,
    Cleared,
}

public class Door : MonoBehaviour
{
    public DoorState state = DoorState.Open;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case DoorState.Open:

                break;
            case DoorState.Hidden:

                break;
            case DoorState.Cleared:

                break;
        }
    }
}
