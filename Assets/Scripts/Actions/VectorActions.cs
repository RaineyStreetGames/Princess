using UnityEngine;
public static class VectorActions
{
    public static Vector3 xz(this Vector3 vv)
    {
        return new Vector3(vv.x, 0, vv.z);
    }

    public static Vector3 yz(this Vector3 vv)
    {
        return new Vector3(0, vv.y, vv.z);
    }

    public static Vector3 xy(this Vector3 vv)
    {
        return new Vector3(vv.x, vv.y, 0);
    }
}
