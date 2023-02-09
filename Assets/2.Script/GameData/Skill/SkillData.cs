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

            while (t_reader.Read())
            {
                if (t_reader.IsStartElement(XmlElementName.SKILL))
                {
                    t_length = int.Parse(t_reader.GetAttribute(XmlElementName.LENGTH));
                    names = new string[t_length];
                    skillStats = new SkillStat[t_length];
                }

                if (t_reader.IsStartElement(XmlElementName.ID))
                {
                    t_curID = int.Parse(t_reader.ReadElementContentAsString());
                    skillStats[t_curID] = new SkillStat();
                }
                if (t_reader.IsStartElement(XmlElementName.NAME))
                    names[t_curID] = t_reader.ReadElementContentAsString();

                if (t_reader.IsStartElement(XmlElementName.SkillData.ICONFILEPATH))
                    skillStats[t_curID].skillIconPath = t_reader.ReadElementContentAsString();
                if (t_reader.IsStartElement(XmlElementName.SkillData.ICONFILENAME))
                    skillStats[t_curID].skillIconName = t_reader.ReadElementContentAsString();
                if (t_reader.IsStartElement(XmlElementName.SkillData.CLASSTYPE))
                    skillStats[t_curID].classType = (EClassType)Enum.Parse(typeof(EClassType), t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLTYPE))
                    skillStats[t_curID].skillType = (SkillStat.ESkillType)Enum.Parse(typeof(SkillStat.ESkillType), t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.COOLTIME))
                    skillStats[t_curID].coolTime = int.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.NEEDMANA))
                    skillStats[t_curID].needMana = int.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.ISNOMOTION))
                    skillStats[t_curID].isNoMotion = bool.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.SkillData.NUMCOMBO))
                {
                    skillStats[t_curID].numCombo = int.Parse(t_reader.ReadElementContentAsString());
                    skillStats[t_curID].skillInfo = new SkillInfo[skillStats[t_curID].numCombo];
                    for (int i = 0; i < skillStats[t_curID].numCombo; i++)
                        skillStats[t_curID].skillInfo[i] = new SkillInfo();
                }

                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLMOTION))
                {
                    string[] t_motions = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_motions.Length; i++)
                    {
                        if (t_motions[i] == string.Empty) continue;
                        skillStats[t_curID].skillInfo[i].skillMotion = t_motions[i];
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.NUMSKILLEFFECT))
                {
                    string[] t_num = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_num.Length; i++)
                    {
                        if (t_num[i] == string.Empty) continue;
                        skillStats[t_curID].skillInfo[i].numSkillEffect = int.Parse(t_num[i]);
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLEFFECTPATH))
                {

                    string[] t_paths = t_reader.ReadElementContentAsString().Split('.');
                    for (int i = 0; i < t_paths.Length; i++)
                    {
                        if (t_paths[i] == string.Empty) continue;
                        string[] t_path = t_paths[i].Split(',');
                        for (int j = 0; j < t_path.Length; j++)
                            skillStats[t_curID].skillInfo[i].skillEffectPaths[j] = t_path[j];
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLEFFECTNAME))
                {

                    string[] t_names = t_reader.ReadElementContentAsString().Split('.');
                    for (int i = 0; i < t_names.Length; i++)
                    {
                        if (t_names[i] == string.Empty) continue;
                        string[] t_name = t_names[i].Split(',');
                        for (int j = 0; j < t_name.Length; j++)
                            skillStats[t_curID].skillInfo[i].skillEffectNames[j] = t_name[j];
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.EFFECTOFFSET))
                {
                    string[] t_offsets = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_offsets.Length; i++)
                    {
                        if (t_offsets[i] == string.Empty) continue;
                        string[] t_effect = t_offsets[i].Split(',');
                        for (int j = 0; j < t_effect.Length; j++)
                        {
                            string[] t_vector = t_effect[j].Split('.');
                            skillStats[t_curID].skillInfo[i].effectOffsets[j].x = int.Parse(t_vector[0]);
                            skillStats[t_curID].skillInfo[i].effectOffsets[j].y = int.Parse(t_vector[1]);
                            skillStats[t_curID].skillInfo[i].effectOffsets[j].z = int.Parse(t_vector[2]);
                        }
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.SKILLRANGE))
                {
                    string[] t_ranges = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_ranges.Length; i++)
                    {
                        if (t_ranges[i] == string.Empty) continue;
                        string[] t_range = t_ranges[i].Split('.');
                        skillStats[t_curID].skillInfo[i].skillRange.x = int.Parse(t_range[0]);
                        skillStats[t_curID].skillInfo[i].skillRange.y = int.Parse(t_range[1]);
                        skillStats[t_curID].skillInfo[i].skillRange.z = int.Parse(t_range[2]);
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.RANGEOFFSET))
                {
                    string[] t_rangeOffsets = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_rangeOffsets.Length; i++)
                    {
                        if (t_rangeOffsets[i] == string.Empty) continue;
                        string[] t_range = t_rangeOffsets[i].Split('.');
                        skillStats[t_curID].skillInfo[i].rangeOffset.x = int.Parse(t_range[0]);
                        skillStats[t_curID].skillInfo[i].rangeOffset.y = int.Parse(t_range[1]);
                        skillStats[t_curID].skillInfo[i].rangeOffset.z = int.Parse(t_range[2]);
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.PREDELAY))
                {
                    string[] t_preDelays = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_preDelays.Length; i++)
                    {
                        if (t_preDelays[i] == string.Empty) continue;
                        skillStats[t_curID].skillInfo[i].preDelay = int.Parse(t_preDelays[i]);
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.DURATION))
                {
                    string[] t_durations = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_durations.Length; i++)
                    {
                        if (t_durations[i] == string.Empty) continue;
                        skillStats[t_curID].skillInfo[i].duration = int.Parse(t_durations[i]);
                    }
                }
                if (t_reader.IsStartElement(XmlElementName.SkillData.POSTDELAY))
                {
                    string[] t_postDelays = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_postDelays.Length; i++)
                    {
                        if (t_postDelays[i] == string.Empty) continue;
                        skillStats[t_curID].skillInfo[i].postDelay = int.Parse(t_postDelays[i]);
                    }
                }

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

                        t_writer.WriteStartElement(XmlElementName.IDENTITY);
                        {
                            t_writer.WriteElementString(XmlElementName.ID, i.ToString());
                            t_writer.WriteElementString(XmlElementName.NAME, names[i]);
                        }
                        t_writer.WriteEndElement();

                        t_writer.WriteStartElement(XmlElementName.SkillData.SKILLSTAT);
                        {
                            t_writer.WriteElementString(XmlElementName.SkillData.ICONFILEPATH, t_clip.skillIconPath);
                            t_writer.WriteElementString(XmlElementName.SkillData.ICONFILENAME, t_clip.skillIconName);
                            t_writer.WriteElementString(XmlElementName.SkillData.CLASSTYPE, t_clip.classType.ToString());
                            t_writer.WriteElementString(XmlElementName.SkillData.SKILLTYPE, t_clip.skillType.ToString());
                            t_writer.WriteElementString(XmlElementName.SkillData.COOLTIME, t_clip.coolTime.ToString());
                            t_writer.WriteElementString(XmlElementName.SkillData.NEEDMANA, t_clip.needMana.ToString());
                            t_writer.WriteElementString(XmlElementName.SkillData.ISNOMOTION, t_clip.isNoMotion.ToString());
                            t_writer.WriteElementString(XmlElementName.SkillData.NUMCOMBO, t_clip.numCombo.ToString());

                            string t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo) t_str += t_info.skillMotion + "/";
                            t_writer.WriteElementString(XmlElementName.SkillData.SKILLMOTION, t_str);

                            t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo) t_str += t_info.numSkillEffect + "/";
                            t_writer.WriteElementString(XmlElementName.SkillData.NUMSKILLEFFECT, t_str);

                            t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo)
                            {
                                foreach (string t_path in t_info.skillEffectPaths) t_str += t_path + ",";
                                t_str += ".";
                            }
                            t_writer.WriteElementString(XmlElementName.SkillData.SKILLEFFECTPATH, t_str);

                            t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo)
                            {
                                foreach (string t_name in t_info.skillEffectNames) t_str += t_name + ",";
                                t_str += ".";
                            }
                            t_writer.WriteElementString(XmlElementName.SkillData.SKILLEFFECTNAME, t_str);

                            t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo)
                            {
                                foreach (Vector3 t_offset in t_info.effectOffsets)
                                    t_str += t_offset.x.ToString() + "." + t_offset.y.ToString() + "." + t_offset.z.ToString() + ",";
                                t_str += "/";
                            }
                            t_writer.WriteElementString(XmlElementName.SkillData.EFFECTOFFSET, t_str);

                            t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo)
                            {
                                t_str += t_info.skillRange.x.ToString() + "." + t_info.skillRange.y.ToString() + "." + t_info.skillRange.z.ToString();
                                t_str += "/";
                            }
                            t_writer.WriteElementString(XmlElementName.SkillData.SKILLRANGE, t_str);

                            t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo)
                            {
                                t_str += t_info.rangeOffset.x.ToString() + "." + t_info.rangeOffset.y.ToString() + "." + t_info.rangeOffset.z.ToString();
                                t_str += "/";
                            }
                            t_writer.WriteElementString(XmlElementName.SkillData.RANGEOFFSET, t_str);

                            t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo) t_str += t_info.preDelay.ToString() + "/";
                            t_writer.WriteElementString(XmlElementName.SkillData.PREDELAY, t_str);

                            t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo) t_str += t_info.duration.ToString() + "/";
                            t_writer.WriteElementString(XmlElementName.SkillData.DURATION, t_str);

                            t_str = "";
                            foreach (SkillInfo t_info in t_clip.skillInfo) t_str += t_info.postDelay.ToString() + "/";
                            t_writer.WriteElementString(XmlElementName.SkillData.POSTDELAY, t_str);
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
        t_copy.isNoMotion = t_origin.isNoMotion;
        t_copy.numCombo = t_origin.numCombo;

        t_copy.skillInfo = new SkillInfo[t_copy.numCombo];
        for (int i = 0; i < t_copy.numCombo; i++)
        {
            t_copy.skillInfo[i] = new SkillInfo();
            t_copy.skillInfo[i].skillMotion = t_origin.skillInfo[i].skillMotion;
            t_copy.skillInfo[i].numSkillEffect = t_origin.skillInfo[i].numSkillEffect;

            t_copy.skillInfo[i].skillEffectPaths = new string[t_copy.skillInfo[i].numSkillEffect];
            t_copy.skillInfo[i].skillEffectNames = new string[t_copy.skillInfo[i].numSkillEffect];
            t_copy.skillInfo[i].skillEffects = new GameObject[t_copy.skillInfo[i].numSkillEffect];
            t_copy.skillInfo[i].effectOffsets= new Vector3[t_copy.skillInfo[i].numSkillEffect];
            for (int j = 0; j < t_copy.skillInfo[i].numSkillEffect; j++)
            {
                t_copy.skillInfo[i].skillEffectPaths[j] = t_origin.skillInfo[i].skillEffectPaths[j];
                t_copy.skillInfo[i].skillEffectNames[j] = t_origin.skillInfo[i].skillEffectNames[j];
                t_copy.skillInfo[i].skillEffects[j] = t_origin.skillInfo[i].skillEffects[j];
                t_copy.skillInfo[i].effectOffsets[j] = t_origin.skillInfo[i].effectOffsets[j];
            }

            t_copy.skillInfo[i].skillRange = t_origin.skillInfo[i].skillRange;
            t_copy.skillInfo[i].rangeOffset = t_origin.skillInfo[i].rangeOffset;
            t_copy.skillInfo[i].preDelay = t_origin.skillInfo[i].preDelay;
            t_copy.skillInfo[i].duration = t_origin.skillInfo[i].duration;
            t_copy.skillInfo[i].postDelay = t_origin.skillInfo[i].postDelay;
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
