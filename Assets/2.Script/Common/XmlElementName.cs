using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlElementName
{
    public const string SOUND = "Sound";
    // Data Attribute
    public const string LENGTH = "Length";

    // Identity Element
    public const string IDENTITY = "Identity";
    // Common Identity Attribute
    public const string ID = "ID";
    public const string NAME = "Name";
    public const string CLIPPATH = "ClipPath";
    public const string CLIPNAME = "ClipName";

    public class SoundData
    {
        // Options Element
        public const string OPTIONS = "Options";
        // Options Attribute
        public const string PLAYTYPE = "PlayType";
        public const string MAXVOLUME = "MaxVolume";
        public const string PITCH = "Pitch";
        public const string SPATIALBLEND = "SpatialBlend";

        // Loop Element
        public const string LOOPOPTIONS = "LoopOptions";
        // Loop Attribute
        public const string ISLOOP = "Loop";
        public const string CNTLOOP = "CntLoop";
        public const string STARTLOOP = "StartLoop";
        public const string CHECKTIME = "CheckTime";
        public const string SETTIME = "SetTime";
    }
}
