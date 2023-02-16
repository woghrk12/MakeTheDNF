using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonobehaviour<DataManager>
{
    #region Variables

    [SerializeField] private InputData inputData = null;
    [SerializeField] private SoundData soundData = null;
    [SerializeField] private EffectData effectData = null;
    [SerializeField] private SkillData skillData = null;

    #endregion Variables

    #region Properties

    public InputData InputData => inputData;
    public SoundData SoundData => soundData;
    public EffectData EffectData => effectData;
    public SkillData SkillData => skillData;

    #endregion Properties
}
