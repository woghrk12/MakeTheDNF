using System.Linq;
using System.Text; 
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class SkillTool : EditorWindow
{
    #region Variables

    private static int selection = -1;
    private Vector2 scrollPos1 = Vector2.zero;
    private Vector2 scrollPos2 = Vector2.zero;

    private static SkillData skillData;
    private const string enumName = "ESkillList";

    private bool isSelectedAnother = false;
    private ReorderableList effectList;
    private ReorderableList canCancelList;
    private ReorderableList preLearnedList;

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
            int t_oldSelection = selection;
            EditorToolLayer<SkillStat>.EditorToolTopLayer(skillData, ref selection, EditorHelper.uiWidthMiddle);
            EditorGUILayout.BeginHorizontal();
            {
                EditorToolLayer<SkillStat>.EditorToolListLayer(skillData, ref scrollPos1, ref selection, EditorHelper.uiWidthMiddle);
                if (selection >= 0)
                {
                    isSelectedAnother = t_oldSelection != selection;
                    InfoLayer();
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorToolLayer<SkillStat>.EditorToolBottomLayer(skillData, ref selection, enumName);
        }
        EditorGUILayout.EndVertical();
    }

    #region Panel Methods

    private void InfoLayer()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Skill Setting", EditorStyles.boldLabel);

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2);
            {
                SkillStat t_clip = skillData.skillStats[selection];

                if (isSelectedAnother)
                {
                    // Initialize Effect List 
                    effectList = new ReorderableList(ArrayHelper.ArrayToList(t_clip.skillEffects), typeof(EEffectList));
                    effectList.index = -1;
                    effectList.drawElementCallback = (Rect p_rect, int p_idx, bool p_isActive, bool p_isFocused) =>
                    {
                        p_rect.y += 3f;
                        p_rect.width = EditorHelper.uiWidthLarge;
                        p_rect.height = EditorGUIUtility.singleLineHeight;
                        t_clip.skillEffects[p_idx] = (EEffectList)EditorGUI.EnumPopup(p_rect, t_clip.skillEffects[p_idx]);
                    };
                    effectList.drawHeaderCallback = (Rect p_rect) => { EditorGUI.LabelField(p_rect, "Effect List"); };
                    effectList.onAddCallback = (ReorderableList p_list) => 
                    {
                        t_clip.AddEffect();
                        p_list.list.Add(EEffectList.NONE);
                    };
                    effectList.onRemoveCallback = (ReorderableList p_list) => 
                    {
                        int t_idx = p_list.index;
                        p_list.list.RemoveAt(t_idx);
                        t_clip.RemoveEffect(t_idx);
                        p_list.index = t_idx - 1;
                    };

                    // Initialize CanCancel List
                    canCancelList = new ReorderableList(t_clip.canCancelList, typeof(int));

                    // Initialize PreLearned List
                    preLearnedList = new ReorderableList(t_clip.preLearnedList, typeof(int));
                }

                // Identity
                EditorGUILayout.LabelField("ID", t_clip.skillID.ToString(), GUILayout.Width(EditorHelper.uiWidthLarge));
                skillData.names[selection] = EditorGUILayout.TextField("Name", skillData.names[selection], GUILayout.Width(EditorHelper.uiWidthLarge));
                
                // Skill Icon
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

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                // Skill Stat
                EditorGUILayout.LabelField("Skill Stat", EditorStyles.boldLabel);
                t_clip.classType = (EClassType)EditorGUILayout.EnumPopup("Class Type", t_clip.classType, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.skillType = (SkillStat.ESkillType)EditorGUILayout.EnumPopup("Skill Type", t_clip.skillType, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.coolTime = EditorGUILayout.FloatField("Cool Time", t_clip.coolTime, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.needMana = EditorGUILayout.IntField("Need Mana", t_clip.needMana, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.preDelay = EditorGUILayout.FloatField("Pre Delay", t_clip.preDelay, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.duration = EditorGUILayout.FloatField("Duration", t_clip.duration, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.postDelay = EditorGUILayout.FloatField("Post Delay", t_clip.postDelay, GUILayout.Width(EditorHelper.uiWidthLarge));

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                // Skill Motion
                EditorGUILayout.LabelField("Skill Motion", EditorStyles.boldLabel);
                t_clip.isNoMotion = EditorGUILayout.Toggle("No Motion", t_clip.isNoMotion, GUILayout.Width(EditorHelper.uiWidthLarge));
                if (t_clip.isNoMotion) t_clip.skillMotion = string.Empty;
                else t_clip.skillMotion = EditorGUILayout.TextField("Skill Motion", t_clip.skillMotion, GUILayout.Width(EditorHelper.uiWidthLarge));

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                // Skill Effect
                EditorGUILayout.LabelField("Skill Effect", EditorStyles.boldLabel);
                effectList.DoLayoutList();

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                // Acquire Level
                EditorGUILayout.LabelField("Acquire Level", EditorStyles.boldLabel);
                t_clip.acquireLevel = (SkillStat.EAcquireLevel)EditorGUILayout.EnumPopup("Acquire Level", t_clip.acquireLevel, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.minLevel = EditorGUILayout.IntField("Min Level", t_clip.minLevel, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.maxLevel = EditorGUILayout.IntField("Max Level", t_clip.maxLevel, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.stepLevel = EditorGUILayout.IntField("Step Level", t_clip.stepLevel, GUILayout.Width(EditorHelper.uiWidthLarge));
                t_clip.needPoint = EditorGUILayout.IntField("Need Point", t_clip.needPoint, GUILayout.Width(EditorHelper.uiWidthLarge));
                
                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                EditorGUILayout.LabelField("Skill List Info", EditorStyles.boldLabel);
                // List Modify

                skillData.skillStats[selection] = t_clip;
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();
    }
   
    #endregion Panel Methods
}
