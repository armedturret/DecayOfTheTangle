using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    //checks if a given layer fits in the layer mask
    public static bool InLayerMask(LayerMask layerMask, int layer)
    {
        return layerMask.value == (layerMask.value | 1 << layer);
    }
}
