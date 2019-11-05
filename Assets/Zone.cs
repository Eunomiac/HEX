using UnityEngine;

public abstract class Zone : MonoBehaviour
{
    public GameObject dormant;

    public Threat ActiveThreat { get; protected set; }
    protected Threat[] threatList;

    public abstract bool isTargeted (Vector3 dir);

    public int NumThreats { get { return ActiveThreat.GetComponentsInChildren<ThreatAnimator>().Length; } }
    public MAGIC.Element Type { get { return ActiveThreat.Type; } }
    public bool IsHold { get { return ActiveThreat.IsHold; } }

    protected virtual void Start ()
    {
        threatList = GetComponentsInChildren<Threat>(true);
    }

    public virtual void Attack ()
    {
        Debug.Log("Attacking From " + name);
        dormant.gameObject.SetActive(false);
        ActiveThreat = threatList[Random.Range(0, threatList.Length)];
        ActiveThreat.gameObject.SetActive(true);
    }

    public virtual void Neutralize ()
    {
        Debug.Log("Successfully Neutralized " + name);
        Reset();
    }

    public virtual void Reset ()
    {
        if ( ActiveThreat )
            ActiveThreat.gameObject.SetActive(false);
        ActiveThreat = null;
        dormant.gameObject.SetActive(true);
    }


}
