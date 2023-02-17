using System;
using System.IO;
using System.Xml;
using UnityEngine;

public class EffectData : BaseData<EffectClip>
{
    #region Override Methods

    public override void AddData(string p_newName)
    {
        if (database == null)
        {
            database = new EffectClip[] { new EffectClip() };
            names = new string[] { p_newName };
            return;
        }

        database = ArrayHelper.Add(new EffectClip(), database);
        names = ArrayHelper.Add(p_newName, names);
    }

    public override void RemoveData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;
        if (DataCount <= 0) return;

        database = ArrayHelper.Remove(p_idx, database);
        names = ArrayHelper.Remove(p_idx, names);

        if (DataCount <= 0)
        {
            database = null;
            names = null;
        }
    }

    public override void ClearData()
    {
        foreach (EffectClip t_clip in database) t_clip.ReleaseClip();

        database = null;
        names = null;
    }

    public override void CopyData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;

        database = ArrayHelper.Add(database[p_idx], database);
        names = ArrayHelper.Add(names[p_idx], names);
    }

    #endregion Override Methods
}