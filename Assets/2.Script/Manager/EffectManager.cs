using System;
using System.Collections;
using UnityEngine;

public class EffectManager : SingletonMonobehaviour<EffectManager>
{
    #region Variables

    private Transform effectRoot = null;

    private float minZ, maxZ;
    private int hashOnEffect, hashOffEffect;
    private int hashIdleState, hashActivateState;

    #endregion Variables

    #region Unity Events

    protected override void Awake()
    {
        base.Awake();

        effectRoot = new GameObject("EffectRoot").transform;
        effectRoot.SetParent(transform);

        hashOnEffect = Animator.StringToHash(AnimatorKey.On);
        hashOffEffect = Animator.StringToHash(AnimatorKey.Off);

        hashIdleState = Animator.StringToHash(AnimatorKey.EffectState.Idle);
        hashActivateState = Animator.StringToHash(AnimatorKey.EffectState.Activate);

        var t_data = DataManager.EffectData;
        for(int i =0;i < t_data.DataCount; i++)
            ObjectPoolingManager.Instance.AddPool(t_data.names[i], t_data.effectClips[i].Clip, 2);
    }

    #endregion Unity Events

    #region Methods

    public void SetZValue(float p_minZ, float p_maxZ)
    {
        minZ = p_minZ;
        maxZ = p_maxZ;
    }

    public IEnumerator PlayInstanceEffect(EEffectList p_idx, bool p_isLeft, Vector3 p_position, float p_sizeFactor)
    {
        string t_tag = p_idx.ToString();
        GameObject t_effect = ObjectPoolingManager.Instance.Instantiate(t_tag);
        t_effect.transform.position = new Vector3(p_position.x, p_position.y + p_position.z * DNFTransform.convRate, 0f);
        t_effect.transform.localScale = new Vector3(p_isLeft ? -p_sizeFactor : p_sizeFactor, p_sizeFactor, 1f);

        if (t_effect.transform.GetChild(0).TryGetComponent(out SpriteRenderer t_renderer))
        {
            float t_objZ = maxZ - p_position.z;
            float t_totalZ = maxZ - minZ;
            t_renderer.sortingOrder = (int)Mathf.Lerp(Int64.MinValue, Int64.MaxValue, t_objZ / t_totalZ);
        }

        if (t_effect.TryGetComponent(out Animator t_anim))
        {
            t_anim.SetTrigger(hashOnEffect);
            while (t_anim.GetCurrentAnimatorStateInfo(0).fullPathHash != hashActivateState) yield return Utilities.WaitForSeconds(0.1f);
            while (t_anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f) yield return Utilities.WaitForSeconds(0.1f);
            t_anim.SetTrigger(hashOffEffect);
            while (t_anim.GetCurrentAnimatorStateInfo(0).fullPathHash != hashIdleState) yield return Utilities.WaitForSeconds(0.1f);
        }

        ObjectPoolingManager.Instance.Destroy(t_effect);
    }

    #endregion Methods

}
