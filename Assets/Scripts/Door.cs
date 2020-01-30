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

    public GameObject uiObject;

    void Start()
    {

    }

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

    private void OnTriggerEnter(Collider collider)
    {
        var hero = collider.gameObject.GetComponent<Hero>();
        if (hero != null)
        {
            hero.AddUIComponent(uiObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        var hero = collider.gameObject.GetComponent<Hero>();
        if (hero != null)
        {
            hero.RemoveUIComponent(uiObject);
        }
    }
}
