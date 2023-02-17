using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseData<T> : ScriptableObject 
{
    #region Variables

    protected string[] names = null;
    protected T[] database = null;

    #endregion Variables

    #region Properties

    public int DataCount { get => database == null ? 0 : database.Length; }

    #endregion Properties

    #region Set/Get Method

    public void SetData(int p_idx, T p_data)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;

        database[p_idx] = p_data;
    }

    public T GetData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return default;

        return database[p_idx];
    }

    public void SetName(int p_idx, string p_name)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;

        names[p_idx] = p_name;
    }

    public string GetName(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return string.Empty;

        return names[p_idx];
    }
    #endregion Set/Get Method

    #region Methods

    public string[] GetNameList(bool p_isShowID, string p_filterWord = "")
    {
        string[] t_retList = new string[0];
        if (names == null) return t_retList;

        t_retList = new string[DataCount];

        for (int i = 0; i < DataCount; i++)
        {
            if (p_filterWord != "" && !names[i].ToLower().Contains(p_filterWord.ToLower())) continue;
            t_retList[i] = p_isShowID ? i.ToString() + " : " + names[i] : names[i];
        }

        return t_retList;
    }

    #endregion Methods

    #region Virtual Methods

    public virtual void AddData(string p_newName) { }
    public virtual void RemoveData(int p_idx) { }
    public virtual void ClearData() { }
    public virtual void CopyData(int p_idx) { }

    #endregion Virtual Methods
}
