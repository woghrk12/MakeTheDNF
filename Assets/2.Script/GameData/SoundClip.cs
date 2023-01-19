using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClip
{
    public enum ESoundPlayType 
    { 
        NONE = -1,  
        BGM,
        EFFECT,
        UI,
    }

    #region Variables

    public int clipID = 0;
    public string clipName = string.Empty;
    public string clipPath = string.Empty;
    private AudioClip clip = null;

    public ESoundPlayType playType = ESoundPlayType.NONE;
    public float maxVolume = 1.0f;
    public float pitch = 1.0f;
    public float spatialBlend = 1.0f;

    public bool isLoop = false;
    public int cntLoop = 0;
    public int startLoop = 0;
    public float[] checkTime = new float[0];
    public float[] setTime = new float[0];
    private int curLoop = 0;

    #endregion Variables

    #region Properties

    public AudioClip Clip
    {
        set { clip = value; }
        get
        {
            if (clip == null) PreLoad();
            if (clip == null && clipName != string.Empty)
            {
                Debug.LogWarning($"Can not load audio clip resources {clipName}");
                return null;
            }

            return clip;
        }
    }

    public bool HasLoop  { get => checkTime.Length > 0; }

    #endregion Properties

    #region Constructor

    public SoundClip() { }

    public SoundClip(string p_clipPath, string p_clipName)
    {
        clipPath = p_clipPath;
        clipName = p_clipName;
    }

    #endregion Constructor

    #region Methods

    public void PreLoad()
    {
        if (clip != null) return;

        clip = Resources.Load(clipPath + clipName) as AudioClip;
    }

    public void ReleaseClip()
    {
        if (clip == null) return;

        clip = null;
    }

    // Loop Method
    public void AddLoop()
    {
        cntLoop++;
        checkTime = ArrayHelper.Add(0.0f, checkTime);
        setTime = ArrayHelper.Add(0.0f, setTime);
    }

    public void RemoveLoop(int p_idx)
    {
        cntLoop--;
        checkTime = ArrayHelper.Remove(p_idx, checkTime);
        setTime = ArrayHelper.Remove(p_idx, setTime);
    }

    public void NextLoop()
    {
        curLoop++;
        if (curLoop >= cntLoop) curLoop = 0;
    }

    public void MoveLoop(int p_idx)
    {
        curLoop = p_idx >= cntLoop ? 0 : p_idx;
    }

    public void CheckLoop(AudioSource p_source)
    {
        if (p_source.time < checkTime[curLoop]) return;

        p_source.time = setTime[curLoop];
        NextLoop();
    }

    #endregion Methods
}
