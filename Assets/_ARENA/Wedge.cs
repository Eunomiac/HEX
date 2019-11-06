using UnityEngine;

public class Wedge : Zone
{
    public override bool isTargeted (Vector3 dir)
    {
        return Vector3.Angle(transform.forward, dir) <= 31f;
    }

    public override void Neutralize ()
    {
        base.Neutralize();
    }

    public override void Reset ()
    {
        ARENA.I.DeactivateWedge(this);
        base.Reset();
    }

}
