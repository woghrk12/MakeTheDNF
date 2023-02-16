using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class SoundData : BaseData<SoundClip>
{
    #region Override Methods

    public override string[] GetNameList(bool p_isShowID, string p_filterWord = "")
    {
        string[] t_retList = new string[0];
        if (database == null) return t_retList;

        t_retList = new string[DataCount];

        for (int i = 0; i < DataCount; i++)
        {
            if (p_filterWord != "" && !database[i].clipName.ToLower().Contains(p_filterWord.ToLower())) continue;
            t_retList[i] = p_isShowID ? i.ToString() + " : " + database[i].clipName : database[i].clipName;
        }

        return t_retList;
    }

    public override void AddData(string p_newName)
    {
        if (database == null)
        {
            database = new SoundClip[] { new SoundClip() };
            return;
        }
        database = ArrayHelper.Add(new SoundClip(), database);
    }

    public override void RemoveData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;
        if (DataCount <= 0) return;

        database = ArrayHelper.Remove(p_idx, database);
        if (DataCount <= 0) database = null;
    }

    public override void ClearData()
    {
        foreach (SoundClip t_clip in database) t_clip.ReleaseClip();

        database = null;
    }

    public override void CopyData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;

        database = ArrayHelper.Add(database[p_idx], database);
    }

    #endregion Override Methods
}
