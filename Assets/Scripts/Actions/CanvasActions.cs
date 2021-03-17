using UnityEngine;
using System.Collections;

public static class CanvasActions
{
    public static IEnumerator FadeOut(this CanvasRenderer canvasRenderer, float fadeTime)
    {
        var startAlpha = canvasRenderer.GetAlpha();
        while (canvasRenderer != null && canvasRenderer.GetAlpha() > 0)
        {
            canvasRenderer.SetAlpha(canvasRenderer.GetAlpha() - startAlpha * Time.deltaTime / fadeTime);
            yield return null;
        }
    }

    public static IEnumerator FadeIn(this CanvasRenderer canvasRenderer, float fadeTime)
    {
        while (canvasRenderer != null && canvasRenderer.GetAlpha() < 1.0f)
        {
            canvasRenderer.SetAlpha(canvasRenderer.GetAlpha() + Time.deltaTime / fadeTime);
            yield return null;
        }
    }

    public static IEnumerator FadeOutIn(this CanvasRenderer canvasRenderer, float fadeTime)
    {
        yield return new WaitForSeconds(fadeTime);

        yield return FadeOut(canvasRenderer, fadeTime);

        yield return FadeIn(canvasRenderer, fadeTime);
    }
}
