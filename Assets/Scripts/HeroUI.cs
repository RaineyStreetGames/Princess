using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroUI : MonoBehaviour
{
    public Canvas canvas;
    public Dictionary<string, Button> uiComponents;

    private float width;
    private float height;



    // Start is called before the first frame update
    void Start()
    {
        uiComponents = new Dictionary<string, Button>();
        width = canvas.GetComponent<RectTransform>().rect.width;
        height = canvas.GetComponent<RectTransform>().rect.height;
    }

    public void AddUIComponent(string name, Button ui)
    {
        if (!uiComponents.ContainsKey(name))
        {
            Debug.Log("AddUIComponent " + name);
            var pos = new Vector3(width / 2, height / 4, 0);
            var component = Instantiate(ui, pos, Quaternion.Euler(Vector3.zero));
            component.transform.SetParent(canvas.transform, false);
            uiComponents.Add(name, component);
            rearrangeButtons();
        }
    }

    public void RemoveUIComponent(string name)
    {
        if (uiComponents.ContainsKey(name))
        {
            Debug.Log("RemoveUIComponent " + name);
            var component = uiComponents[name];
            uiComponents.Remove(name);

            var button = component.GetComponent<Button>();
            if (button != null)
                button.End();
            else
                Destroy(component);

            rearrangeButtons();
        }
    }

    private void rearrangeButtons()
    {
        var init = width / (uiComponents.Count() + 1);
        var i = 0;
        foreach (var b in uiComponents.Values)
        {
            b.SetPosition(new Vector3(init + (init * i), height / 4, 0));
            i++;
        }

    }
}
