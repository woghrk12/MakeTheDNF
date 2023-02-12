using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

using UnityObject = UnityEngine.Object;

public class SoundTool : EditorWindow
{
    #region Variables

    private static int selection = -1;
    private Vector2 scrollPos1 = Vector2.zero;
    private Vector2 scrollPos2 = Vector2.zero;

    private static SoundData soundData;
    private const string enumName = "SoundList";

    private bool isSelectedAnother = false;
    private ReorderableList loopList;

    #endregion Variables

    [MenuItem("Tools/Data/SoundData Tool")]
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
                int t_oldSelection = selection;
                EditorToolLayer<SoundClip>.EditorToolListLayer(soundData, ref scrollPos1, ref selection, EditorHelper.uiWidthMiddle);
                if (selection >= 0)
                {
                    isSelectedAnother = t_oldSelection != selection;
                    InfoLayer();
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorToolLayer<SoundClip>.EditorToolBottomLayer(soundData, ref selection, enumName);
        }
        EditorGUILayout.EndVertical();
    }

    #region Panel Methods

    private void InfoLayer()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.Separator();
            
            EditorGUILayout.LabelField("Sound Setting", EditorStyles.boldLabel);

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
            scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2);
            {
                SoundClip t_clip = soundData.soundClips[selection];

                if (isSelectedAnother)
                {
                    // Initialize Loop List
                    loopList = new ReorderableList(ArrayHelper.ArrayToList(t_clip.checkTime), typeof(int));
                    loopList.drawElementCallback = (Rect p_rect, int p_idx, bool p_isActive, bool p_isFocused) =>
                    {
                        p_rect.height = EditorGUIUtility.singleLineHeight;

                        p_rect.width = 50f;
                        EditorGUI.LabelField(p_rect, p_idx.ToString());
                        p_rect.x += p_rect.width;

                        p_rect.width = 100f;
                        EditorGUI.LabelField(p_rect, "Check Time");
                        p_rect.x += p_rect.width;

                        p_rect.width = EditorHelper.uiWidthSmall;
                        t_clip.checkTime[p_idx] = EditorGUI.FloatField(p_rect, t_clip.checkTime[p_idx]);
                        p_rect.x += p_rect.width + 50f;

                        p_rect.width = 100f;
                        EditorGUI.LabelField(p_rect, "Set Time");
                        p_rect.x += p_rect.width;

                        p_rect.width = EditorHelper.uiWidthSmall;
                        t_clip.setTime[p_idx] = EditorGUI.FloatField(p_rect, t_clip.setTime[p_idx]);
                    };
                    loopList.drawHeaderCallback = (Rect p_rect) => { EditorGUI.LabelField(p_rect, "Loop List"); };
                    loopList.onAddCallback = (ReorderableList p_list) => 
                    {
                        int t_idx = p_list.index;
                        t_clip.AddLoop();
                        p_list.list.Add(0.0f);
                        p_list.index = t_idx;
                    };
                    loopList.onRemoveCallback = (ReorderableList p_list) => 
                    {
                        int t_idx = p_list.index;
                        p_list.list.RemoveAt(t_idx);
                        t_clip.RemoveLoop(t_idx);
                        p_list.index = 0;
                    };
                }

                // Identity
                EditorGUILayout.LabelField("ID", t_clip.clipID.ToString(), GUILayout.Width(EditorHelper.uiWidthLarge));
                soundData.names[selection] = EditorGUILayout.TextField("Name", soundData.names[selection], GUILayout.Width(EditorHelper.uiWidthLarge));

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                // Audio Clip
                EditorGUILayout.LabelField("Audio Clip", EditorStyles.boldLabel);
                if (t_clip.Clip == null && t_clip.clipName != string.Empty) t_clip.PreLoad();
                t_clip.Clip = EditorGUILayout.ObjectField("Audio Clip", t_clip.Clip, typeof(AudioClip), false, GUILayout.Width(EditorHelper.uiWidthLarge)) as AudioClip;

                if (t_clip.Clip != null)
                {
                    t_clip.clipPath = EditorHelper.GetPath(t_clip.Clip);
                    t_clip.clipName = t_clip.Clip.name;
                    t_clip.maxVolume = EditorGUILayout.FloatField("Max Volume", t_clip.maxVolume, GUILayout.Width(EditorHelper.uiWidthLarge));
                    t_clip.pitch = EditorGUILayout.Slider("Pitch", t_clip.pitch, -3.0f, 3.0f, GUILayout.Width(EditorHelper.uiWidthLarge));
                    t_clip.spatialBlend = EditorGUILayout.Slider("Pan Level", t_clip.spatialBlend, 0.0f, 1.0f, GUILayout.Width(EditorHelper.uiWidthLarge));

                    EditorGUILayout.Separator();
                    EditorGUILayout.Separator();

                    // Loop Option
                    t_clip.isLoop = EditorGUILayout.Toggle("Loop", t_clip.isLoop, GUILayout.Width(EditorHelper.uiWidthLarge));
                    if (t_clip.isLoop) loopList.DoLayoutList();
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
}
