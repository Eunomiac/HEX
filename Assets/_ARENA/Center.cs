using UnityEngine;

public class Center : Zone
{

    public override bool isTargeted (Vector3 dir)
    {
        return dir == Vector3.zero;
    }

    public override void Attack ()
    {
        Debug.Log("Counter Threat Now!");
    }

    public override void Reset ()
    {
    }
}
