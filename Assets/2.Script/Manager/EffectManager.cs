using System;   
using UnityEngine;

public class EffectManager : SingletonMonobehaviour<EffectManager>
{
    #region Variables

    private Transform effectRoot = null;

    private float minZ, maxZ;
    private int hashOnEffect, hashOffEffect;

    #endregion Variables

    #region Unity Events

    protected override void Awake()
    {
        base.Awake();

        effectRoot = new GameObject("EffectRoot").transform;
        effectRoot.SetParent(transform);

        hashOnEffect = Animator.StringToHash(AnimatorKey.On);
        hashOffEffect = Animator.StringToHash(AnimatorKey.Off);
    }

    #endregion Unity Events

    #region Methods

    public void SetZValue(float p_minZ, float p_maxZ)
    {
        minZ = p_minZ;
        maxZ = p_maxZ;
    }

    public GameObject OnEffect(EEffectList p_idx, bool p_isLeft, Vector3 p_position, float p_sizeFactor = 1f)
    {
        string t_tag = p_idx.ToString();
        GameObject t_effect = ObjectPoolingManager.Instance.Instantiate(t_tag);

        t_effect.transform.localScale = new Vector3(p_isLeft ? -p_sizeFactor : p_sizeFactor, p_sizeFactor, 1f);

        if (t_effect.transform.GetChild(0).TryGetComponent(out SpriteRenderer t_renderer))
        {
            float t_objZ = maxZ - p_position.z;
            float t_totalZ = maxZ - minZ;
            t_renderer.sortingOrder = (int)Mathf.Lerp(Int64.MinValue, Int64.MaxValue, t_objZ / t_totalZ);
        }

        if (t_effect.TryGetComponent(out Animator t_anim)) t_anim.SetTrigger(hashOnEffect);

        return t_effect;
    }

    public void OffEffect(GameObject p_effect)
    {
        if (p_effect.TryGetComponent(out Animator t_anim)) t_anim.SetTrigger(hashOffEffect);
        ObjectPoolingManager.Instance.Destroy(p_effect);
    }

    #endregion Methods
}
