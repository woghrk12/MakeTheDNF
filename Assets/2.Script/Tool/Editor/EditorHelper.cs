using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

using UnityObject = UnityEngine.Object;

public class EditorHelper
{
    #region Helper Methods

    public static string GetPath(Object p_clip)
    {
        string[] t_pathNode = AssetDatabase.GetAssetPath(p_clip).Split('/');
        string t_retString = string.Empty;
        bool t_isFindResources = false;

        for (int i = 0; i < t_pathNode.Length; i++)
        {
            if (t_isFindResources)
            {
                t_retString += t_pathNode[i] + "/";
                continue;
            }
            if (t_pathNode[i] == "Resources") t_isFindResources = true;
        }

        return t_retString;
    }

    public static void CreateEnumStructure(string p_enumName, StringBuilder p_data)
    {
        string t_enumTemplate = File.ReadAllText(FilePath.EnumTemplateFilePath);

        t_enumTemplate = t_enumTemplate.Replace("$ENUM$", p_enumName);
        t_enumTemplate = t_enumTemplate.Replace("$DATA$", p_data.ToString());

        if (!Directory.Exists(FilePath.EnumFolderPath)) Directory.CreateDirectory(FilePath.EnumFolderPath);

        string t_enumFilePath = FilePath.EnumFolderPath + p_enumName + ".cs";
        if (File.Exists(t_enumFilePath)) File.Delete(t_enumFilePath);
        File.WriteAllText(t_enumFilePath, t_enumTemplate);
    }

    #endregion Helper Methods

    #region Editor Function

    public static void EditorToolTopLayer(BaseData p_data, ref int p_selection, ref UnityObject p_clip, int p_uiWidth)
    {
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Add", GUILayout.Width(p_uiWidth)))
            {
                p_data.AddData("New Clip");
                p_selection = p_data.DataCount - 1;
                p_clip = null;
            }
            if (GUILayout.Button("Copy", GUILayout.Width(p_uiWidth)))
            {
                p_data.CopyData(p_selection);
                p_selection = p_data.DataCount - 1;
                p_clip = null;
            }
            if (GUILayout.Button("Remove", GUILayout.Width(p_uiWidth)))
            {
                p_clip = null; 
                p_data.RemoveData(p_selection);
            }
            if (GUILayout.Button("Clear", GUILayout.Width(p_uiWidth)))
            {
                p_clip = null;
                p_data.ClearData();
            }
            if (p_selection >= p_data.DataCount) p_selection = p_data.DataCount - 1;
        }
        EditorGUILayout.EndHorizontal();
    }

    public static void EditorToolListLayer(BaseData p_data, ref Vector2 p_scrollPos, ref int p_selection, ref UnityObject p_clip, int p_uiWidth)
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(p_uiWidth));
        {
            EditorGUILayout.LabelField("Source List");
            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical("box");
            {
                p_scrollPos = EditorGUILayout.BeginScrollView(p_scrollPos);
                {
                    if (p_data.DataCount > 0)
                    {
                        int t_lastSelection = p_selection;
                        p_selection = GUILayout.SelectionGrid(p_selection, p_data.GetNameList(true), 1);
                        if (t_lastSelection != p_selection) p_clip = null;
                    }
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

    #endregion Editor Function
}
