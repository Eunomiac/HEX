using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ARENA : MonoBehaviour
{
    private static ARENA instance;
    public static ARENA Inst
    {
        get {
            instance = instance ?? FindObjectOfType<ARENA>();
            return instance;
        }
    }

    private float lastAttackTime;
    private List<Wedge> dormantWedges;
    private List<Wedge> activeWedges = new List<Wedge>();

    void Start ()
    {
        dormantWedges = GetComponentsInChildren<Wedge>().ToList();
    }

    void Update ()
    {
        if ( Time.time - lastAttackTime >= PREFS.Inst.TimeBetweenAttacks )
            ActivateWedge(dormantWedges[Random.Range(0, dormantWedges.Count)]);
    }

    public Zone GetTargetZone (Vector3 dir)
    {
        foreach ( Zone zone in GetComponentsInChildren<Zone>() )
        {
            if ( zone.isTargeted(dir) )
                return zone;
        }
        Debug.LogError("ERROR: No Wedge Found. Dir = " + dir.ToString());
        return null;
    }

    void ActivateWedge (Wedge wedge)
    {
        activeWedges.Add(wedge);
        dormantWedges.Remove(wedge);
        wedge.Attack();
        lastAttackTime = Time.time;
    }

    public void DeactivateWedge (Wedge wedge)
    {
        activeWedges.Remove(wedge);
        dormantWedges.Add(wedge);
    }
}
