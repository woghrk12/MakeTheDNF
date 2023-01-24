using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Hitbox))]
public class Hitbox_Editor : Editor
{
    #region Variables

    private Hitbox hitbox = null;

    #endregion Variables

    public override void OnInspectorGUI()
    {
        hitbox = (Hitbox)target;
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        if (!hitbox || hitbox.hitboxType == Hitbox.EHitboxType.NONE) return;

        if (hitbox.hitboxType == Hitbox.EHitboxType.BOX)
        {
            Handles.color = Color.red;
            hitbox.sizeX = Handles.ScaleSlider(hitbox.sizeX, hitbox.transform.position, hitbox.transform.right, Quaternion.identity, 1f, 0.1f);
            Handles.color = Color.blue;
            hitbox.sizeZ = Handles.ScaleSlider(hitbox.sizeZ, hitbox.transform.position, hitbox.transform.up, Quaternion.identity, 1f, 0.1f);
            Handles.color = Color.red;
            Handles.DrawWireCube(hitbox.transform.position, new Vector3(hitbox.sizeX, hitbox.sizeZ * DNFTransform.convRate, 0f));
        }
        else if (hitbox.hitboxType == Hitbox.EHitboxType.CIRCLE)
        {
            Handles.color = Color.red;   
            hitbox.sizeX = Handles.ScaleSlider(hitbox.sizeX, hitbox.transform.position, hitbox.transform.right, Quaternion.identity, 1f, 0.1f);
            float t_angle = Mathf.Acos(DNFTransform.convRate) * Mathf.Rad2Deg;
            Handles.color = Color.red;
            Handles.DrawWireArc(hitbox.transform.position, Quaternion.AngleAxis(t_angle, hitbox.transform.right) * hitbox.transform.forward, hitbox.transform.right, 360, hitbox.sizeX * 0.5f);
        }

        Handles.color = Color.green;
        hitbox.sizeY = Handles.ScaleSlider(hitbox.sizeY, hitbox.transform.position + Vector3.left, hitbox.transform.up, Quaternion.identity, 1f, 0.1f);
        Handles.DrawWireCube(hitbox.transform.position + new Vector3(0f, hitbox.sizeY * 0.5f, 0f), new Vector3(hitbox.sizeX, hitbox.sizeY, 0f));
    }
}
