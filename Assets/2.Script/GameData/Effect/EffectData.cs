using System;
using System.IO;
using System.Xml;
using UnityEngine;

public class EffectData : BaseData<EffectClip>
{
    #region Variables

    public EffectClip[] effectClips = new EffectClip[0];

    private string dataPath = "Data/effectData";
    private string xmlFileName = "effectData.xml";

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
                if (t_reader.IsStartElement(XmlElementName.EFFECT))
                {
                    t_length = int.Parse(t_reader.GetAttribute(XmlElementName.LENGTH));
                    names = new string[t_length];
                    effectClips = new EffectClip[t_length];
                }

                // Effect Identity
                if (t_reader.IsStartElement(XmlElementName.EffectData.EFFECTSETTINGS))
                {
                    t_curID = int.Parse(t_reader.GetAttribute(XmlElementName.ID));
                    names[t_curID] = t_reader.GetAttribute(XmlElementName.NAME);
                    effectClips[t_curID] = new EffectClip();
                }

                // Effect Clip
                if (t_reader.IsStartElement(XmlElementName.EffectData.CLIPPATH))
                    effectClips[t_curID].clipPath = t_reader.ReadElementContentAsString();
                if (t_reader.IsStartElement(XmlElementName.EffectData.CLIPNAME))
                    effectClips[t_curID].clipName = t_reader.ReadElementContentAsString();

                // Options
                if (t_reader.IsStartElement(XmlElementName.EffectData.PLAYTYPE))
                    effectClips[t_curID].effectType = (EffectClip.EEffectType)Enum.Parse(typeof(EffectClip.EEffectType), t_reader.ReadElementContentAsString());
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
                t_writer.WriteStartElement(XmlElementName.EFFECT);
                t_writer.WriteAttributeString(XmlElementName.LENGTH, t_length.ToString());
                {
                    for (int i = 0; i < t_length; i++)
                    {
                        EffectClip t_clip = effectClips[i];

                        t_writer.WriteStartElement(XmlElementName.EffectData.EFFECTSETTINGS);
                        t_writer.WriteAttributeString(XmlElementName.ID, i.ToString());
                        t_writer.WriteAttributeString(XmlElementName.NAME, names[i]);
                        {
                            t_writer.WriteStartElement(XmlElementName.EffectData.EFFECTCLIP);
                            {
                                t_writer.WriteElementString(XmlElementName.EffectData.CLIPPATH, t_clip.clipPath);
                                t_writer.WriteElementString(XmlElementName.EffectData.CLIPNAME, t_clip.clipName);
                            }
                            t_writer.WriteEndElement();

                            t_writer.WriteStartElement(XmlElementName.EffectData.OPTIONS);
                            {
                                t_writer.WriteElementString(XmlElementName.EffectData.PLAYTYPE, t_clip.effectType.ToString());
                            }
                            t_writer.WriteEndElement();
                        }
                        t_writer.WriteEndElement();
                    }
                }
                t_writer.WriteEndElement();
            }
            t_writer.WriteEndDocument();
        }
    }

    public override EffectClip GetCopyClip(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return null;

        EffectClip t_origin = effectClips[p_idx];
        EffectClip t_copyClip = new EffectClip();

        t_copyClip.clipID = t_origin.clipID;
        t_copyClip.clipPath = t_origin.clipPath;
        t_copyClip.clipName = t_origin.clipName;
        t_copyClip.Clip = t_origin.Clip;
        t_copyClip.effectType = t_origin.effectType;

        return t_copyClip;
    }

    public override void AddData(string p_newName)
    {
        if (names == null)
        {
            names = new string[] { p_newName };
            effectClips = new EffectClip[] { new EffectClip() };
            return;
        }

        names = ArrayHelper.Add(p_newName, names);
        effectClips = ArrayHelper.Add(new EffectClip(), effectClips);
    }

    public override void RemoveData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;
        if (DataCount <= 0) return;

        names = ArrayHelper.Remove(p_idx, names);
        effectClips = ArrayHelper.Remove(p_idx, effectClips);

        if (DataCount <= 0)
        {
            names = null;
            effectClips = null;
        }
    }

    public override void ClearData()
    {
        foreach (EffectClip t_clip in effectClips) t_clip.ReleaseClip();

        names = null;
        effectClips = null;
    }

    public override void CopyData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;

        names = ArrayHelper.Add(names[p_idx], names);
        effectClips = ArrayHelper.Add(effectClips[p_idx], effectClips);
    }

    #endregion Override Methods
}