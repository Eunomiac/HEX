using UnityEngine;

public class Button : MonoBehaviour
{
    public INPUT.Button buttonName;
    public INPUT.Button ButtonName { get { return buttonName; } }

    private float LastTapTime { get; set; }
    private int TapCount { get; set; } = 0;
    private bool IsHolding  { get; set; }
    private Vector3? DirAtTap { get; set; }
    private float PauseTimer { get; set; } = 0f;

    void Update ()
    {
        if ( PauseTimer > 0 )
        {
            PauseTimer = Mathf.Max(0f, PauseTimer - Time.deltaTime);
            return;
        }
        if ( Input.GetButtonDown(ButtonName.ToString()) )  // This Is a **TAP**;
        {
            LastTapTime = Time.time;
            TapCount++;
            DirAtTap = INPUT.I.DirLS;
            if ( TapCount == 1 )
                PLAYER.I.FirstTap(this);           // If this is the FIRST tap, alert PLAYER immediately for instant graphic response.
        }
        else if ( Input.GetButton(ButtonName.ToString()) && LastTapTime > 0f && Time.time - LastTapTime > PREFS.I.Leeway && !IsHolding )
        {
            PLAYER.I.StartHold(this, TapCount, (Vector3) DirAtTap);    // ELSE IF button held for longer than leeway, this is a **HOLD**.
            IsHolding = true;
            Clear();
        }
        else if ( IsHolding && Input.GetButtonUp(ButtonName.ToString()) )
        {
            PLAYER.I.EndHold(this);
            IsHolding = false;
            Clear();
        }
        else if ( TapCount > 0 && !Input.GetButton(ButtonName.ToString()) && (Time.time - LastTapTime > PREFS.I.Leeway || TapCount >= PREFS.I.MaxTaps) )
        {
            PLAYER.I.MultiTap(this, TapCount, (Vector3) DirAtTap);
            Clear();
        }
    }

    public void Clear (bool isPausing)
    {
        if ( isPausing )
            PauseTimer = PREFS.I.Leeway * 2;
        Clear();
    }

    public void Clear ()
    {
        LastTapTime = 0;
        TapCount = 0;
        DirAtTap = null;
    }
}
