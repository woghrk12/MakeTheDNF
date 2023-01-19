using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonobehaviour<DataManager>
{
    private static SoundData soundData = null;

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

    private void Start()
    {
        if (ReferenceEquals(soundData, null))
        {
            soundData = ScriptableObject.CreateInstance<SoundData>();
            soundData.LoadData();
        }
    }
}
