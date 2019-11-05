using UnityEngine;

public class CastSlot : MonoBehaviour
{
    public bool IsAiming
    {
        get { return isAiming; }
        set {
            isAiming = value;
            Vector4 color = reticule.color;
            color.w = isAiming ? 1f : 0.25f;
            reticule.color = color;
        }
    }

    private SpriteRenderer reticule;
    private bool isAiming = false;

    void Start ()
    {
        reticule = GetComponentInChildren<SpriteRenderer>();
    }

    void Update ()
    {
        if ( IsAiming && INPUT.Inst.DirLS != Vector3.zero )
            transform.forward = INPUT.Inst.DirLS;
    }
}
