using UnityEngine;

public class Button : MonoBehaviour
{
    private CanvasRenderer canvas;
    private Vector3 target;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        canvas = GetComponent<CanvasRenderer>();
        target = transform.position;
        if (canvas != null)
        {
            canvas.transform.localScale = Vector3.zero;
            StartCoroutine(canvas.transform.ScaleIn(0.5f));
            canvas.SetAlpha(0);
            StartCoroutine(canvas.FadeIn(0.5f));
        }
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.3f);
    }

    public void End()
    {
        Destroy(gameObject, 0.5f);
        // StartCoroutine(transform.ScaleOut(0.5f));
        var renderer = GetComponent<CanvasRenderer>();
        if (renderer != null)
            StartCoroutine(renderer.FadeOut(0.5f));
    }

    public void SetPosition(Vector3 pos)
    {
        target = pos;
    }
}
