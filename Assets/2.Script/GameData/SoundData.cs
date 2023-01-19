using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class SoundData : BaseData
{
    #region Variables

    public SoundClip[] soundClips = new SoundClip[0];

    private string dataPath = "Data/soundData";
    private string xmlFileName = "soundData.xml";

    #endregion Variables

    #region Override Methods

    public override void LoadData()
    {
        xmlFilePath = Application.dataPath + FilePath.DataDirectoryPath;
        TextAsset t_asset = Resources.Load(dataPath) as TextAsset;
        if (t_asset == null || t_asset.text == null)
        {
            AddData("New Clip");
            return;
        }

        using (XmlReader t_reader = XmlReader.Create(new StringReader(t_asset.text)))
        {
            int t_length = 0;
            int t_curID = 0;

            while (t_reader.Read())
            {
                if (t_reader.IsStartElement(XmlElementName.SOUND))
                {
                    t_length = int.Parse(t_reader.GetAttribute(XmlElementName.LENGTH));
                    names = new string[t_length];
                    soundClips = new SoundClip[t_length];
                }
                if (t_reader.IsStartElement(XmlElementName.IDENTITY))
                {
                    t_curID = int.Parse(t_reader.GetAttribute(XmlElementName.ID));
                    names[t_curID] = t_reader.GetAttribute(XmlElementName.NAME);
                    soundClips[t_curID] = new SoundClip();
                    soundClips[t_curID].clipPath = t_reader.GetAttribute(XmlElementName.CLIPPATH);
                    soundClips[t_curID].clipName = t_reader.GetAttribute(XmlElementName.CLIPNAME);
                }
                if (t_reader.IsStartElement(XmlElementName.SoundData.OPTIONS))
                {
                    soundClips[t_curID].playType = (SoundClip.ESoundPlayType)Enum.Parse(typeof(SoundClip.ESoundPlayType), t_reader.GetAttribute(XmlElementName.SoundData.PLAYTYPE));
                    soundClips[t_curID].maxVolume = float.Parse(t_reader.GetAttribute(XmlElementName.SoundData.MAXVOLUME));
                    soundClips[t_curID].pitch = float.Parse(t_reader.GetAttribute(XmlElementName.SoundData.PITCH));
                    soundClips[t_curID].spatialBlend = float.Parse(t_reader.GetAttribute(XmlElementName.SoundData.SPATIALBLEND));
                }
                if(t_reader.IsStartElement(XmlElementName.SoundData.LOOPOPTIONS))
                {
                    soundClips[t_curID].isLoop = bool.Parse(t_reader.GetAttribute(XmlElementName.SoundData.ISLOOP));
                    int t_cntLoop = int.Parse(t_reader.GetAttribute(XmlElementName.SoundData.CNTLOOP));
                    soundClips[t_curID].cntLoop = t_cntLoop;
                    soundClips[t_curID].checkTime = new float[t_cntLoop];
                    soundClips[t_curID].setTime = new float[t_cntLoop];

                    string[] t_time = t_reader.GetAttribute(XmlElementName.SoundData.CHECKTIME).Split('/');
                    for (int i = 0; i < t_time.Length; i++)
                    {
                        if (t_time[i] == string.Empty) continue;
                        soundClips[i].checkTime[i] = float.Parse(t_time[i]);
                    }

                    t_time = t_reader.GetAttribute(XmlElementName.SoundData.SETTIME).Split('/');
                    for (int i = 0; i < t_time.Length; i++)
                    {
                        if (t_time[i] == string.Empty) continue;
                        soundClips[i].checkTime[i] = float.Parse(t_time[i]);
                    }
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
            {
                int t_length = DataCount;
                t_writer.WriteStartElement(XmlElementName.SOUND);
                t_writer.WriteAttributeString(XmlElementName.LENGTH, t_length.ToString());
                {
                    for (int i = 0; i < t_length; i++)
                    {
                        SoundClip t_clip = soundClips[i];
                        t_writer.WriteStartElement(XmlElementName.IDENTITY);
                        t_writer.WriteAttributeString(XmlElementName.ID, i.ToString());
                        t_writer.WriteAttributeString(XmlElementName.NAME, names[i]);
                        t_writer.WriteAttributeString(XmlElementName.CLIPPATH, t_clip.clipPath);
                        t_writer.WriteAttributeString(XmlElementName.CLIPNAME, t_clip.clipName);
                        t_writer.WriteEndElement();

                        t_writer.WriteStartElement(XmlElementName.SoundData.OPTIONS);
                        t_writer.WriteAttributeString(XmlElementName.SoundData.PLAYTYPE, t_clip.playType.ToString());
                        t_writer.WriteAttributeString(XmlElementName.SoundData.MAXVOLUME, t_clip.maxVolume.ToString());
                        t_writer.WriteAttributeString(XmlElementName.SoundData.PITCH, t_clip.pitch.ToString());
                        t_writer.WriteAttributeString(XmlElementName.SoundData.SPATIALBLEND, t_clip.spatialBlend.ToString());
                        t_writer.WriteEndElement();

                        if (t_clip.isLoop)
                        {
                            t_writer.WriteStartElement(XmlElementName.SoundData.LOOPOPTIONS);
                            t_writer.WriteAttributeString(XmlElementName.SoundData.ISLOOP, t_clip.isLoop.ToString());
                            t_writer.WriteAttributeString(XmlElementName.SoundData.CNTLOOP, t_clip.cntLoop.ToString());
                            t_writer.WriteAttributeString(XmlElementName.SoundData.STARTLOOP, t_clip.startLoop.ToString());

                            string t_str = "";
                            foreach (float t_checkTime in t_clip.checkTime) t_str += t_checkTime.ToString() + "/";
                            t_writer.WriteAttributeString(XmlElementName.SoundData.CHECKTIME, t_str);

                            t_str = "";
                            foreach (float t_setTime in t_clip.setTime) t_str += t_setTime.ToString() + "/";
                            t_writer.WriteAttributeString(XmlElementName.SoundData.SETTIME, t_str);
                            t_writer.WriteEndElement();
                        }
                    }
                }
                t_writer.WriteEndElement();
            }
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
