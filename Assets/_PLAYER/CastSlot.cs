using UnityEngine;

public class CastSlot : MonoBehaviour
{
    private SpriteRenderer reticule;
    private SpriteRenderer Reticule
    {
        get {
            reticule = reticule ?? GetComponentInChildren<SpriteRenderer>();
            return reticule;
        }
    }

    private bool isAiming = false;
    public bool IsAiming
    {
        get { return isAiming; }
        set {
            isAiming = value;
            Vector4 color = Reticule.color;
            color.w = isAiming ? 1f : 0.25f;
            Reticule.color = color;
        }
    }

    void Update ()
    {
        if ( IsAiming && INPUT.I.DirLS != Vector3.zero )
            transform.forward = INPUT.I.DirLS;
    }
}
