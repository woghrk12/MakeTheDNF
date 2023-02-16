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
                inputData = Resources.Load("Data/Database/InputData") as InputData;
                if (ReferenceEquals(InputData, null))
                {
                    inputData = ScriptableObject.CreateInstance<InputData>();
                }
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
                soundData = Resources.Load("Data/Database/SoundData") as SoundData;
                if (ReferenceEquals(soundData, null))
                {
                    soundData = ScriptableObject.CreateInstance<SoundData>();
                }
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
                effectData = Resources.Load("Data/Database/EffectData") as EffectData;
                if (ReferenceEquals(effectData, null))
                {
                    effectData = ScriptableObject.CreateInstance<EffectData>();
                }
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
                skillData = Resources.Load("Data/Database/SkillData") as SkillData;
                if (ReferenceEquals(skillData, null))
                {
                    skillData = ScriptableObject.CreateInstance<SkillData>();
                }
            }

            return skillData;
        }
    }

    #endregion Properties

    private void Start()
    {
        if (ReferenceEquals(inputData, null))
        {
            inputData = Resources.Load("Data/Database/InputData") as InputData;
            if (ReferenceEquals(InputData, null))
            {
                inputData = ScriptableObject.CreateInstance<InputData>();
            }
        }

        if (ReferenceEquals(soundData, null))
        {
            soundData = Resources.Load("Data/Database/SoundData") as SoundData;
            if (ReferenceEquals(soundData, null))
            {
                soundData = ScriptableObject.CreateInstance<SoundData>();
            }
        }

        if (ReferenceEquals(effectData, null))
        {
            effectData = Resources.Load("Data/Database/EffectData") as EffectData;
            if (ReferenceEquals(effectData, null))
            {
                effectData = ScriptableObject.CreateInstance<EffectData>();
            }
        }

        if (ReferenceEquals(skillData, null))
        {
            skillData = Resources.Load("Data/Database/SkillData") as SkillData;
            if (ReferenceEquals(skillData, null))
            {
                skillData = ScriptableObject.CreateInstance<SkillData>();
            }
        }
    }
}
