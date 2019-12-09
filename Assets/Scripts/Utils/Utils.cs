using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static void SetParentAndModifyScale(Transform target,Transform parent)
    {
        var scale = target.transform.localScale;
        target.transform.transform.SetParent(parent);
        scale.Scale(parent.lossyScale);
        target.transform.localScale = scale;
    }
}
