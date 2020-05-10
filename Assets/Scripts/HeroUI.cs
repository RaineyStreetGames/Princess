using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroUI : MonoBehaviour
{
    public Canvas canvas;
    private Dictionary<string, GameObject> uiComponents;



    // Start is called before the first frame update
    void Start()
    {
        uiComponents = new Dictionary<string, GameObject>();
    }

    public void AddUIComponent(GameObject ui)
    {
        if (!uiComponents.ContainsKey(ui.name))
        {
            var pos = new Vector3(0, canvas.GetComponent<RectTransform>().rect.height / 4, 0);
            var component = Instantiate(ui, pos, Quaternion.Euler(Vector3.zero));
            component.transform.SetParent(canvas.transform, false);
            uiComponents.Add(ui.name, component);
        }
    }

    public void RemoveUIComponent(GameObject ui)
    {
        if (uiComponents.ContainsKey(ui.name))
        {
            var component = uiComponents[ui.name];
            uiComponents.Remove(ui.name);

            var button = component.GetComponent<Button>();
            if (button != null)
                button.End();
            else
                Destroy(component);
        }
    }
}
