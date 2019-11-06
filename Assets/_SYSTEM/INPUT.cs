using UnityEngine;

public class INPUT : MonoBehaviour
{
    private static INPUT instance;
    public static INPUT I
    {
        get {            
            return instance = instance ?? FindObjectOfType<INPUT>();;
        }
    }

    public enum Button { A, B, X, Y, LB, RB, Back, Start, LT = 9, RT };

    public Vector3 DirLS
    {
        get {
            return new Vector3(Input.GetAxis("Horizontal"), 0, -1 * Input.GetAxis("Vertical"));
        }
    }

    public Vector3 DirRS
    {
        get {
            return new Vector3(Input.GetAxis("RightStickHoriz"), 0, -1 * Input.GetAxis("RightStickVert"));
        }
    }

}