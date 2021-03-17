using UnityEngine;
using System.Collections;

public static class TransformActions
{
    public static IEnumerator ScaleOut(this Transform transform, float FadeTime)
    {
        while (transform != null && transform.localScale != Vector3.zero)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, FadeTime);
            yield return null;
        }
    }

    public static IEnumerator ScaleIn(this Transform transform, float FadeTime)
    {
        while (transform != null && transform.localScale != Vector3.one)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, FadeTime);
            yield return null;
        }
    }

    public static IEnumerator DelayUpdatePosition(this RectTransform transform, float delayTime, Vector3 pos)
    {
        yield return new WaitForSeconds(delayTime);

        transform.anchoredPosition3D = pos;
    }
}
