using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DNFTransform))]
public class Hitbox : MonoBehaviour
{
    public enum EHitboxType { NONE = -1, BOX, CIRCLE, }
        
    #region Variables

    [SerializeField] private DNFTransform charTransform = null;

    public EHitboxType hitboxType = EHitboxType.NONE;

    public Vector3 size = Vector3.zero;
    public Vector3 offset = Vector3.zero;
    private Vector3 minHitbox = Vector3.zero;
    private Vector3 maxHitbox = Vector3.zero;

    #endregion Variables

    #region Unity Event

    private void Awake()
    {
        if(charTransform == null) charTransform = GetComponent<DNFTransform>();
    }

    private void Update()
    {
        var t_position = charTransform.Position + offset;

        minHitbox.x = t_position.x - size.x * 0.5f;
        maxHitbox.x = t_position.x + size.x * 0.5f;
        minHitbox.z = t_position.z - size.z * 0.5f;
        maxHitbox.z = t_position.z + size.z * 0.5f;

        if (!charTransform.HasYObj) return;

        minHitbox.y = t_position.y;
        maxHitbox.y = t_position.y + size.y;
    }

    #endregion Unity Event

    #region Methods

    public bool CalculateOnHit(Hitbox p_target)
    {
        if (hitboxType == p_target.hitboxType)
            return hitboxType == EHitboxType.BOX ? CheckBB(this, p_target) : CheckCC(this, p_target);

        return hitboxType == EHitboxType.BOX ? CheckBC(this, p_target) : CheckBC(p_target, this);
    }

    #endregion Methods

    #region Helper Methods

    private bool CheckBB(Hitbox p_boxA, Hitbox p_boxB)
    {
        Vector3 t_aMin = p_boxA.minHitbox; 
        Vector3 t_aMax = p_boxA.maxHitbox;
        Vector3 t_bMin = p_boxB.minHitbox; 
        Vector3 t_bMax = p_boxB.maxHitbox;

        if (t_aMin.x < t_bMin.x || t_aMax.x > t_bMax.x) return false;
        if (t_aMin.z < t_bMin.z || t_aMax.z > t_bMax.z) return false;

        if (!p_boxA.charTransform.HasYObj || !p_boxB.charTransform.HasYObj) return true;

        if (t_aMin.y < t_bMin.y || t_aMax.y > t_bMax.y) return false;

        return true;
    }

    private bool CheckInsideCircle(Vector3 p_center, float p_radius, float p_x, float p_z)
    {
        return (p_x - p_center.x) * (p_x - p_center.x) + (p_z - p_center.z) * (p_z - p_center.z) < p_radius * p_radius;
    }

    private bool CheckBC(Hitbox p_box, Hitbox p_circle)
    {
        Vector3 t_boxMin = p_box.minHitbox; 
        Vector3 t_boxMax = p_box.maxHitbox;
        Vector3 t_circleMin = p_circle.minHitbox; 
        Vector3 t_circleMax = p_circle.maxHitbox;
        Vector3 t_boxCenter = (t_boxMin + t_boxMax) * 0.5f;
        Vector3 t_circleCenter = (t_circleMin + t_circleMax) * 0.5f;

        int t_rectNum = t_circleCenter.x < t_boxMin.x ? 0 : (t_circleCenter.x > t_boxMax.x ? 2 : 1)
            + 3 * (t_circleCenter.z < t_boxMin.z ? 0 : (t_circleCenter.z > t_boxMax.z ? 2 : 1));

        switch (t_rectNum)
        {
            case 1:
            case 7:
                float t_distHeightRadius = (t_boxMax.z - t_boxMin.z) * 0.5f + p_circle.size.x;
                float t_distVerticalCenter = t_boxCenter.z > t_circleCenter.z ? t_boxCenter.z - t_circleCenter.z : t_circleCenter.z - t_boxCenter.z;
                if (t_distHeightRadius < t_distVerticalCenter) return false;
                break;

            case 3:
            case 5:
                float t_distWidthRadius = (t_boxMax.x - t_boxMin.x) * 0.5f + p_circle.size.z;
                float t_distHorizontalCenter = t_boxCenter.x > t_circleCenter.x ? t_boxCenter.x - t_circleCenter.z : t_circleCenter.z - t_boxCenter.z;
                if (t_distWidthRadius < t_distHorizontalCenter) return false;
                break;

            default:
                float t_cornerX = (t_rectNum == 0 || t_rectNum == 6) ? t_boxMin.x : t_boxMax.x;
                float t_cornerZ = (t_rectNum == 0 || t_rectNum == 2) ? t_boxMin.z : t_boxMax.z;
                if (!CheckInsideCircle(t_circleCenter, p_circle.size.x * 0.5f, t_cornerX, t_cornerZ)) return false;
                break;
        }

        if (!p_box.charTransform.HasYObj || !p_circle.charTransform.HasYObj) return true;

        if (t_boxMax.y < t_circleMin.y || t_boxMin.y > t_circleMax.y) return false;

        return true;
    }

    private bool CheckCC(Hitbox p_circleA, Hitbox p_circleB)
    {
        Vector3 t_aCenter = (p_circleA.minHitbox + p_circleA.maxHitbox) * 0.5f;
        Vector3 t_bCenter = (p_circleB.minHitbox + p_circleB.maxHitbox) * 0.5f;
        Vector3 t_dist = t_aCenter - t_bCenter;
        float t_sumRadius = (p_circleA.size.x + p_circleB.size.x) * 0.5f;
        t_dist.y = 0;

        if (t_sumRadius * t_sumRadius < t_dist.sqrMagnitude) return false;

        if (!p_circleA.charTransform.HasYObj || !p_circleB.charTransform.HasYObj) return true;

        if (p_circleA.maxHitbox.y < p_circleB.minHitbox.y || p_circleA.minHitbox.y > p_circleB.maxHitbox.y) return false;

        return true;
    }

    #endregion Helper Methods
}
