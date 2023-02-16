using System;
using System.IO;
using System.Xml;
using UnityEngine;

public class EffectData : BaseData<EffectClip>
{
    #region Override Methods

    public override string[] GetNameList(bool p_isShowID, string p_filterWord = "")
    {
        throw new NotImplementedException();
    }

    public override EffectClip GetClip(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return null;

        return database[p_idx];
    }

    public override void AddData(string p_newName)
    {
        if (database == null)
        {
            database = new EffectClip[] { new EffectClip() };
            return;
        }

        database = ArrayHelper.Add(new EffectClip(), database);
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
        foreach (EffectClip t_clip in database) t_clip.ReleaseClip();

        database = null;
    }

    public override void CopyData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;

        database = ArrayHelper.Add(database[p_idx], database);
    }

    #endregion Override Methods
}