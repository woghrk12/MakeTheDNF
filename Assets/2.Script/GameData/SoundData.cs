using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class SoundData : BaseData
{
    #region Variables

    private SoundClip[] soundClips = new SoundClip[0];

    private string dataPath = "Data/soundData";

    private string xmlFilePath = Application.dataPath + FilePath.DataDirectoryPath;
    private string xmlFileName = "soundData.xml";

    private const string soundString = "sound";
    private const string clipString = "clip";

    #endregion Variables

    #region Override Methods

    public override void LoadData()
    {
        TextAsset t_asset = Resources.Load(dataPath) as TextAsset;
        if (t_asset == null || t_asset.text == null)
        {
            AddData("New Sound Data");
            return;
        }

        using (XmlReader t_reader = XmlReader.Create(new StringReader(t_asset.text)))
        {
            int t_curID = 0;
            while (t_reader.Read())
            {
                if (!t_reader.IsStartElement()) continue;

                switch (t_reader.Name)
                {
                    case XmlElementName.SoundData.LENGTH:
                        int t_length = int.Parse(t_reader.ReadElementContentAsString());
                        names = new string[t_length];
                        soundClips = new SoundClip[t_length];
                        break;

                    case XmlElementName.SoundData.ID:
                        t_curID = int.Parse(t_reader.ReadElementContentAsString());
                        soundClips[t_curID] = new SoundClip();
                        soundClips[t_curID].clipID = t_curID;
                        break;

                    case XmlElementName.SoundData.NAME:
                        names[t_curID] = t_reader.ReadElementContentAsString();
                        break;

                    case XmlElementName.SoundData.CLIPNAME:
                        soundClips[t_curID].clipName = t_reader.ReadElementContentAsString();
                        break;

                    case XmlElementName.SoundData.CLIPPATH:
                        soundClips[t_curID].clipPath = t_reader.ReadElementContentAsString();
                        break;

                    case XmlElementName.SoundData.PLAYTYPE:
                        soundClips[t_curID].playType = (SoundClip.ESoundPlayType)Enum.Parse(typeof(SoundClip.ESoundPlayType), t_reader.ReadElementContentAsString());
                        break;

                    case XmlElementName.SoundData.MAXVOLUME:
                        soundClips[t_curID].maxVolume = float.Parse(t_reader.ReadElementContentAsString());
                        break;

                    case XmlElementName.SoundData.PITCH:
                        soundClips[t_curID].pitch = float.Parse(t_reader.ReadElementContentAsString());
                        break;

                    case XmlElementName.SoundData.SPATIALBLEND:
                        soundClips[t_curID].spatialBlend = float.Parse(t_reader.ReadElementContentAsString());
                        break;

                    case XmlElementName.SoundData.ISLOOP:
                        soundClips[t_curID].isLoop = true;
                        break;

                    case XmlElementName.SoundData.CNTLOOP:
                        int t_cnt = int.Parse(t_reader.ReadElementContentAsString());
                        soundClips[t_curID].cntLoop = t_cnt;
                        soundClips[t_curID].checkTime = new float[t_cnt];
                        soundClips[t_curID].setTime = new float[t_cnt];                        
                        break;

                    case XmlElementName.SoundData.STARTLOOP:
                        int t_start = int.Parse(t_reader.ReadElementContentAsString());
                        soundClips[t_curID].startLoop = t_start;
                        soundClips[t_curID].MoveLoop(t_start);
                        break;

                    case XmlElementName.SoundData.CHECKTIME:
                        string[] t_checkTime = t_reader.ReadElementContentAsString().Split('/');
                        for (int i = 0; i < t_checkTime.Length; i++)
                        {
                            if (t_checkTime[i] == string.Empty) continue;
                            soundClips[t_curID].checkTime[i] = float.Parse(t_checkTime[i]);
                        }
                        break;

                    case XmlElementName.SoundData.SETTIME:
                        string[] t_setTime = t_reader.ReadElementContentAsString().Split('/');
                        for (int i = 0; i < t_setTime.Length; i++)
                        {
                            if (t_setTime[i] == string.Empty) continue;
                            soundClips[t_curID].setTime[i] = float.Parse(t_setTime[i]);
                        }
                        break;
                }
            }
        }
    }

    public override void SaveData()
    {
        XmlWriterSettings t_settings = new XmlWriterSettings();
        t_settings.Encoding = System.Text.Encoding.Unicode;

        using (XmlWriter t_writer = XmlWriter.Create(xmlFilePath + xmlFileName, t_settings))
        {
            t_writer.WriteStartDocument();
            t_writer.WriteStartElement(soundString);

            int t_length = DataCount;
            t_writer.WriteElementString(XmlElementName.SoundData.LENGTH, t_length.ToString());
            for (int i = 0; i < t_length; i++)
            {
                SoundClip t_clip = soundClips[i];
                t_writer.WriteStartElement(clipString);
                t_writer.WriteElementString(XmlElementName.SoundData.ID, i.ToString());
                t_writer.WriteElementString(XmlElementName.SoundData.NAME, names[i]);
                t_writer.WriteElementString(XmlElementName.SoundData.CLIPNAME, t_clip.clipName);
                t_writer.WriteElementString(XmlElementName.SoundData.CLIPPATH, t_clip.clipPath);
                t_writer.WriteElementString(XmlElementName.SoundData.PLAYTYPE, t_clip.playType.ToString());
                t_writer.WriteElementString(XmlElementName.SoundData.MAXVOLUME, t_clip.maxVolume.ToString());
                t_writer.WriteElementString(XmlElementName.SoundData.PITCH, t_clip.pitch.ToString());
                t_writer.WriteElementString(XmlElementName.SoundData.SPATIALBLEND, t_clip.spatialBlend.ToString());

                if (!t_clip.isLoop) continue;
                
                t_writer.WriteElementString(XmlElementName.SoundData.ISLOOP, t_clip.isLoop.ToString());
                t_writer.WriteElementString(XmlElementName.SoundData.CNTLOOP, t_clip.cntLoop.ToString());
                t_writer.WriteElementString(XmlElementName.SoundData.STARTLOOP, t_clip.startLoop.ToString());

                string t_str = "";
                foreach (float t_checkTime in t_clip.checkTime) t_str += t_checkTime.ToString() + "/";
                t_writer.WriteElementString(XmlElementName.SoundData.CHECKTIME, t_str);

                t_str = "";
                foreach (float t_setTime in t_clip.setTime) t_str += t_setTime.ToString() + "/";
                t_writer.WriteElementString(XmlElementName.SoundData.SETTIME, t_str);

                t_writer.WriteEndElement();
            }
            t_writer.WriteEndElement();
            t_writer.WriteWhitespace("\n");
            
            t_writer.WriteEndElement();
            t_writer.WriteEndDocument();
        }
    }

    public override void AddData(string p_newName)
    {
        if (names == null)
        {
            names = new string[] { p_newName };
            soundClips = new SoundClip[] { new SoundClip() };
            return;
        }

        names = ArrayHelper.Add(p_newName, names);
        soundClips = ArrayHelper.Add(new SoundClip(), soundClips);
    }

    public override void RemoveData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;
        if (DataCount <= 0) return;

        names = ArrayHelper.Remove(p_idx, names);
        soundClips = ArrayHelper.Remove(p_idx, soundClips);

        if (DataCount <= 0)
        {
            names = null;
            soundClips = null;
        }
    }

    public override void ClearData()
    {
        foreach (SoundClip t_clip in soundClips) t_clip.ReleaseClip();

        names = null;
        soundClips = null;
    }

    public override void CopyData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;

        names = ArrayHelper.Add(names[p_idx], names);
        soundClips = ArrayHelper.Add(soundClips[p_idx], soundClips);
    }

    #endregion Override Methods

    #region Helper Methods

    public SoundClip GetClip(int p_idx, bool p_isCopy = false)
    {
        if (p_idx < 0 || p_idx >= DataCount) return null;

        SoundClip t_origin = soundClips[p_idx];

        if (!p_isCopy)
        {
            t_origin.PreLoad();
            return t_origin;
        }

        SoundClip t_copyClip = new SoundClip();

        t_copyClip.clipID = t_origin.clipID;
        t_copyClip.clipName = t_origin.clipName;
        t_copyClip.clipPath = t_origin.clipPath;
        t_copyClip.Clip = t_origin.Clip;
        t_copyClip.playType = t_origin.playType;
        t_copyClip.maxVolume = t_origin.maxVolume;
        t_copyClip.pitch = t_origin.pitch;
        t_copyClip.spatialBlend = t_origin.spatialBlend;

        if (t_origin.isLoop)
        {
            int t_loopLength = t_origin.cntLoop;

            t_copyClip.isLoop = t_origin.isLoop;
            t_copyClip.cntLoop = t_loopLength;
            t_copyClip.startLoop = t_origin.startLoop;

            t_copyClip.checkTime = new float[t_loopLength];
            t_copyClip.setTime = new float[t_loopLength];

            for (int i = 0; i < t_loopLength; i++)
            {
                t_copyClip.checkTime[i] = t_origin.checkTime[i];
                t_copyClip.setTime[i] = t_origin.setTime[i];
            }
        }

        return t_copyClip;
}

    #endregion Helper Methods
}
