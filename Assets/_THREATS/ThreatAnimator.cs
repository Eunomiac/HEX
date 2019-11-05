using UnityEngine;

public class ThreatAnimator : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void AnimEvent_StrikePlayer ()
    {
        GetComponentInParent<Threat>().StrikePlayer(this);
    }
}
