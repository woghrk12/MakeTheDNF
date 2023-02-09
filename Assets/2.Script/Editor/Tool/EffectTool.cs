using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class EffectTool : EditorWindow
{
    #region Variables

    private static int selection = -1;
    private Vector2 scrollPos1 = Vector2.zero;
    private Vector2 scrollPos2 = Vector2.zero;

    private static EffectData effectData;

    #endregion Variables

    [MenuItem("Tools/Data/EffectData Tool")]
    private static void Init()
    {
        effectData = CreateInstance<EffectData>();
        effectData.LoadData();
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
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Reload Settings"))
                {
                    effectData = CreateInstance<EffectData>();
                    effectData.LoadData();
                    selection = 0;
                }
                if (GUILayout.Button("Save Settings"))
                {
                    effectData.SaveData();
                    CreateEnumStructure();
                    AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
    }

    #region Panel Methods

    private void InfoLayer()
    {
        if (effectData.DataCount <= 0) return;

        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("Setting", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2);
            {
                EffectClip t_clip = effectData.effectClips[selection];

                EditorGUILayout.LabelField("ID", t_clip.clipID.ToString(), GUILayout.Width(EditorHelper.uiWidthLarge));
                effectData.names[selection] = EditorGUILayout.TextField("Name", effectData.names[selection], GUILayout.Width(EditorHelper.uiWidthLarge));

                if (t_clip.Clip == null && t_clip.clipName != string.Empty) t_clip.PreLoad();

                t_clip.Clip = EditorGUILayout.ObjectField("Effect Clip", t_clip.Clip, typeof(GameObject), false, GUILayout.Width(EditorHelper.uiWidthLarge)) as GameObject;

                if (t_clip.Clip != null)
                {
                    t_clip.clipPath = EditorHelper.GetPath(t_clip.Clip);
                    t_clip.clipName = t_clip.Clip.name;
                }
                else
                {
                    t_clip.clipPath = string.Empty;
                    t_clip.clipName = string.Empty;
                }
            }
            EditorGUILayout.EndScrollView();
        }
        EditorGUILayout.EndVertical();
    }

    #endregion Panel Methods

    #region Helper Methods

    private void CreateEnumStructure()
    {
        StringBuilder t_builder = new StringBuilder();
        t_builder.AppendLine();

        int t_lenght = effectData.names != null ? effectData.DataCount : 0;
        for (int i = 0; i < t_lenght; i++)
        {
            if (effectData.names[i] == string.Empty) continue;

            string t_name = effectData.names[i];
            t_name = string.Concat(t_name.Where(t_char => !char.IsWhiteSpace(t_char)));
            t_builder.AppendLine("    " + t_name + " = " + i + ",");
        }
        EditorHelper.CreateEnumStructure("EffectList", t_builder);
    }

    #endregion Helper Methods
}
