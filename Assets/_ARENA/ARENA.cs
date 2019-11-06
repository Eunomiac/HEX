using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ARENA : MonoBehaviour
{
    private static ARENA instance;
    public static ARENA I
    {
        get {            
            return instance = instance ?? FindObjectOfType<ARENA>();;
        }
    }

    private static float speedMult = 0f;
    public static float Speed
    {
        get {            
            return speedMult = speedMult == 0f ? 1f : speedMult;
        }
        set {
            speedMult = (float) value;
        }
    }

    private static float nextAttackTime = 0f;
    public static float NextAttackTime
    {
        get {   
            if (nextAttackTime <= Time.time) {
                float nextAtkTime = nextAttackTime;         
                float randomShift = Random.Range(0.75f * PREFS.I.TimeBetweenAttacks, 1.25f * PREFS.I.TimeBetweenAttacks);
                nextAttackTime = Time.time + ARENA.Speed * randomShift;
                return nextAtkTime == 0f ? nextAttackTime : nextAtkTime;
            } else {
                return nextAttackTime;
            }
        }
    }

    private List<Wedge> dormantWedges;
    private List<Wedge> DormantWedges 
    { 
        get { 
            return dormantWedges = dormantWedges ?? GetComponentsInChildren<Wedge>().ToList(); 
        }
    }
    // private List<Wedge> activeWedges = new List<Wedge>();
    private List<Wedge> activeWedges;
    private List<Wedge> ActiveWedges
    {
        get {            
            return activeWedges = activeWedges ?? new List<Wedge>();
        }
    }

    void Update ()
    {
        if ( Time.time >= ARENA.NextAttackTime )
            ActivateWedge(DormantWedges[Random.Range(0, DormantWedges.Count)]);
    }

    public Zone GetTargetZone (Vector3 dir)
    {
        foreach ( Zone zone in GetComponentsInChildren<Zone>() )
        {
            if ( zone.isTargeted(dir) )
                return zone;
        }
        Debug.LogError($"ERROR: No Wedge Found. Dir = {dir.ToString()}");
        return null;
    }

    void ActivateWedge (Wedge wedge)
    {
        ActiveWedges.Add(wedge);
        DormantWedges.Remove(wedge);
        wedge.Attack();
    }

    public void DeactivateWedge (Wedge wedge)
    {
        activeWedges.Remove(wedge);
        dormantWedges.Add(wedge);
    }
}
