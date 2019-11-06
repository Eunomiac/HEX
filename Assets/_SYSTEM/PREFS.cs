using UnityEngine;

public class PREFS : MonoBehaviour
{
    public float leeway;
    public int maxTaps;
    public float timeBetweenAttacks;
    public int playerHealth;
    public CameraShake cameraShaker;

    private static PREFS instance;
    public static PREFS Inst
    {
        get {
            instance = instance ?? FindObjectOfType<PREFS>();
            return instance;
        }
    }

    public float Leeway { get { return leeway; } }
    public int MaxTaps { get { return maxTaps; } }
    public float TimeBetweenAttacks { get { return timeBetweenAttacks; } }
    public int PlayerHealth { get { return playerHealth; } }

}
