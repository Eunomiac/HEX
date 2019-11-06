using UnityEngine;

public class THREAT : MonoBehaviour
{
    public MAGIC.Element type;
    public MAGIC.Element Type { get { return type; } set { type = value; } }

    public int Damage { get; set; } = 5;
    public bool IsHoldingButton { get; set; } = false;

    private ThreatAnimator[] ActiveThreats { get { return GetComponentsInChildren<ThreatAnimator>(); } }

    public void Activate () {
        gameObject.SetActive(true);
        foreach ( ThreatAnimator threat in GetComponentsInChildren<ThreatAnimator>(true) ) {
            threat.gameObject.SetActive(true);
        }
    }
    
    public void StrikePlayer (ThreatAnimator anim)
    {
        PLAYER.I.TakeHit(Damage);
        DisableThreatUnit(anim);
    }

    private void DisableThreatUnit (ThreatAnimator anim)
    {
        anim.gameObject.SetActive(false);
        if ( ActiveThreats.Length == 0 ) {
            GetComponentInParent<Zone>().Reset();
        }
    }
}
