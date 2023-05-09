using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WordGrid
{
    public static Vector3 GridPositionFronWorldPosition3D(Vector3 worldPos, float gridScale)
    {
        var x = Mathf.Round(worldPos.x / gridScale) * gridScale;
        var y = Mathf.Round(worldPos.y / gridScale) * gridScale;
        var z = Mathf.Round(worldPos.z / gridScale) * gridScale;

        return new Vector3(x, y, z);
    }
}
