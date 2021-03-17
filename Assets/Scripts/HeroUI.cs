using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroUI : MonoBehaviour
{
    public Canvas canvas;
    public List<(string, Button)> uiComponents;
    private List<float> indexes;
    public float y = 32;

    // Start is called before the first frame update
    void Start()
    {
        uiComponents = new List<(string, Button)>();
        indexes = new List<float>() { 217, 290, 358, 430, 502, 572 };
    }

    public void AddUIComponent(string name, Button ui)
    {
        if (!uiComponents.Exists(x => x.Item1 == name))
        {
            if (uiComponents.Count < indexes.Count)
            {
                var x = indexes[uiComponents.Count];
                // Debug.Log("AddUIComponent " + name);
                var pos = new Vector3(x, y, 0);
                var component = Instantiate(ui, pos, Quaternion.Euler(Vector3.zero));
                component.transform.SetParent(canvas.transform, false);
                component.transform.SetSiblingIndex(1);
                uiComponents.Add((name, component));
            }
        }
    }

    public void RemoveUIComponent(string name)
    {
        if (uiComponents.Exists(x => x.Item1 == name))
        {
            // Debug.Log("RemoveUIComponent " + name);
            var index = uiComponents.FindIndex(x => x.Item1 == name);
            if (index >= 0)
            {
                var (_, component) = uiComponents[index];
                component.End();
                uiComponents.RemoveAt(index);

                for (int i = index; i < uiComponents.Count; i++)
                {
                    var (n, c) = uiComponents[i];
                    // Debug.Log("MovingUIComponent " + n);
                    c.SetPosition(new Vector3(indexes[i], y, 0));
                }
            }
        }
    }
}
