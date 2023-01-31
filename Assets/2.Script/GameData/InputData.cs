using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public class PlayerKeySetting
{
    public KeyCode[] moveButtons;
    public KeyCode attackButton;
    public KeyCode jumpButton;
    public KeyCode[] skillSlotButtons;
    public KeyCode[] uiButtons;
}

public class InputData : ScriptableObject
{
    #region Variables

    private string dataPath = "Data/inputData";
    private string xmlFilePath = string.Empty;    
    private string xmlFileName = "inputData.xml";

    #endregion Variables

    #region Methods

    public PlayerKeySetting LoadData()
    {
        xmlFilePath = Application.dataPath + FilePath.DataDirectoryPath;
        TextAsset t_asset = Resources.Load(dataPath) as TextAsset;

        if (t_asset == null || t_asset.text == null) return null;

        PlayerKeySetting t_keySetting = new PlayerKeySetting();
        using (XmlReader t_reader = XmlReader.Create(new StringReader(t_asset.text)))
        {
            while (t_reader.Read())
            {
                if (t_reader.IsStartElement(XmlElementName.InputData.MOVE))
                {
                    string[] t_buttons = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_buttons.Length; i++)
                        t_keySetting.moveButtons[i] = (KeyCode)Enum.Parse(typeof(KeyCode), t_buttons[i]);
                }
                if (t_reader.IsStartElement(XmlElementName.InputData.Attack))
                    t_keySetting.attackButton = (KeyCode)int.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.InputData.Jump))
                    t_keySetting.jumpButton = (KeyCode)int.Parse(t_reader.ReadElementContentAsString());
                if (t_reader.IsStartElement(XmlElementName.InputData.SKILLSLOTS))
                {
                    string[] t_buttons = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_buttons.Length; i++)
                        t_keySetting.skillSlotButtons[i] = (KeyCode)Enum.Parse(typeof(KeyCode), t_buttons[i]);
                }
                if (t_reader.IsStartElement(XmlElementName.InputData.UI))
                {
                    string[] t_buttons = t_reader.ReadElementContentAsString().Split('/');
                    for (int i = 0; i < t_buttons.Length; i++)
                        t_keySetting.uiButtons[i] = (KeyCode)Enum.Parse(typeof(KeyCode), t_buttons[i]);
                }
            }
        }

        return t_keySetting;
    }

    public void SaveData(PlayerKeySetting p_keySetting)
    {
        XmlWriterSettings t_setttings = new XmlWriterSettings();
        t_setttings.Encoding = System.Text.Encoding.Unicode;

        using (XmlWriter t_writer = XmlWriter.Create(xmlFilePath + xmlFileName, t_setttings))
        {
            
            t_writer.WriteStartDocument();
            {
                t_writer.WriteStartElement(XmlElementName.INPUT);
                {
                    string t_str = "";
                    foreach (KeyCode t_key in p_keySetting.moveButtons) t_str += t_key.ToString() + "/";
                    t_writer.WriteElementString(XmlElementName.InputData.MOVE, t_str);
                    t_writer.WriteElementString(XmlElementName.InputData.Attack, p_keySetting.attackButton.ToString());
                    t_writer.WriteElementString(XmlElementName.InputData.Jump, p_keySetting.jumpButton.ToString());
                    t_str = "";
                    foreach (KeyCode t_key in p_keySetting.skillSlotButtons) t_str += t_key.ToString() + "/";
                    t_writer.WriteElementString(XmlElementName.InputData.SKILLSLOTS, t_str);
                    t_str = "";
                    foreach (KeyCode t_key in p_keySetting.uiButtons) t_str += t_key.ToString() + "/";
                    t_writer.WriteElementString(XmlElementName.InputData.UI, t_str);
                }
                t_writer.WriteEndElement();
            }
            t_writer.WriteEndDocument();
        }
    }

    #endregion Methods
}
