using UnityEditor;
using UnityEngine;

public class EffectTool : EditorWindow
{
    #region Variables

    private static int selection = -1;
    private Vector2 scrollPos1 = Vector2.zero;
    private Vector2 scrollPos2 = Vector2.zero;

    private static EffectData effectData;
    private const string dataPath = "Assets/9.Resources/Resources/Data/Database/EffectData.asset";
    private const string enumName = "EEffectList";

    #endregion Variables

    [MenuItem("Tools/Data/Effect Tool")]
    private static void Init()
    {
        effectData = AssetDatabase.LoadAssetAtPath<EffectData>(dataPath);
        if (effectData == null)
        {
            effectData = CreateInstance<EffectData>();
            AssetDatabase.CreateAsset(effectData, dataPath);
            AssetDatabase.Refresh();
        }
        selection = -1;
        GetWindow<EffectTool>(false, "Effect Tool").Show();
    }

    private void OnGUI()
    {
        if (effectData == null) return;

        EditorGUILayout.BeginVertical();
        {
            EditorToolLayer<EffectClip>.EditorToolTopLayer(effectData, ref selection, EditorHelper.uiWidthMiddle);
            EditorGUILayout.BeginHorizontal();
            {
                EditorToolLayer<EffectClip>.EditorToolListLayer(effectData, ref scrollPos1, ref selection, EditorHelper.uiWidthMiddle);
                if (selection >= 0) InfoLayer();
                EditorUtility.SetDirty(effectData);
            }
            EditorGUILayout.EndHorizontal();
            EditorToolLayer<EffectClip>.EditorToolBottomLayer(effectData, ref selection, enumName);
        }
        EditorGUILayout.EndVertical();
    }

    #region Panel Methods

    private void InfoLayer()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.Separator();

            EditorGUILayout.LabelField("Effect Setting", EditorStyles.boldLabel);

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2);
            {
                EffectClip t_clip = effectData.GetData(selection);

                // Identity
                EditorGUILayout.LabelField("ID", t_clip.clipID.ToString(), GUILayout.Width(EditorHelper.uiWidthLarge));
                var t_name = effectData.GetName(selection);
                t_name = EditorGUILayout.TextField("Name", t_name, GUILayout.Width(EditorHelper.uiWidthLarge));
                effectData.SetName(selection, t_name);

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                // Effect Clip
                if (t_clip.Clip == null && t_clip.clipName != string.Empty) t_clip.PreLoad();
                t_clip.Clip = EditorGUILayout.ObjectField("Effect Clip", t_clip.Clip, typeof(GameObject), false, GUILayout.Width(EditorHelper.uiWidthLarge)) as GameObject;

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                if (t_clip.Clip != null)
                {
                    t_clip.clipPath = EditorHelper.GetPath(t_clip.Clip);
                    t_clip.clipName = t_clip.Clip.name;

                    // Options
                    EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
                    t_clip.effectType = (EffectClip.EEffectType)EditorGUILayout.EnumPopup("Effect Type", t_clip.effectType, GUILayout.Width(EditorHelper.uiWidthLarge));
                }
                else
                {
                    t_clip.clipPath = string.Empty;
                    t_clip.clipName = string.Empty;
                }

                effectData.SetData(selection, t_clip);
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();
    }

    #endregion Panel Methods
}