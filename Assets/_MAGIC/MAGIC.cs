using UnityEngine;

public class MAGIC : MonoBehaviour
{
    private static MAGIC instance;
    public static MAGIC I
    {
        get {
            instance = instance ?? FindObjectOfType<MAGIC>();
            return instance;
        }
    }

    public enum Element { Earth, Air, Fire, Water }; // { Earth, Air, Fire, Water, Light, Dark, Mind, Arcane };
    public enum Reaction { Resistant, Vulnerable, Normal, Immune };
    public enum FailureCondition { NoCastSlot, NoTarget };

    public Reaction GetReaction (Element attacker, Element defender)
    {
        int difference = (int) attacker - (int) defender + (int) attacker < (int) defender ? 4 : 0;
        return (Reaction) difference;
    }

    public void Fail (FailureCondition failCon)
    {
        switch ( failCon )
        {
            case FailureCondition.NoCastSlot:
                Debug.Log("FAIL: NO AVAILABLE CASTING SLOT.");
                break;
            case FailureCondition.NoTarget:
                Debug.Log("FAIL: INVALID TARGET FOR SPELL.");
                break;
            default:
                Debug.Log("FAIL: UNKNOWN REASON");
                break;
        }
        PREFS.I.cameraShaker.ShakeCamera(2f, 0.018f);
    }
}
