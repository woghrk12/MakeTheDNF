using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClip
{
    #region Variables

    public int clipID = 0;
    private string clipName = string.Empty;
    private string clipPath = string.Empty;
    private AudioClip clip = null;

    public float maxVolume = 1.0f;
    public float pitch = 1.0f;
    public float dopplerLevel = 1.0f;
    public float spatialBlend = 1.0f;
    public AudioRolloffMode audioRollOffMode = AudioRolloffMode.Logarithmic;

    public bool isLoop = false;
    public float[] checkTime = new float[0];
    public float[] setTime = new float[0];
    public int curLoop = 0;

    #endregion Variables

    #region Properties

    public AudioClip Clip
    {
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

    public SoundClip(string p_clipPath, string p_clipName)
    {
        clipPath = p_clipPath;
        clipName = p_clipName;
    }

    #endregion Constructor

    #region Method

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
        checkTime = ArrayHelper.Add(0.0f, checkTime);
        setTime = ArrayHelper.Add(0.0f, setTime);
    }

    public void RemoveLoop(int p_idx)
    {
        checkTime = ArrayHelper.Remove(p_idx, checkTime);
        setTime = ArrayHelper.Remove(p_idx, setTime);
    }

    public void NextLoop()
    {
        curLoop++;
        if (curLoop >= checkTime.Length) curLoop = 0;
    }

    public void CheckLoop(AudioSource p_source)
    {
        if (!HasLoop) return;
        if (p_source.time < checkTime[curLoop]) return;

        p_source.time = setTime[curLoop];
        NextLoop();
    }

    #endregion Method
}
