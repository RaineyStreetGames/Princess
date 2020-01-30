using UnityEngine;

public class Button : MonoBehaviour
{
    private CanvasRenderer canvas;
    void Start()
    {
        canvas = GetComponent<CanvasRenderer>();
        if (canvas != null)
        {
            canvas.transform.localScale = Vector3.zero;
            StartCoroutine(canvas.transform.ScaleIn(0.5f));
            canvas.SetAlpha(0);
            StartCoroutine(canvas.FadeIn(0.5f));
        }
    }

    public void End()
    {
        Destroy(this, 0.5f);
        // StartCoroutine(transform.ScaleOut(0.5f));
        var renderer = GetComponent<CanvasRenderer>();
        if (renderer != null)
            StartCoroutine(renderer.FadeOut(0.5f));
    }
}
