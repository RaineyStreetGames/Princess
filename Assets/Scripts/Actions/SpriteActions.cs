using UnityEngine;
using System.Collections;

public static class SpriteActions
{
    public static IEnumerator FadeOut(this SpriteRenderer spriteRenderer, float FadeTime)
    {
        Color startColor = spriteRenderer.color;

        while (spriteRenderer != null && spriteRenderer.color.a > 0)
        {
            float a = spriteRenderer.color.a - startColor.a * Time.deltaTime / FadeTime;
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, a);
            yield return null;
        }
    }

    public static IEnumerator FadeIn(this SpriteRenderer spriteRenderer, float FadeTime)
    {
        Color startColor = spriteRenderer.color;

        while (spriteRenderer != null && spriteRenderer.color.a < 1.0f)
        {
            float a = spriteRenderer.color.a + Time.deltaTime / FadeTime;
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, a);
            yield return null;
        }
    }
}
