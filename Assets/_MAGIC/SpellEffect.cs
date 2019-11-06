using UnityEngine;

public class SpellEffect : MonoBehaviour
{
    public MAGIC.Element spellType;
    public MAGIC.Element SpellType { get; }
    public GameObject spriteHolder;
    public GameObject SpriteHolder { get; }

    private SpellEffectAnimator animator;
    private SpellEffectAnimator Animator 
    {
        get {
            return animator = animator ?? GetComponentInChildren<SpellEffectAnimator>();
        }
    }
    private Zone TargetZone { get; set; }
    private bool IsZoneAttacking { get; set; }

    void Update ()
    {
        if ( TargetZone && !IsZoneAttacking && TargetZone.ActiveThreat )
            IsZoneAttacking = true;
        else if ( TargetZone && IsZoneAttacking && !TargetZone.ActiveThreat )
            CancelSpell();
    }

    public bool IsZoneValidTarget (Zone zone, int numTaps, bool isHold)
    {
        return zone.ActiveThreat
            && zone.NumThreats == numTaps
            && zone.Type == SpellType
            && zone.IsHold == isHold;
    }

    public void CastAtZone (Zone zone, int numTaps, bool isHold)
    {
        if ( IsZoneValidTarget(zone, numTaps, isHold) ) {
            TargetZone = zone;
            Animator.SetCastingSpeed(numTaps);
            transform.rotation = zone.transform.rotation;
            SpriteHolder.transform.rotation = Quaternion.identity;
        } else {
            MAGIC.I.Fail(MAGIC.FailureCondition.NoTarget);
            if ( zone.ActiveThreat ) {
                Debug.Log($"   ... failed at {zone.name}: {numTaps}:{zone.NumThreats}, {SpellType}:{zone.Type}, {isHold}:{zone.IsHold}");
            } else {
                Debug.Log($"   ... failed at {zone.name}: Empty Zone");
            }
            CancelSpell();
        }
    }

    public void FinishSpell ()
    {
        TargetZone.Neutralize();
        CancelSpell();
    }

    void CancelSpell ()
    {
        PLAYER.I.FreeCastSlot(GetComponentInParent<CastSlot>());
        Destroy(gameObject);
    }
}
