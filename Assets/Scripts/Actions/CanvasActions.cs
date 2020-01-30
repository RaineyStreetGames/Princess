using UnityEngine;
using System.Collections;

public static class CanvasActions
{
    public static IEnumerator FadeOut(this CanvasRenderer canvasRenderer, float FadeTime)
    {
        var startAlpha = canvasRenderer.GetAlpha();
        while (canvasRenderer != null && canvasRenderer.GetAlpha() > 0)
        {
            canvasRenderer.SetAlpha(canvasRenderer.GetAlpha() - startAlpha * Time.deltaTime / FadeTime);
            yield return null;
        }
    }

    public static IEnumerator FadeIn(this CanvasRenderer canvasRenderer, float FadeTime)
    {
        while (canvasRenderer != null && canvasRenderer.GetAlpha() < 1.0f)
        {
            canvasRenderer.SetAlpha(canvasRenderer.GetAlpha() + Time.deltaTime / FadeTime);
            yield return null;
        }
    }
}
