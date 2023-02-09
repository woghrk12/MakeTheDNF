using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboInfo : ScriptableObject
{
    #region Variables

    public string motion = string.Empty;

    public int numEffects = 0;
    public string[] effectPaths = new string[0];
    public string[] effectNames = new string[0];
    public GameObject[] effects = new GameObject[0];
    public Vector3[] effectOffsets = new Vector3[0];

    public Vector3 range = Vector3.zero;
    public Vector3 rangeOffset = Vector3.zero;

    public float preDelay = 0f;
    public float duration = 0f;
    public float postDelay = 0f;

    #endregion Variables

    #region Methods

    public void PreLoadEffect()
    {
        for (int i = 0; i < numEffects; i++)
        {
            if (effects[i] != null) continue;
            effects[i] = Resources.Load(effectPaths[i] + effectNames[i]) as GameObject;
        }
    }

    public void ReleaseEffect()
    {
        for (int i = 0; i < numEffects; i++)
        {
            if (effects[i] == null) continue;
            effects[i] = null;
        }
    }

    #endregion Methods

    #region Helper Methods

    public void AddEffect()
    {
        numEffects++;
        effectPaths = ArrayHelper.Add(string.Empty, effectPaths);
        effectNames = ArrayHelper.Add(string.Empty, effectPaths);
        effects = ArrayHelper.Add(null, effects);
        effectOffsets = ArrayHelper.Add(Vector3.zero, effectOffsets);
    }

    public void RemoveEffect(int p_idx)
    {
        numEffects--;
        effectPaths = ArrayHelper.Remove(p_idx, effectPaths);
        effectNames = ArrayHelper.Remove(p_idx, effectPaths);
        effects = ArrayHelper.Remove(p_idx, effects);
        effectOffsets = ArrayHelper.Remove(p_idx, effectOffsets);
    }

    #endregion Helper Methods
}
