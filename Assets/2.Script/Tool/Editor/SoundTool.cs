using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

using UnityObject = UnityEngine.Object;

public class SoundTool : EditorWindow
{
    #region Variables

    private static int selection = -1;
    private Vector2 scrollPos1 = Vector2.zero;
    private Vector2 scrollPos2 = Vector2.zero;

    private static SoundData soundData;

    #endregion Variables

    [MenuItem("Tools/SoundData Tool")]
    private static void Init()
    {
        soundData = CreateInstance<SoundData>();
        soundData.LoadData();
        selection = -1;
        GetWindow<SoundTool>(false, "Sound Tool").Show();
    }

    private void OnGUI()
    {
        if (soundData == null) return;

        EditorGUILayout.BeginVertical();
        {
            EditorToolLayer<SoundClip>.EditorToolTopLayer(soundData, ref selection, EditorHelper.uiWidthMiddle);
            
            EditorGUILayout.BeginHorizontal();
            {
                EditorToolLayer<SoundClip>.EditorToolListLayer(soundData, ref scrollPos1, ref selection, EditorHelper.uiWidthMiddle);
                if(selection >= 0) InfoLayer();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Reload Settings"))
                {
                    soundData = CreateInstance<SoundData>();
                    soundData.LoadData();
                    selection = 0;
                }
                if (GUILayout.Button("Save Settings"))
                {
                    soundData.SaveData();
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
        if (soundData.DataCount <= 0) return;

        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField("Setting", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2);
            {
                SoundClip t_clip = soundData.soundClips[selection];

                EditorGUILayout.LabelField("ID", t_clip.clipID.ToString(), GUILayout.Width(EditorHelper.uiWidthLarge));
                soundData.names[selection] = EditorGUILayout.TextField("Name", soundData.names[selection], GUILayout.Width(EditorHelper.uiWidthLarge));

                if (t_clip.Clip == null && t_clip.clipName != string.Empty) t_clip.PreLoad();

                t_clip.Clip = EditorGUILayout.ObjectField("Audio Clip", t_clip.Clip, typeof(AudioClip), false, GUILayout.Width(EditorHelper.uiWidthLarge)) as AudioClip;

                if (t_clip.Clip != null)
                {
                    t_clip.clipPath = EditorHelper.GetPath(t_clip.Clip);
                    t_clip.clipName = t_clip.Clip.name;

                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Options");
                    t_clip.maxVolume = EditorGUILayout.FloatField("Max Volume", t_clip.maxVolume, GUILayout.Width(EditorHelper.uiWidthLarge));
                    t_clip.pitch = EditorGUILayout.Slider("Pitch", t_clip.pitch, -3.0f, 3.0f, GUILayout.Width(EditorHelper.uiWidthLarge));
                    t_clip.spatialBlend = EditorGUILayout.Slider("Pan Level", t_clip.spatialBlend, 0.0f, 1.0f, GUILayout.Width(EditorHelper.uiWidthLarge));

                    EditorGUILayout.Separator();
                    t_clip.isLoop = EditorGUILayout.Toggle("Loop", t_clip.isLoop, GUILayout.Width(EditorHelper.uiWidthLarge));
                    if (t_clip.isLoop)
                    {
                        if (GUILayout.Button("Add Loop", GUILayout.Width(EditorHelper.uiWidthMiddle))) t_clip.AddLoop();
                        for (int i = 0; i < t_clip.cntLoop; i++)
                        {
                            EditorGUILayout.BeginVertical();
                            {
                                EditorGUILayout.LabelField("Loop Step " + i, EditorStyles.boldLabel);
                                if (GUILayout.Button("Remove", GUILayout.Width(EditorHelper.uiWidthMiddle)))
                                {
                                    t_clip.RemoveLoop(i);
                                    EditorGUILayout.EndVertical();
                                    continue;
                                }
                                t_clip.checkTime[i] = EditorGUILayout.FloatField("Check Time", t_clip.checkTime[i], GUILayout.Width(EditorHelper.uiWidthMiddle));
                                t_clip.setTime[i] = EditorGUILayout.FloatField("Set Time", t_clip.setTime[i], GUILayout.Width(EditorHelper.uiWidthMiddle));
                            }
                            EditorGUILayout.EndVertical();
                        }
                    }
                }
                else
                {
                    t_clip.clipPath = string.Empty;
                    t_clip.clipName = string.Empty;
                }

                soundData.soundClips[selection] = t_clip;
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

        int t_lenght = soundData.names != null ? soundData.DataCount : 0;
        for (int i = 0; i < t_lenght; i++)
        {
            if (soundData.names[i] == string.Empty) continue;

            string t_name = soundData.names[i];
            t_name = string.Concat(t_name.Where(t_char => !char.IsWhiteSpace(t_char)));
            t_builder.AppendLine("    " + t_name + " = " + i + ",");
        }
        EditorHelper.CreateEnumStructure("SoundList", t_builder);
    }

    #endregion Helper Methods
}
