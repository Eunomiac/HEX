using UnityEngine;

public class PREFS : MonoBehaviour
{
    public float leeway; // Duration of window to detect second/third button taps.
    public float Leeway { get { return leeway; } }

    public int maxTaps; // The maximum number of button taps to detect as one input command.
    public int MaxTaps { get { return maxTaps; } }

    public float timeBetweenAttacks; // The average time between wedge activations (actual time is randomized between 0.75-1.25 * this value, and scaled by the ARENA.Speed multiplier).
    public float TimeBetweenAttacks { get { return timeBetweenAttacks; } }

    public int playerHealth; // The player's starting health.
    public int PlayerHealth { get { return playerHealth; } }

    public CameraShake cameraShaker; // Unity reference object for the camera control.

    private static PREFS instance;
    public static PREFS I
    {
        get {
            instance = instance ?? FindObjectOfType<PREFS>();
            return instance;
        }
    }
}