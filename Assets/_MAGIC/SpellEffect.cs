using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public MAGIC.Element spellType;
    public GameObject spriteHolder;

    private SpellEffectAnimator animator;
    private Zone targetZone;
    private bool isZoneAttacking;

    void Awake ()
    {
        animator = GetComponentInChildren<SpellEffectAnimator>();
    }

    void Update ()
    {
        if ( targetZone && !isZoneAttacking && targetZone.ActiveThreat )
            isZoneAttacking = true;
        else if ( targetZone && isZoneAttacking && !targetZone.ActiveThreat )
            CancelSpell();
    }

    public bool isZoneValidTarget (Zone zone, int numTaps, bool isHold)
    {
        if ( zone.ActiveThreat )
            return zone.NumThreats == numTaps
                && zone.Type == spellType
                && zone.IsHold == isHold;
        else
            return false;
    }

    public void castAtZone (Zone zone, int numTaps, bool isHold)
    {
        if ( isZoneValidTarget(zone, numTaps, isHold) )
        {
            targetZone = zone;
            animator.SetCastingSpeed(numTaps);
            transform.rotation = zone.transform.rotation;
            spriteHolder.transform.rotation = Quaternion.identity;
        }
        else
        {
            MAGIC.Inst.Fail(MAGIC.FailureCondition.NoTarget);
            if ( zone.ActiveThreat )
            {
                Debug.Log("   ... failed at " + zone.name + ": " + numTaps + ":" + zone.NumThreats + ", " + spellType + ":" + zone.Type + ", " + isHold + ":" + zone.IsHold);
            }
            else
            {
                Debug.Log("   ... failed at " + zone.name + ": Empty Zone");
            }
            CancelSpell();
        }
    }

    public void spellFinished ()
    {
        targetZone.Neutralize();
        CancelSpell();
    }

    void CancelSpell ()
    {
        PLAYER.Inst.FreeCastSlot(GetComponentInParent<CastSlot>());
        Destroy(gameObject);
    }
}
