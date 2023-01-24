using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNFTransform
{
    #region Variables

    private static readonly int xRate = 16;
    private static readonly int yRate = 9;
    public static readonly float convRate = (float)yRate / xRate;

    private Transform posObj;
    private Transform yPosObj;
    private Transform scaleObj;
    private Vector3 position;

    #endregion Variables

    #region Properties

    public float X 
    {
        set
        {
            position.x = value;

            var t_pos = posObj.position;
            t_pos.x = position.x;

            posObj.position = t_pos;
        }
        get => position.x;
    }

    public float Y 
    {
        set
        {
            position.y = value;

            var t_pos = yPosObj.localPosition;
            t_pos.y = position.y;

            yPosObj.localPosition = t_pos;
        }
        get => position.y;
    }

    public float Z 
    {
        set
        {
            position.z = value;

            var t_pos = posObj.position;
            t_pos.y = position.z * convRate;

            posObj.position = t_pos;
        }
        get => position.z;
    }

    public Vector3 Position 
    {
        set
        {
            X = value.x;
            Y = HasYObj ? value.y : 0f;
            Z = value.z;
        }
        get => position;
    }

    public bool HasYObj { get => yPosObj != null; }

    public Vector3 LocalScale { set { scaleObj.localScale = value; } get => scaleObj.localScale; }

    #endregion Properties

    #region Constructor

    public DNFTransform(Transform p_posObj, Transform p_scaleObj, Vector3 p_pos = default)
    {
        posObj = p_posObj;
        yPosObj = null;
        scaleObj = p_scaleObj;
        Position = p_pos;
    }
    public DNFTransform(Transform p_posObj, Transform p_yPosObj, Transform p_scaleObj, Vector3 p_pos = default)
    {
        posObj = p_posObj;
        yPosObj = p_yPosObj;
        scaleObj = p_scaleObj;
        Position = p_pos;
    }

    #endregion Constructor
}
