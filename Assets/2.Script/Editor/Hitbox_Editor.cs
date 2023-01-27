using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Hitbox))]
public class Hitbox_Editor : Editor
{
    #region Variables

    private bool isShowHandle = false;
    private bool isChangeOffset = true;
    private Hitbox hitbox = null;

    #endregion Variables

    public override void OnInspectorGUI()
    {
        hitbox = target as Hitbox;
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        if (!hitbox || hitbox.hitboxType == Hitbox.EHitboxType.NONE) return;

        Event t_curEvent = Event.current;
        if (!Application.isPlaying && t_curEvent.type == EventType.KeyDown)
        {
            if (t_curEvent.keyCode == KeyCode.F1) isShowHandle = !isShowHandle;
            if (t_curEvent.keyCode == KeyCode.F2) isChangeOffset = !isChangeOffset;
        }

        Vector3 t_charGroundPos, t_charPos, t_hitboxGroundPos, t_hitboxPos;

        // Init position value
        if (hitbox.charTransform == null)
        {
            t_charGroundPos = t_charPos = new Vector3(hitbox.transform.position.x, hitbox.transform.position.y, 0f);
            t_hitboxGroundPos = t_charGroundPos + new Vector3(hitbox.offset.x, hitbox.offset.z * DNFTransform.convRate, 0f);
            t_hitboxPos = t_charPos + new Vector3(hitbox.offset.x, hitbox.offset.y + hitbox.offset.z * DNFTransform.convRate, 0f);
        }
        else
        {
            t_charGroundPos = new Vector3(hitbox.charTransform.X, hitbox.charTransform.Z * DNFTransform.convRate, 0f);
            t_charPos = t_charGroundPos + new Vector3(0f, hitbox.charTransform.Y, 0f);
            t_hitboxGroundPos = t_charGroundPos + new Vector3(hitbox.offset.x, hitbox.offset.z * DNFTransform.convRate, 0f);
            t_hitboxPos = t_charPos + new Vector3(hitbox.offset.x, hitbox.offset.y + hitbox.offset.z * DNFTransform.convRate, 0f);
        }

        #region Hitbox

        Handles.color = Color.red;
        if (hitbox.hitboxType == Hitbox.EHitboxType.BOX)
        {
            // Show box hitbox range of x, z axis of DNF transform
            Handles.DrawWireCube(t_hitboxGroundPos, new Vector3(hitbox.size.x, hitbox.size.z * DNFTransform.convRate, 0f));
        }
        else if (hitbox.hitboxType == Hitbox.EHitboxType.CIRCLE)
        {
            // Show circle hitbox range of x, z axis of DNF transform
            float t_angle = Mathf.Acos(DNFTransform.convRate) * Mathf.Rad2Deg;
            Handles.DrawWireArc(
                t_hitboxGroundPos,
                Quaternion.AngleAxis(t_angle, hitbox.transform.right) * hitbox.transform.forward,
                hitbox.transform.right,
                360,
                hitbox.size.x * 0.5f);
        }

        // Show box hitbox range of x, y axis of DNF transform
        Handles.color = Color.green;
        Handles.DrawWireCube(t_hitboxPos + new Vector3(0f, hitbox.size.y * 0.5f, 0f), new Vector3(hitbox.size.x, hitbox.size.y, 0f));

        #endregion Hitbox

        if (!isShowHandle) return;

        #region Handler

        if (isChangeOffset)
        {
            Vector3 t_yOffset = t_charPos - t_charGroundPos;

            // Set offset along X axis of DNF transform
            Handles.color = Color.red;
            t_hitboxPos = Handles.Slider(t_hitboxPos, hitbox.transform.right);
            t_hitboxGroundPos.x = t_hitboxPos.x;
            hitbox.offset.x = t_hitboxGroundPos.x - t_charGroundPos.x;

            // Set offset along Z axis of DNF transform
            Handles.color = Color.blue;
            t_hitboxPos = Handles.Slider(t_hitboxPos, hitbox.transform.up * (-0.1f));
            t_hitboxGroundPos.y = t_hitboxPos.y - t_yOffset.y - hitbox.offset.y;
            hitbox.offset.z = (t_hitboxGroundPos.y - t_charGroundPos.y) * DNFTransform.invConvRate;

            // Set offset along Y axis of DNF transform
            Handles.color = Color.green;
            t_hitboxPos = Handles.Slider(t_hitboxPos, hitbox.transform.up);
            hitbox.offset.y = t_hitboxPos.y - t_yOffset.y - t_hitboxGroundPos.y;
        }
        else
        {
            // Set size along x axis of DNF transform
            Handles.color = Color.red;
            hitbox.size.x = Handles.ScaleSlider(hitbox.size.x, t_hitboxGroundPos, hitbox.transform.right, Quaternion.identity, 1f, 0.1f);

            if (hitbox.hitboxType == Hitbox.EHitboxType.BOX)
            {
                // Set size along z axis of DNF transform
                Handles.color = Color.blue;
                hitbox.size.z = Handles.ScaleSlider(hitbox.size.z, t_hitboxGroundPos, hitbox.transform.up * (-1.0f), Quaternion.identity, 1f, 0.1f);
            }

            // Set size along y axis of DNF transform
            Handles.color = Color.green;
            hitbox.size.y = Handles.ScaleSlider(hitbox.size.y, t_hitboxGroundPos, hitbox.transform.up, Quaternion.identity, 1f, 0.1f);
        }

        #endregion Handler
    }
}
