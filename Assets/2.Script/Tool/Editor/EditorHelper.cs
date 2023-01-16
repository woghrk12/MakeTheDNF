using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;

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
}
