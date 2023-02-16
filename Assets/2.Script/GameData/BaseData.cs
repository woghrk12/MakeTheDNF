using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseData<T> : ScriptableObject
{
    #region Variables

    protected T[] database = null;

    #endregion Variables

    #region Properties

    public int DataCount { get => database == null ? 0 : database.Length; }

    #endregion Properties

    #region Abstract Methods

    public abstract string[] GetNameList(bool p_isShowID, string p_filterWord = "");
    public abstract void SetData(int p_idx, T p_data);
    public abstract T GetData(int p_idx);

    #endregion Abstract Methods

    #region Virtual Methods

    public virtual void AddData(string p_newName) { }
    public virtual void RemoveData(int p_idx) { }
    public virtual void ClearData() { }
    public virtual void CopyData(int p_idx) { }

    #endregion Virtual Methods
}
