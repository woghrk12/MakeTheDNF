using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingletonMonobehaviour<SoundManager>
{
    #region Variables

    private const string mixerName = "AudioMixer";
    private const string audioContainerName = "Container";

    private const string masterGroupName = "Master";
    private const string effectGroupName = "Effect";
    private const string bgmGroupName = "BGM";
    private const string uiGroupName = "UI";

    private const string masterVolumeParam = "Volume_Master";
    private const string effectVolumeParam = "Volume_Effect";
    private const string bgmVolumeParam = "Volume_BGM";
    private const string uiVolumeParam = "Volume_UI";

    private const string nameBGMSoundSource = "BGM";
    private const string nameEffectSoundSource = "Effect";
    private const string nameUISoundSource = "UI";

    private AudioMixer mixer = null;
    private Transform audioRoot = null;
    private AudioSource bgmAudio = null;
    private AudioSource[] effectAudios = null;
    private AudioSource uiAudio = null;

    private float minVolume = -80.0f;
    private float maxVolume = 0.0f;

    private SoundClip curBGMSound = null;

    [SerializeField] private int numEffectChannel = 5;
    private float[] effectPlayStartTime = null;

    private bool isCheckingLoop = false;

    #endregion Variables

    #region Properties

    public float MasterVolume
    {
        set
        {
            float t_volume = Mathf.Lerp(minVolume, maxVolume, Mathf.Clamp01(value));
            mixer.SetFloat(masterVolumeParam, t_volume);
            PlayerPrefs.SetFloat(masterVolumeParam, t_volume);
        }
        get
        {
            if (PlayerPrefs.HasKey(masterVolumeParam)) return Mathf.Lerp(minVolume, maxVolume, PlayerPrefs.GetFloat(masterVolumeParam));
            return maxVolume;
        }
    }

    public float BGMVolume
    {
        set
        {
            float t_volume = Mathf.Lerp(minVolume, maxVolume, Mathf.Clamp01(value));
            mixer.SetFloat(bgmVolumeParam, t_volume);
            PlayerPrefs.SetFloat(bgmVolumeParam, t_volume);
        }
        get
        {
            if (PlayerPrefs.HasKey(bgmVolumeParam)) return Mathf.Lerp(minVolume, maxVolume, PlayerPrefs.GetFloat(bgmVolumeParam));
            return maxVolume;
        }
    }

    public float EffectVolume
    {
        set
        {
            float t_volume = Mathf.Lerp(minVolume, maxVolume, Mathf.Clamp01(value));
            mixer.SetFloat(effectVolumeParam, t_volume);
            PlayerPrefs.SetFloat(effectVolumeParam, t_volume);
        }
        get
        {
            if (PlayerPrefs.HasKey(effectVolumeParam)) return Mathf.Lerp(minVolume, maxVolume, PlayerPrefs.GetFloat(effectVolumeParam));
            return maxVolume;
        }
    }

    public float UIVolume
    {
        set
        {
            float t_volume = Mathf.Lerp(minVolume, maxVolume, Mathf.Clamp01(value));
            mixer.SetFloat(uiVolumeParam, t_volume);
            PlayerPrefs.SetFloat(uiVolumeParam, t_volume);
        }
        get
        {
            if (PlayerPrefs.HasKey(uiVolumeParam)) return Mathf.Lerp(minVolume, maxVolume, PlayerPrefs.GetFloat(uiVolumeParam));
            return maxVolume;
        }
    }

    #endregion Properties

    #region Unity Event

    protected override void Awake()
    {
        base.Awake();

        if (mixer == null) mixer = Resources.Load(mixerName) as AudioMixer;

        if (audioRoot == null)
        {
            audioRoot = new GameObject(audioContainerName).transform;
            audioRoot.SetParent(transform);
            audioRoot.localPosition = Vector3.zero;
        }
        if (bgmAudio == null)
        {
            GameObject t_bgmSoundObj = new GameObject(nameBGMSoundSource, typeof(AudioSource));
            t_bgmSoundObj.transform.SetParent(audioRoot);
            bgmAudio = t_bgmSoundObj.GetComponent<AudioSource>();
            bgmAudio.playOnAwake = false;
        }
        if (uiAudio == null)
        {
            GameObject t_uiSoundObj = new GameObject(nameUISoundSource, typeof(AudioSource));
            t_uiSoundObj.transform.SetParent(audioRoot);
            uiAudio = t_uiSoundObj.GetComponent<AudioSource>();
            uiAudio.playOnAwake = false;
        }
        if (effectAudios == null || effectAudios.Length <= 0)
        {
            effectPlayStartTime = new float[numEffectChannel];
            effectAudios = new AudioSource[numEffectChannel];

            for (int i = 0; i < numEffectChannel; i++)
            {
                effectPlayStartTime[i] = 0.0f;
                GameObject t_effectSoundObj = new GameObject(nameEffectSoundSource, typeof(AudioSource));
                t_effectSoundObj.transform.SetParent(audioRoot);
                effectAudios[i] = t_effectSoundObj.GetComponent<AudioSource>();
                effectAudios[i].playOnAwake = false;
            }
        }

        if (mixer != null)
        {
            bgmAudio.outputAudioMixerGroup = mixer.FindMatchingGroups(bgmGroupName)[0];
            uiAudio.outputAudioMixerGroup = mixer.FindMatchingGroups(uiGroupName)[0];
            for (int i = 0; i < numEffectChannel; i++)
                effectAudios[i].outputAudioMixerGroup = mixer.FindMatchingGroups(effectGroupName)[0];
        }

        ResetVolume();
    }

    #endregion Unity Event

    #region Methods

    public void ResetVolume()
    {
        if (mixer == null) return;

        mixer.SetFloat(masterVolumeParam, MasterVolume);
        mixer.SetFloat(bgmVolumeParam, BGMVolume);
        mixer.SetFloat(effectVolumeParam, EffectVolume);
        mixer.SetFloat(uiVolumeParam, UIVolume);
    }

    private void PlayAudioSource(AudioSource p_source, SoundClip p_clip, float p_volume)
    {
        if (p_source == null || p_clip == null) return;

        p_source.Stop();
        p_source.clip = p_clip.Clip;
        p_source.volume = p_volume;
        p_source.loop = p_clip.isLoop;
        p_source.pitch = p_clip.pitch;
        p_source.spatialBlend = p_clip.spatialBlend;

        p_source.Play();
    }

    public void PlayBGM(SoundClip p_clip, bool p_isRestart = true, bool p_isLoop = false, int p_loopPos = -1)
    {
        if (p_clip == curBGMSound && !p_isRestart) return;

        bgmAudio.Stop();
        curBGMSound = p_clip;

        if (curBGMSound.HasLoop && p_isLoop)
        {
            p_clip.MoveLoop(p_loopPos >= 0 ? p_loopPos : p_clip.startLoop);
            StartCoroutine(CheckBGMLoop());
        }

        PlayAudioSource(bgmAudio, p_clip, BGMVolume);
    }
    public void StopBGM()
    {
        bgmAudio.Stop();
        StopAllCoroutines();
    }

    public void PlayUISound(SoundClip p_clip) => PlayAudioSource(uiAudio, p_clip, UIVolume);
    public void StopUISound() => uiAudio.Stop();

    public void PlayEffectSound(SoundClip p_clip)
    {
        for (int i = 0; i < numEffectChannel; i++)
        {
            if (!effectAudios[i].isPlaying)
            {
                PlayAudioSource(effectAudios[i], p_clip, EffectVolume);
                effectPlayStartTime[i] = Time.realtimeSinceStartup;
                return;
            }
            else if (effectAudios[i].clip == p_clip.Clip)
            {
                effectAudios[i].Stop();
                PlayAudioSource(effectAudios[i], p_clip, EffectVolume);
                effectPlayStartTime[i] = Time.realtimeSinceStartup;
                return;
            }
        }

        float t_maxTime = effectPlayStartTime[0];
        int t_selectIdx = 0;

        for (int i = 1; i < numEffectChannel; i++)
        {
            if (effectPlayStartTime[i] <= t_maxTime) continue;

            t_maxTime = effectPlayStartTime[i];
            t_selectIdx = i;
        }

        PlayAudioSource(effectAudios[t_selectIdx], p_clip, EffectVolume);
    }
    public void StopEffectSound()
    {
        foreach (AudioSource t_effectAudio in effectAudios)
            t_effectAudio.Stop();
    }

    #endregion Methods

    #region Helper Methods

    private IEnumerator CheckBGMLoop()
    {
        while (isCheckingLoop)
        {
            yield return new WaitForSeconds(0.05f);
            curBGMSound.CheckLoop(bgmAudio);
        }
    }

    #endregion Helper Methods
}
