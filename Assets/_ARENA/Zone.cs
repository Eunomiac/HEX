using UnityEngine;

public abstract class Zone : MonoBehaviour
{
    public GameObject dormant;
    public GameObject Dormant { get { return dormant; } }

    public THREAT ActiveThreat { get; protected set; }
    protected THREAT[] threatList;
    protected THREAT[] ThreatList
    {
        get {
            threatList = threatList ?? GetComponentsInChildren<THREAT>(true);
            THREAT[] threatTest = GetComponentsInChildren<THREAT>(true);
            return threatList = threatList ?? GetComponentsInChildren<THREAT>(true);
        }
    }

    public abstract bool isTargeted (Vector3 dir);

    public int NumThreats { get { return ActiveThreat.GetComponentsInChildren<ThreatAnimator>().Length; } }
    public MAGIC.Element Type { get { return ActiveThreat.Type; } }
    public bool IsHold { get { return ActiveThreat.IsHoldingButton; } }

    public virtual void Attack ()
    {
        int threatNum = Random.Range(0, ThreatList.Length);
        Debug.Log($"Attacking From {name} (Threat {threatNum}/{ThreatList.Length})");
        Dormant.gameObject.SetActive(false);
        ActiveThreat = ThreatList[Random.Range(0, ThreatList.Length)];
        ActiveThreat.Activate();
    }

    public virtual void Neutralize ()
    {
        Debug.Log($"Successfully Neutralized {name}");
        Reset();
    }

    public virtual void Reset ()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        if ( ActiveThreat )
            ActiveThreat.gameObject.SetActive(false);
        ActiveThreat = null;
        Dormant.gameObject.SetActive(true);
    }
}
