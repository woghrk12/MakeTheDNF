using System.Linq;
using System.Text; 
using UnityEditor;
using UnityEngine;

public class SkillTool : EditorWindow
{
    #region Variables

    private static int selection = -1;
    private Vector2 scrollPos1 = Vector2.zero;
    private Vector2 scrollPos2 = Vector2.zero;

    private static SkillData skillData;

    #endregion Variables

    [MenuItem("Tools/Data/SkillData Tool")]
    private static void Init()
    {
        skillData = CreateInstance<SkillData>();
        skillData.LoadData();
        selection = -1;
        GetWindow<SkillTool>(false, "Skill Tool").Show();
    }

    private void OnGUI()
    {
        if (skillData == null) return;

        EditorGUILayout.BeginVertical();
        {
            EditorToolLayer<SkillStat>.EditorToolTopLayer(skillData, ref selection, EditorHelper.uiWidthMiddle);
            EditorGUILayout.BeginHorizontal();
            {
                EditorToolLayer<SkillStat>.EditorToolListLayer(skillData, ref scrollPos1, ref selection, EditorHelper.uiWidthMiddle);
                if (selection >= 0) InfoLayer();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Reload Settings"))
                {
                    skillData = CreateInstance<SkillData>();
                    skillData.LoadData();
                    selection = 0;
                    Debug.Log("Reload Complete!!");
                }
                if (GUILayout.Button("Save Settings"))
                {
                    skillData.SaveData();
                    CreateEnumStructure();
                    AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                    Debug.Log("Save Complete!!");
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    #region Panel Methods

    private void InfoLayer()
    {
        if (skillData.DataCount <= 0) return;

        EditorGUILayout.BeginVertical();
        {
            scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2);
            {
                SkillStat t_clip = skillData.skillStats[selection];

                EditorGUILayout.LabelField("Identity", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("ID", t_clip.skillID.ToString(), GUILayout.Width(EditorHelper.uiWidthLarge));
                skillData.names[selection] = EditorGUILayout.TextField("Name", skillData.names[selection], GUILayout.Width(EditorHelper.uiWidthLarge));

                if (t_clip.skillIcon == null && t_clip.skillIconName != string.Empty) t_clip.PreLoadIcon();
                t_clip.skillIcon = EditorGUILayout.ObjectField("Skill Icon", t_clip.skillIcon, typeof(Sprite), false, GUILayout.Width(EditorHelper.uiWidthLarge)) as Sprite;
                if (t_clip.skillIcon != null)
                {
                    t_clip.skillIconPath = EditorHelper.GetPath(t_clip.skillIcon);
                    t_clip.skillIconName = "Icon_" + skillData.names[selection];
                }
                else
                {
                    t_clip.skillIconPath = string.Empty;
                    t_clip.skillIconName = string.Empty;
                }

                EditorGUILayout.LabelField("Skill Info", EditorStyles.boldLabel);
                t_clip.classType = (EClassType)EditorGUILayout.EnumPopup("Class Type", t_clip.classType, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.skillType = (SkillStat.ESkillType)EditorGUILayout.EnumPopup("Skill Type", t_clip.skillType, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.coolTime = EditorGUILayout.FloatField("Cool Time", t_clip.coolTime, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.needMana = EditorGUILayout.IntField("Need Mana", t_clip.needMana, GUILayout.Width(EditorHelper.uiWidthLarge));
                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Skill Info related to level", EditorStyles.boldLabel);
                t_clip.acquireLevel = (SkillStat.EAcquireLevel)EditorGUILayout.EnumPopup("Acquire Level", t_clip.acquireLevel, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.minLevel = EditorGUILayout.IntField("Min Level", t_clip.minLevel, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.maxLevel = EditorGUILayout.IntField("Max Level", t_clip.maxLevel, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.stepLevel = EditorGUILayout.IntField("Step Level", t_clip.stepLevel, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.needPoint = EditorGUILayout.IntField("Need Point", t_clip.needPoint, GUILayout.Width(EditorHelper.uiWidthLarge));
                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Skill List Info", EditorStyles.boldLabel);

                EditorGUILayout.Separator();

                t_clip.isNoMotion = EditorGUILayout.Toggle("No Motion", t_clip.isNoMotion, GUILayout.Width(EditorHelper.uiWidthLarge));

                if (GUILayout.Button("Add Combo", GUILayout.Width(EditorHelper.uiWidthMiddle))) t_clip.AddCombo();
                if (t_clip.numCombo > 0) ComboLayer(t_clip);
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();
    }

    private void ComboLayer(SkillStat p_clip)
    {
        EditorGUILayout.BeginVertical();
        {
            for (int i = 0; i < p_clip.numCombo; i++)
            {
                SkillInfo t_info = p_clip.skillInfo[i];

                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Combo " + i.ToString(), EditorStyles.boldLabel, GUILayout.Width(EditorHelper.uiWidthSmall));
                    if (GUILayout.Button("Remove", GUILayout.Width(EditorHelper.uiWidthSmall)))
                    {
                        p_clip.RemoveCombo(i);
                        EditorGUILayout.EndHorizontal();
                        continue;
                    }
                }
                EditorGUILayout.EndHorizontal();

                t_info.skillMotion = EditorGUILayout.TextField("Skill Motion", t_info.skillMotion, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_info.numSkillEffect = EditorGUILayout.IntField("Num Effect", t_info.numSkillEffect, GUILayout.Width(EditorHelper.uiWidthLarge));
                for (int j = 0; j < t_info.numSkillEffect; j++)
                {
                    t_info.skillEffects[j] = EditorGUILayout.ObjectField("Effect", t_info.skillEffects[j], typeof(GameObject), false, GUILayout.Width(EditorHelper.uiWidthLarge)) as GameObject;
                    if (t_info.skillEffects[i] != null)
                    {
                        t_info.skillEffectPaths[j] = EditorHelper.GetPath(t_info.skillEffects[j]);
                        t_info.skillEffectNames[j] = t_info.skillEffects[j].name;
                    }
                    else
                    {
                        t_info.skillEffectPaths[j] = string.Empty;
                        t_info.skillEffectNames[j] = string.Empty;
                    }
                    t_info.effectOffsets[j] = EditorGUILayout.Vector3Field("Effect Offset", t_info.effectOffsets[j], GUILayout.Width(EditorHelper.uiWidthLarge));
                }
                t_info.skillRange = EditorGUILayout.Vector3Field("Skill Range", t_info.skillRange, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_info.rangeOffset = EditorGUILayout.Vector3Field("Range Offset", t_info.rangeOffset, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_info.preDelay = EditorGUILayout.FloatField("Pre Delay", t_info.preDelay, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_info.duration = EditorGUILayout.FloatField("Duration", t_info.duration, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_info.postDelay = EditorGUILayout.FloatField("Post Delay", t_info.postDelay, GUILayout.Width(EditorHelper.uiWidthLarge));
            }                     
        }
        EditorGUILayout.EndVertical();
    }

    #endregion Panel Methods

    #region Helper Methods

    private void CreateEnumStructure()
    {
        StringBuilder t_builder = new StringBuilder();
        t_builder.AppendLine();

        int t_lenght = skillData.names != null ? skillData.DataCount : 0;
        for (int i = 0; i < t_lenght; i++)
        {
            if (skillData.names[i] == string.Empty) continue;

            string t_name = skillData.names[i];
            string t_className = skillData.skillStats[i].classType.ToString();
            t_name = string.Concat(t_name.Where(t_char => !char.IsWhiteSpace(t_char)));
            t_builder.AppendLine("    " + t_className + "_" + t_name + " = " + i + ",");
        }
        EditorHelper.CreateEnumStructure("SkillList", t_builder);
    }

    #endregion Helper Methods
}
