using UnityEngine;

public class Button : MonoBehaviour
{
    private CanvasRenderer canvas;
    public Vector3 target;
    private Vector3 velocity = Vector3.zero;

    private RectTransform rect;

    void Start()
    {
        canvas = GetComponent<CanvasRenderer>();
        rect = GetComponent<RectTransform>();
        target = rect.anchoredPosition;
        canvas.SetAlpha(0);
        StartCoroutine(canvas.FadeIn(0.25f));
    }

    public void End()
    {
        Destroy(gameObject, 0.25f);
        StartCoroutine(canvas.FadeOut(0.25f));
    }

    public void SetPosition(Vector3 pos)
    {
        target = pos;
        StartCoroutine(rect.DelayUpdatePosition(0.5f, pos));
    }
}
