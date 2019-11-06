using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    public SpellEffect[] SpellList = new SpellEffect[4];

    private static PLAYER instance;
    public static PLAYER I
    {
        get {
            instance = instance ?? FindObjectOfType<PLAYER>();
            return instance;
        }
    }

    private int Health { get; set; }
    private Dictionary<INPUT.Button, SpellEffect> Spells { get; } = new Dictionary<INPUT.Button, SpellEffect>();
    private SpellEffect LastSpellCast { get; set; }
    private List<CastSlot> IdleCastSlots { get; set; }
    private List<CastSlot> BusyCastSlots { get; } = new List<CastSlot>();
    private CastSlot activeCastSlot;

    private CastSlot ActiveCastSlot
    {
        get { return activeCastSlot; }
        set {
            if ( activeCastSlot )
                activeCastSlot.IsAiming = false;
            activeCastSlot = value;
            if ( activeCastSlot )
                activeCastSlot.IsAiming = true;
        }
    }

    void Start ()
    {
        Health = PREFS.I.PlayerHealth;
        for ( int i = 0; i < SpellList.Length; i++ )
            Spells.Add((INPUT.Button) i, SpellList[i]);
        IdleCastSlots = GetComponentsInChildren<CastSlot>().ToList();
    }

    void Update ()
    {
        if ( !ActiveCastSlot && IdleCastSlots.Count > 0 )
            ActiveCastSlot = IdleCastSlots[0];
    }

    public void FirstTap (Button button)
    {
        if ( !ActiveCastSlot )
        {
            MAGIC.I.Fail(MAGIC.FailureCondition.NoCastSlot);
            button.Clear(true);
        }
        else
        {
            Debug.Log($"First Tap: {button.ToString()}");
            BusyCastSlots.Add(ActiveCastSlot);
            IdleCastSlots.Remove(ActiveCastSlot);
            ActiveCastSlot = null;
        }
    }

    public void FreeCastSlot (CastSlot castSlot)
    {
        IdleCastSlots.Add(castSlot);
        BusyCastSlots.Remove(castSlot);
        if ( castSlot == ActiveCastSlot )
            ActiveCastSlot = null;
    }

    public void MultiTap (Button button, int tapCount, Vector3 dir)
    {
        Debug.Log($"Multi-Tap: {button.ToString()} (x{tapCount}) => {ARENA.I.GetTargetZone(dir).name}");
        LastSpellCast = Instantiate(Spells[button.ButtonName], BusyCastSlots.Last().transform, false);
        LastSpellCast.CastAtZone(ARENA.I.GetTargetZone(dir), tapCount, false);
    }

    public void StartHold (Button button, int tapCount, Vector3 dir)
    {
        Debug.Log($"Start Hold: {button.ToString()} (x{tapCount})");
        LastSpellCast = Instantiate(Spells[button.ButtonName], BusyCastSlots.Last().transform, false);
        LastSpellCast.CastAtZone(ARENA.I.GetTargetZone(dir), tapCount, true);
    }

    public void EndHold (Button button)
    {
        Debug.Log("End Hold");
    }

    public void TakeHit (int damage)
    {
        Health -= damage;
        Debug.Log($"Hit For {damage}! Health = {Health}");
    }
}
