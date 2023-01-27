using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNFTransform : MonoBehaviour
{
    #region Variables

    private static readonly int xRate = 2;
    private static readonly int yRate = 1;
    public static readonly float convRate = (float)yRate / xRate;
    public static readonly float invConvRate = (float)xRate / yRate;

    [SerializeField] private Transform posObj = null;
    [SerializeField] private Transform yPosObj = null;
    [SerializeField] private Transform scaleObj = null;

    #endregion Variables

    #region Properties

    public float X 
    {
        set
        {
            var t_pos = posObj.position;
            t_pos.x = value;    
            posObj.position = t_pos;
        }
        get => posObj.position.x;
    }

    public float Y 
    {
        set
        {
            var t_pos = yPosObj.localPosition;
            t_pos.y = value;
            yPosObj.localPosition = t_pos;
        }
        get => yPosObj.localPosition.y;
    }

    public float Z 
    {
        set
        {
            var t_pos = posObj.position;
            t_pos.y = value * convRate;
            posObj.position = t_pos;
        }
        get => posObj.position.y * invConvRate;
    }

    public Vector3 Position 
    {
        set
        {
            X = value.x;
            Y = HasYObj ? value.y : 0f;
            Z = value.z;
        }
        get => new Vector3(X, HasYObj ? Y : 0f, Z);
    }

    public bool HasYObj { get => yPosObj != null; }

    public Vector3 LocalScale { set { scaleObj.localScale = value; } get => scaleObj.localScale; }

    #endregion Properties
}
