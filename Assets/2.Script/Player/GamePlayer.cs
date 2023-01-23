using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    #region Variables

    private Animator anim = null;

    // DNF Transform
    private DNFTransform charTransform = null;
    private Transform posObj = null;
    private Transform yPosObj = null;
    private Transform scaleObj = null;


    #endregion Variables

    #region Unity Event

    private void Awake()
    {
        anim = GetComponent<Animator>();

        if (posObj == null) posObj = transform;
        if (yPosObj == null) yPosObj = posObj.GetChild(0).transform;
        if (scaleObj == null) scaleObj = yPosObj.GetChild(0).transform;
        charTransform = new DNFTransform(posObj, yPosObj, scaleObj);
    }

    #endregion Unity Event
}
