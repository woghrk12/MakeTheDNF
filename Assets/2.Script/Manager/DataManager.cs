using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonobehaviour<DataManager>
{
    #region Variables

    private static InputData inputData = null;
    private static SoundData soundData = null;
    private static EffectData effectData = null;
    private static SkillData skillData = null;

    #endregion Variables

    #region Properties

    public static InputData InputData
    {
        get
        {
            if (ReferenceEquals(inputData, null))
            {
                inputData = ScriptableObject.CreateInstance<InputData>();
                inputData.LoadData();
            }

            return inputData;
        }
    }

    public static SoundData SoundData
    {
        get
        {
            if (ReferenceEquals(soundData, null))
            {
                soundData = ScriptableObject.CreateInstance<SoundData>();
                soundData.LoadData();
            }

            return soundData;
        }
    }

    public static EffectData EffectData
    {
        get
        {
            if (ReferenceEquals(effectData, null))
            {
                effectData = ScriptableObject.CreateInstance<EffectData>();
                effectData.LoadData();
            }

            return effectData;
        }
    }

    public static SkillData SkillData
    {
        get
        {
            if (ReferenceEquals(skillData, null))
            {
                skillData = ScriptableObject.CreateInstance<SkillData>();
                skillData.LoadData();
            }

            return skillData;
        }
    }

    #endregion Properties

    private void Start()
    {
        if (ReferenceEquals(soundData, null))
        {
            soundData = ScriptableObject.CreateInstance<SoundData>();
            soundData.LoadData();
        }

        if (ReferenceEquals(effectData, null))
        {
            effectData = ScriptableObject.CreateInstance<EffectData>();
            effectData.LoadData();
        }

        if (ReferenceEquals(skillData, null))
        {
            skillData = ScriptableObject.CreateInstance<SkillData>();
            skillData.LoadData();
        }
    }
}
