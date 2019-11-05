using UnityEngine;

public class Threat : MonoBehaviour
{
    public MAGIC.Element type;
    private int damage = 5;
    private bool isHold = false;

    public MAGIC.Element Type { get { return type; } set { type = value; } }
    public int Damage { get { return damage; } set { damage = value; } }
    public bool IsHold { get { return isHold; } set { isHold = value; } }

    private ThreatAnimator[] ActiveThreats { get { return GetComponentsInChildren<ThreatAnimator>(); } }

    public void StrikePlayer (ThreatAnimator anim)
    {
        PLAYER.Inst.TakeHit(damage);
        DisableThreatUnit(anim);
    }

    void DisableThreatUnit (ThreatAnimator anim)
    {
        anim.gameObject.SetActive(false);
        if ( ActiveThreats.Length == 0 )
            GetComponentInParent<Zone>().Reset();
    }


}
