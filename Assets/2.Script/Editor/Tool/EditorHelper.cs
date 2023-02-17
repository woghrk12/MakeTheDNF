using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

using UnityObject = UnityEngine.Object;

public class EditorHelper
{
    #region Variables

    public static readonly int uiWidthLarge = 450;
    public static readonly int uiWidthMiddle = 300;
    public static readonly int uiWidthSmall = 200;

    #endregion Variables

    #region Helper Methods

    public static string GetPath(Object p_clip)
    {
        string[] t_pathNode = AssetDatabase.GetAssetPath(p_clip).Split('/');
        string t_retString = string.Empty;
        bool t_isFindResources = false;

        for (int i = 0; i < t_pathNode.Length - 1; i++)
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
}

public class EditorToolLayer<T>
{
    #region Editor Methods

    public static void EditorToolTopLayer(BaseData<T> p_data, ref int p_selection, int p_uiWidth)
    {
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Add", GUILayout.Width(p_uiWidth)))
            {
                p_data.AddData("New Clip");
                p_selection = p_data.DataCount - 1;
            }
            if (GUILayout.Button("Copy", GUILayout.Width(p_uiWidth)))
            {
                p_data.CopyData(p_selection);
                p_selection = p_data.DataCount - 1;
            }
            if (p_data.DataCount > 0 && GUILayout.Button("Remove", GUILayout.Width(p_uiWidth)))
            {
                p_data.RemoveData(p_selection);
            }
            if (p_data.DataCount > 0 && GUILayout.Button("Clear", GUILayout.Width(p_uiWidth)))
            {
                p_data.ClearData();
            }
            if (p_selection >= p_data.DataCount) p_selection = p_data.DataCount - 1;
        }
        EditorGUILayout.EndHorizontal();
    }

    public static void EditorToolListLayer(BaseData<T> p_data, ref Vector2 p_scrollPos, ref int p_selection, int p_uiWidth)
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
                        p_selection = GUILayout.SelectionGrid(p_selection, p_data.GetNameList(true), 1);
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

    public static void EditorToolBottomLayer(BaseData<T> p_data, ref int p_selection, string p_enumName)
    {
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Reload Settings"))
            {
                p_data = ScriptableObject.CreateInstance<BaseData<T>>();
                
                p_selection = 0;
            }
            if (GUILayout.Button("Save Settings"))
            {
                AssetDatabase.SaveAssets();
                CreateEnumStructure(p_data, p_enumName);
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    #endregion Editor Methods

    #region Helper Methods

    private static void CreateEnumStructure(BaseData<T> p_data, string p_enumName)
    {
        StringBuilder t_builder = new StringBuilder();
        t_builder.AppendLine();

        int t_lenght = p_data.DataCount;
        var t_nameList = p_data.GetNameList(false);
        
        for (int i = 0; i < t_lenght; i++)
        {
            if (t_nameList[i] == string.Empty) continue;

            string t_name = t_nameList[i];
            t_name = string.Concat(t_name.Where(t_char => !char.IsWhiteSpace(t_char)));
            t_builder.AppendLine("    " + t_name + " = " + i + ",");
        }

        EditorHelper.CreateEnumStructure(p_enumName, t_builder);
    }

    #endregion Helper Methods
}
