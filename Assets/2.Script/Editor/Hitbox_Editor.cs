using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Hitbox))]
public class Hitbox_Editor : Editor
{
    #region Variables

    private bool isChangeOffset = false;
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

        Event t_curEvent = Event.current;
        if (!Application.isPlaying && t_curEvent.type == EventType.KeyDown && t_curEvent.keyCode == KeyCode.F1) isChangeOffset = !isChangeOffset;

        var t_origin = new Vector3(hitbox.CharTransform.X, hitbox.CharTransform.Y + hitbox.CharTransform.Z * DNFTransform.convRate, 0f);
        var t_center = t_origin + new Vector3(hitbox.offset.x, hitbox.offset.y + hitbox.offset.z * DNFTransform.convRate, 0f);

        if (isChangeOffset)
        {
            // Set offset along X axis of DNF transform
            Handles.color = Color.red;
            t_center = Handles.Slider(t_center, hitbox.transform.right);
            hitbox.offset.x = t_center.x - t_origin.x;

            // Set offset along Z axis of DNF transform
            Handles.color = Color.blue;
            t_center = Handles.Slider(t_center, hitbox.transform.up * (-0.1f));
            hitbox.offset.z = (t_center.y - t_origin.y - hitbox.offset.y) * DNFTransform.invConvRate;

            // Set offset along Y axis of DNF transform
            Handles.color = Color.green;
            t_center = Handles.Slider(t_center, hitbox.transform.up);
            hitbox.offset.y = t_center.y - t_origin.y - hitbox.offset.z * DNFTransform.convRate;
        }
        else
        {
            // Set size along x axis of DNF transform
            Handles.color = Color.red;
            hitbox.size.x = Handles.ScaleSlider(hitbox.size.x, t_center, hitbox.transform.right, Quaternion.identity, 1f, 0.1f);

            if (hitbox.hitboxType == Hitbox.EHitboxType.BOX)
            {
                // Set size along z axis of DNF transform
                Handles.color = Color.blue;
                hitbox.size.z = Handles.ScaleSlider(hitbox.size.z, t_center, hitbox.transform.up * (-1.0f), Quaternion.identity, 1f, 0.1f);
            }

            // Set size along y axis of DNF transform
            Handles.color = Color.green;
            hitbox.size.y = Handles.ScaleSlider(hitbox.size.y, t_center, hitbox.transform.up, Quaternion.identity, 1f, 0.1f);
        }

        Handles.color = Color.red;
        if (hitbox.hitboxType == Hitbox.EHitboxType.BOX)
        {
            // Show box hitbox range of x, z axis of DNF transform
            Handles.DrawWireCube(t_center - new Vector3(0f, hitbox.offset.y, 0f), new Vector3(hitbox.size.x, hitbox.size.z * DNFTransform.convRate, 0f));
        }
        else if (hitbox.hitboxType == Hitbox.EHitboxType.CIRCLE)
        {
            // Show circle hitbox range of x, z axis of DNF transform
            float t_angle = Mathf.Acos(DNFTransform.convRate) * Mathf.Rad2Deg;
            Handles.DrawWireArc(
                t_center,
                Quaternion.AngleAxis(t_angle, hitbox.transform.right) * hitbox.transform.forward,
                hitbox.transform.right,
                360,
                hitbox.size.x * 0.5f);
        }

        // Show box hitbox range of x, y axis of DNF transform
        Handles.color = Color.green;
        Handles.DrawWireCube(
            t_center - new Vector3(0f, hitbox.offset.z * DNFTransform.convRate, 0f) + new Vector3(0f, hitbox.size.y * 0.5f, 0f),
            new Vector3(hitbox.size.x, hitbox.size.y, 0f));
    }
}
