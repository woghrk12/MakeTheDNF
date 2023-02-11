using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class SkillData : BaseData<SkillStat>
{
    #region Variables

    public SkillStat[] skillStats = new SkillStat[0];

    private string dataPath = "Data/skillData";
    private string xmlFileName = "skillData.xml";

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
            int t_numEffect = 0;

            while (t_reader.Read())
            {
                if (t_reader.IsStartElement(XmlElementName.SKILL))
                {
                    t_length = int.Parse(t_reader.GetAttribute(XmlElementName.LENGTH));
                    names = new string[t_length];
                    skillStats = new SkillStat[t_length];
                }

                // Skill Identity
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLINFO))
                {
                    t_curID = int.Parse(t_reader.GetAttribute(XmlElementName.ID));
                    names[t_curID] = t_reader.GetAttribute(XmlElementName.NAME);
                    skillStats[t_curID] = new SkillStat();
                }
                
                // Skill Icon
                if (t_reader.IsStartElement(XmlElementName.SkillData.ICONFILEPATH))
                    skillStats[t_curID].skillIconPath = t_reader.ReadElementContentAsString();
                if (t_reader.IsStartElement(XmlElementName.SkillData.ICONFILENAME))
                    skillStats[t_curID].skillIconName = t_reader.ReadElementContentAsString();

                // Skill Stat
                if (t_reader.IsStartElement(XmlElementName.SkillData.CLASSTYPE))
                    skillStats[t_curID].classType = (EClassType)Enum.Parse(typeof(EClassType), t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLTYPE))
                    skillStats[t_curID].skillType = (SkillStat.ESkillType)Enum.Parse(typeof(SkillStat.ESkillType), t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.COOLTIME))
                    skillStats[t_curID].coolTime = int.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.NEEDMANA))
                    skillStats[t_curID].needMana = int.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.PREDELAY))
                    skillStats[t_curID].preDelay = float.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.DURATION))
                    skillStats[t_curID].duration = float.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.POSTDELAY))
                    skillStats[t_curID].postDelay = float.Parse(t_reader.ReadElementContentAsString());

                // Skill Motion
                if (t_reader.IsStartElement(XmlElementName.SkillData.ISNOMOTION))
                    skillStats[t_curID].isNoMotion = bool.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLMOTION))
                    skillStats[t_curID].skillMotion = t_reader.ReadElementContentAsString();

                // Skill Effect
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLEFFECT))
                {
                    t_numEffect = int.Parse(t_reader.GetAttribute(XmlElementName.SkillData.NUMSKILLEFFECT));
                    skillStats[t_curID].numSkillEffect = t_numEffect;
                    skillStats[t_curID].skillEffectPaths = new string[t_numEffect];
                    skillStats[t_curID].skillEffectNames = new string[t_numEffect];
                    skillStats[t_curID].skillEffects = new GameObject[t_numEffect];
                    skillStats[t_curID].effectOffsets = new Vector3[t_numEffect];
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLEFFECTPATH))
                {
                    string[] t_paths = t_reader.ReadElementContentAsString().Split(',');
                    for (int i = 0; i < t_paths.Length; i++)
                    {
                        if (t_paths[i] == string.Empty) continue;
                        skillStats[t_curID].skillEffectPaths[i] = t_paths[i];
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLEFFECTNAME))
                {

                    string[] t_names = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_names.Length; i++)
                    {
                        if (t_names[i] == string.Empty) continue;
                        skillStats[t_curID].skillEffectNames[i] = t_names[i];
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.EFFECTOFFSET))
                {
                    string[] t_offsets = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_offsets.Length; i++)
                    {
                        if (t_offsets[i] == string.Empty) continue;
                        string[] t_vector = t_offsets[i].Split(',');
                        skillStats[t_curID].effectOffsets[i].x = int.Parse(t_vector[0]);
                        skillStats[t_curID].effectOffsets[i].y = int.Parse(t_vector[1]);
                        skillStats[t_curID].effectOffsets[i].z = int.Parse(t_vector[2]);
                    }
                }

                // Acquire Level
                if (t_reader.IsStartElement(XmlElementName.SkillData.ACQUIRELEVEL))
                    skillStats[t_curID].acquireLevel = (SkillStat.EAcquireLevel)Enum.Parse(typeof(SkillStat.EAcquireLevel), t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.MINLEVEL))
                    skillStats[t_curID].minLevel = int.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.MAXLEVEL))
                    skillStats[t_curID].maxLevel = int.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.STEPLEVEL))
                    skillStats[t_curID].stepLevel = int.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.NEEDPOINT))
                    skillStats[t_curID].needPoint = int.Parse(t_reader.ReadElementContentAsString());

                // Skill List
                if (t_reader.IsStartElement(XmlElementName.SkillData.CANCANCELLIST))
                {
                    string[] t_canCancelList = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_canCancelList.Length; i++)
                    {
                        if (t_canCancelList[i] == string.Empty) continue;
                        skillStats[t_curID].canCancelList[i] = int.Parse(t_canCancelList[i]);
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.PRELEAREDLIST))
                {
                    string[] t_preLearnedList = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_preLearnedList.Length; i++)
                    {
                        if (t_preLearnedList[i] == string.Empty) continue;
                        skillStats[t_curID].preLearnedList[i] = int.Parse(t_preLearnedList[i]);
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
                t_writer.WriteStartElement(XmlElementName.SKILL);
                t_writer.WriteAttributeString(XmlElementName.LENGTH, t_length.ToString());
                {
                    for (int i = 0; i < t_length; i++)
                    {
                        SkillStat t_clip = skillStats[i];

                        t_writer.WriteStartElement(XmlElementName.SkillData.SKILLINFO);
                        t_writer.WriteAttributeString(XmlElementName.ID, i.ToString());
                        t_writer.WriteAttributeString(XmlElementName.NAME, names[i]);
                        {
                            t_writer.WriteStartElement(XmlElementName.SkillData.SKILLICON);
                            {
                                t_writer.WriteElementString(XmlElementName.SkillData.ICONFILEPATH, t_clip.skillIconPath);
                                t_writer.WriteElementString(XmlElementName.SkillData.ICONFILENAME, t_clip.skillIconName);
                            }
                            t_writer.WriteEndElement();

                            t_writer.WriteStartElement(XmlElementName.SkillData.SKILLSTAT);
                            {
                                t_writer.WriteElementString(XmlElementName.SkillData.CLASSTYPE, t_clip.classType.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.SKILLTYPE, t_clip.skillType.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.COOLTIME, t_clip.coolTime.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.NEEDMANA, t_clip.needMana.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.PREDELAY, t_clip.preDelay.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.DURATION, t_clip.duration.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.POSTDELAY, t_clip.postDelay.ToString());
                            }
                            t_writer.WriteEndElement();

                            t_writer.WriteStartElement(XmlElementName.SkillData.SKILLMOTION);
                            {
                                t_writer.WriteElementString(XmlElementName.SkillData.ISNOMOTION, t_clip.isNoMotion.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.SKILLMOTION, t_clip.skillMotion);
                            }
                            t_writer.WriteEndElement();

                            t_writer.WriteStartElement(XmlElementName.SkillData.SKILLEFFECT);
                            t_writer.WriteAttributeString(XmlElementName.SkillData.NUMSKILLEFFECT, t_clip.numSkillEffect.ToString());
                            {
                                string t_str = "";
                                foreach (string t_path in t_clip.skillEffectPaths) t_str += t_path + ",";
                                t_writer.WriteElementString(XmlElementName.SkillData.SKILLEFFECTPATH, t_str);

                                t_str = "";
                                foreach (string t_name in t_clip.skillEffectNames) t_str += t_name + "/";
                                t_writer.WriteElementString(XmlElementName.SkillData.SKILLEFFECTNAME, t_str);

                                t_str = "";
                                foreach (Vector3 t_offset in t_clip.effectOffsets) 
                                    t_str += t_offset.x.ToString() + "," + t_offset.y.ToString() + "," + t_offset.z.ToString() + "/";
                            }
                            t_writer.WriteEndElement();

                            t_writer.WriteStartElement(XmlElementName.SkillData.ACQUIRE);
                            {
                                t_writer.WriteElementString(XmlElementName.SkillData.ACQUIRELEVEL, t_clip.acquireLevel.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.MINLEVEL, t_clip.minLevel.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.MAXLEVEL, t_clip.maxLevel.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.STEPLEVEL, t_clip.stepLevel.ToString());
                                t_writer.WriteElementString(XmlElementName.SkillData.NEEDPOINT, t_clip.needPoint.ToString());
                            }
                            t_writer.WriteEndElement();

                            t_writer.WriteStartElement(XmlElementName.SkillData.LIST);
                            {
                                string t_str = "";

                                foreach (int t_id in t_clip.canCancelList) t_str += t_id.ToString() + "/";
                                t_writer.WriteElementString(XmlElementName.SkillData.CANCANCELLIST, t_str);

                                foreach (int t_id in t_clip.preLearnedList) t_str += t_id.ToString() + "/";
                                t_writer.WriteElementString(XmlElementName.SkillData.PRELEAREDLIST, t_str);
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

    public override SkillStat GetClip(int p_idx, bool p_isCopy = false)
    {
        if (p_idx < 0 || p_idx >= DataCount) return null;

        SkillStat t_origin = skillStats[p_idx];

        if (!p_isCopy)
        {
            t_origin.PreLoadEffect();
            t_origin.PreLoadIcon();
            return t_origin;
        }

        SkillStat t_copy = new SkillStat();

        t_copy.skillID = t_origin.skillID;

        t_copy.skillIconPath = t_origin.skillIconPath;
        t_copy.skillIconName = t_origin.skillIconName;
        t_copy.skillIcon = t_origin.skillIcon;

        t_copy.classType = t_origin.classType;
        t_copy.skillType = t_origin.skillType;
        t_copy.coolTime = t_origin.coolTime;
        t_copy.needMana = t_origin.needMana;
        t_copy.preDelay = t_origin.preDelay;
        t_copy.duration = t_origin.duration;
        t_copy.postDelay = t_origin.postDelay;

        t_copy.isNoMotion = t_origin.isNoMotion;
        t_copy.skillMotion = t_origin.skillMotion;

        int t_numEffect = t_copy.numSkillEffect = t_origin.numSkillEffect;
        t_copy.skillEffectPaths = new string[t_numEffect];
        t_copy.skillEffectNames = new string[t_numEffect];
        t_copy.skillEffects = new GameObject[t_numEffect];
        t_copy.effectOffsets = new Vector3[t_numEffect];
        for (int i = 0; i < t_numEffect; i++)
        {
            t_copy.skillEffectPaths[i] = t_origin.skillEffectPaths[i];
            t_copy.skillEffectNames[i] = t_origin.skillEffectNames[i];
            t_copy.skillEffects[i] = t_origin.skillEffects[i];
            t_copy.effectOffsets[i] = t_origin.effectOffsets[i];
        }

        t_copy.acquireLevel = t_origin.acquireLevel;
        t_copy.minLevel = t_origin.minLevel;
        t_copy.maxLevel = t_origin.maxLevel;
        t_copy.stepLevel = t_origin.stepLevel;
        t_copy.needPoint = t_origin.needPoint;

        t_copy.canCancelList = new List<int>();
        foreach (int t_skillID in t_origin.canCancelList)
            t_copy.canCancelList.Add(t_skillID);

        t_copy.preLearnedList = new List<int>();
        foreach (int t_skillID in t_origin.preLearnedList)
            t_copy.preLearnedList.Add(t_skillID);

        return t_copy;
    }

    public override void AddData(string p_newName)
    {
        if (names == null)
        {
            names = new string[] { p_newName };
            skillStats = new SkillStat[] { new SkillStat() };
            return;
        }

        names = ArrayHelper.Add(p_newName, names);
        skillStats = ArrayHelper.Add(new SkillStat(), skillStats);
    }

    public override void RemoveData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;
        if (DataCount <= 0) return;

        names = ArrayHelper.Remove(p_idx, names);
        skillStats = ArrayHelper.Remove(p_idx, skillStats);

        if (DataCount <= 0)
        {
            names = null;
            skillStats = null;
        }
    }

    public override void ClearData()
    {
        foreach (SkillStat t_clip in skillStats)
        {
            t_clip.ReleaseEffect();
            t_clip.ReleaseIcon();
        }

        names = null;
        skillStats = null;
    }

    public override void CopyData(int p_idx)
    {
        if (p_idx < 0 || p_idx >= DataCount) return;

        names = ArrayHelper.Add(names[p_idx], names);
        skillStats = ArrayHelper.Add(skillStats[p_idx], skillStats);
    }

    #endregion Override Methods
}
