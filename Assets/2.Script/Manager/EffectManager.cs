using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : SingletonMonobehaviour<EffectManager>
{
    #region Variables

    private Transform effectRoot = null;

    #endregion Variables

    #region Unity Events

    protected override void Awake()
    {
        base.Awake();

        effectRoot = new GameObject("EffectRoot").transform;
        effectRoot.SetParent(transform);
    }

    #endregion Unity Events

    #region Methods

    public GameObject PlayEffect(int p_idx, bool p_isLeft, Vector3 p_position = default)
    {
        GameObject t_effect = DataManager.EffectData.GetClip(p_idx).Instantiate();
        t_effect.SetActive(true);
        t_effect.transform.localScale = new Vector3(p_isLeft ? -1f : 1f, 1f, 1f);
        return t_effect;
    }

    #endregion Methods
}
