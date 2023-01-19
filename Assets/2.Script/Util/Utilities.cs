using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
    private static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds WaitForSeconds(float p_sec)
    {
        if (waitForSeconds.ContainsKey(p_sec)) return waitForSeconds[p_sec];
        WaitForSeconds t_wfs = new WaitForSeconds(p_sec);
        waitForSeconds.Add(p_sec, t_wfs);
        return t_wfs;
    }
}
