using UnityEngine;

public class TagAndLayer
{
    public class LayerName
    {
        public const string Default = "Default";
        public const string TransparentFX = "TransparentFX";
        public const string IgnoreRayCast = "Ignore Raycast";
        public const string Water = "Water";
        public const string UI = "UI";
    }

    public enum LayerIndex
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRayCast = 2,
        Water = 4,
        UI = 5,
    }

    public static int GetLayerByName(string p_layerName)
    {
        return LayerMask.NameToLayer(p_layerName);
    }

    public class TagName
    { 
    
    }
}
