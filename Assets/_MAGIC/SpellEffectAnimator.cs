using UnityEngine;

public class SpellEffectAnimator : MonoBehaviour
{
    public void SetCastingSpeed (int speed)
    {
        GetComponent<Animator>().SetInteger("CastingSpeed", speed);
    }

    public void AnimEvent_CastSpell ()
    {

    }

    public void AnimEvent_SpellFinished ()
    {
        GetComponentInParent<SpellEffect>().spellFinished();
    }
}
