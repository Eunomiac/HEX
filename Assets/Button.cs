using UnityEngine;

public class Button : MonoBehaviour
{
    public INPUT.Button buttonName;

    private float lastTapTime;
    private int tapCount = 0;
    private bool isHolding;
    private Vector3? dirAtTap;
    private float pauseTimer = 0f;

    void Update ()
    {
        if ( pauseTimer > 0 )
        {
            pauseTimer = Mathf.Max(0f, pauseTimer - Time.deltaTime);
            return;
        }
        if ( Input.GetButtonDown(buttonName.ToString()) )  // This Is a **TAP**;
        {
            lastTapTime = Time.time;
            tapCount++;
            dirAtTap = INPUT.Inst.DirLS;
            if ( tapCount == 1 )
                PLAYER.Inst.FirstTap(this);           // If this is the FIRST tap, alert PLAYER immediately for instant graphic response.
        }
        else if ( Input.GetButton(buttonName.ToString()) && lastTapTime > 0f && Time.time - lastTapTime > PREFS.Inst.Leeway && !isHolding )
        {
            PLAYER.Inst.StartHold(this, tapCount, (Vector3) dirAtTap);    // ELSE IF button held for longer than leeway, this is a **HOLD**.
            isHolding = true;
            Clear();
        }
        else if ( isHolding && Input.GetButtonUp(buttonName.ToString()) )
        {
            PLAYER.Inst.EndHold(this);
            isHolding = false;
            Clear();
        }
        else if ( tapCount > 0 && !Input.GetButton(buttonName.ToString()) && (Time.time - lastTapTime > PREFS.Inst.Leeway || tapCount >= PREFS.Inst.MaxTaps) )
        {
            PLAYER.Inst.MultiTap(this, tapCount, (Vector3) dirAtTap);
            Clear();
        }
    }

    public void Clear (bool isPausing)
    {
        if ( isPausing )
            pauseTimer = PREFS.Inst.Leeway * 2;
        Clear();
    }

    public void Clear ()
    {
        lastTapTime = 0;
        tapCount = 0;
        dirAtTap = null;
    }
}
