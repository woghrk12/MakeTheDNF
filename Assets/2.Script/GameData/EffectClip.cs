using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectClip
{
    public enum EEffectType
    { 
        NONE = -1,
        INSTANCE,
        LOOP,
    }
    #region Variables

    public int clipID = 0;
    public string clipName = string.Empty;
    public string clipPath = string.Empty;
    private GameObject clip = null;

    public EEffectType effectType = EEffectType.NONE;

    #endregion Variables

    #region Constructor

    public EffectClip() { }

    public EffectClip(string p_clipPath, string p_clipName)
    {
        clipPath = p_clipPath;
        clipName = p_clipName;
    }

    #endregion Constructor

    #region Methods

    public void Preload()
    {
        if (clip != null) return;

        clip = Resources.Load(clipPath + clipName) as GameObject;
    }

    public void ReleaseClip()
    {
        if (clip == null) return;

        clip = null;
    }

    public GameObject Instantiate(Vector3 p_position)
    {
        if (clip == null) Preload();
        return clip != null ? GameObject.Instantiate(clip, p_position, Quaternion.identity) : null; 
    }

    #endregion Methods
}
