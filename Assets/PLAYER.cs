using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    public SpellEffect[] SpellList = new SpellEffect[4];

    private static PLAYER instance;
    public static PLAYER Inst
    {
        get {
            instance = instance ?? FindObjectOfType<PLAYER>();
            return instance;
        }
    }

    private int health;
    private Dictionary<INPUT.Button, SpellEffect> spells = new Dictionary<INPUT.Button, SpellEffect>();
    private SpellEffect lastSpellCast;
    private List<CastSlot> idleCastSlots, busyCastSlots = new List<CastSlot>();
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
        health = PREFS.Inst.PlayerHealth;
        for ( int i = 0; i < SpellList.Length; i++ )
            spells.Add((INPUT.Button) i, SpellList[i]);
        idleCastSlots = GetComponentsInChildren<CastSlot>().ToList();
    }

    void Update ()
    {
        if ( !ActiveCastSlot && idleCastSlots.Count > 0 )
            ActiveCastSlot = idleCastSlots[0];
    }

    public void FirstTap (Button button)
    {
        if ( !ActiveCastSlot )
        {
            MAGIC.Inst.Fail(MAGIC.FailureCondition.NoCastSlot);
            button.Clear(true);
        }
        else
        {
            Debug.Log("First Tap: " + button.ToString());
            busyCastSlots.Add(ActiveCastSlot);
            idleCastSlots.Remove(ActiveCastSlot);
            ActiveCastSlot = null;
        }
    }

    public void FreeCastSlot (CastSlot castSlot)
    {
        idleCastSlots.Add(castSlot);
        busyCastSlots.Remove(castSlot);
        if ( castSlot == ActiveCastSlot )
            ActiveCastSlot = null;
    }

    public void MultiTap (Button button, int tapCount, Vector3 dir)
    {
        Debug.Log("Multi-Tap: " + button.ToString() + " (x" + tapCount + ") => " + ARENA.Inst.GetTargetZone(dir).name);
        lastSpellCast = Instantiate(spells[button.buttonName], busyCastSlots.Last().transform, false);
        lastSpellCast.castAtZone(ARENA.Inst.GetTargetZone(dir), tapCount, false);
    }

    public void StartHold (Button button, int tapCount, Vector3 dir)
    {
        Debug.Log("Start Hold: " + button.ToString() + " (x" + tapCount + ")");
        lastSpellCast = Instantiate(spells[button.buttonName], busyCastSlots.Last().transform, false);
        lastSpellCast.castAtZone(ARENA.Inst.GetTargetZone(dir), tapCount, true);
    }

    public void EndHold (Button button)
    {
        Debug.Log("End Hold");
    }

    public void TakeHit (int damage)
    {
        health -= damage;
        Debug.Log("Hit For " + damage + "! Health = " + health);
    }
}
