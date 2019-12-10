using UnityEngine;

public static class Utils
{
    /// <summary>
    /// Parents target to 'parent' but modifies the scale using the new parent scale.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="parent"></param>
    public static void SetParentAndModifyScale(Transform target,Transform parent)
    {
        var scale = target.transform.localScale;
        target.transform.transform.SetParent(parent);
        scale.Scale(parent.lossyScale);
        target.transform.localScale = scale;
    }
}
