using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseData<T> : ScriptableObject
{
    #region Variables

    public string[] names = null;
    protected string xmlFilePath = string.Empty;

    #endregion Variables

    #region Properties

    public int DataCount { get => names == null ? 0 : names.Length; }

    #endregion Properties

    #region Helper Methods

    public string[] GetNameList(bool p_isShowID, string p_filterWord = "")
    {
        string[] t_retList = new string[0];
        if (names == null) return t_retList;

        t_retList = new string[names.Length];

        for (int i = 0; i < names.Length; i++)
        {
            if (p_filterWord != "" && !names[i].ToLower().Contains(p_filterWord.ToLower())) continue;
            t_retList[i] = p_isShowID ? i.ToString() + " : " + names[i] : names[i];
        }

        return t_retList;
    }

    #endregion Helper Methods

    #region Abstract Methods

    public abstract void LoadData();
    public abstract void SaveData();
    public abstract T GetCopyClip(int p_idx);
    #endregion Abstract Methods

    #region Virtual Methods

    public virtual void AddData(string p_newName) { }
    public virtual void RemoveData(int p_idx) { }
    public virtual void ClearData() { }
    public virtual void CopyData(int p_idx) { }

    #endregion Virtual Methods
}
