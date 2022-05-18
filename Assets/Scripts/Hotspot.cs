using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotspot : MonoBehaviour
{
    public GameObject areaIndicator;
    public float areaIndicatorRadius = 4f;
    public bool isDisabled = true;
    private MeshRenderer mr;
    public List<Clue> clues = new List<Clue>();
    public List<Clue> foundClues = new List<Clue>();

    public HotspotManager hsManager;

    void Awake()
    {
        mr = this.GetComponent<MeshRenderer>();
        mr.enabled = false;
        GenerateAreaIndicator(0);
    }

    public void SetHotspotManager(HotspotManager _hsManager)
    {
        hsManager = _hsManager;
        Debug.Log("Setting hotspot manager" + hsManager);
    }

    public Clue GetClue()
    {
        if (clues.Count > 0)
        {
            Clue c = clues[0];
            foundClues.Add(c);
            clues.Remove(c);
            return c;
        }
        return null;
    }

    public void unHide()
    {
        areaIndicator.SetActive(true);
    }

    public void GenerateAreaIndicator(int modifier)
    {
        if (areaIndicatorRadius + modifier > 0)
            areaIndicatorRadius += modifier;

        Vector3 indicator = new Vector3(
            areaIndicator.transform.localPosition.x,
            areaIndicator.transform.localPosition.y,
            areaIndicator.transform.localPosition.z);

        areaIndicator.transform.localPosition = new Vector3(
            indicator.x + Random.Range(-areaIndicatorRadius, areaIndicatorRadius),
            indicator.y,
            indicator.z + Random.Range(-areaIndicatorRadius, areaIndicatorRadius));
    }

    public void RemoveClue(Clue clue)
    {
        if (clues.Contains(clue))
            clues.Remove(clue);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player has passed through hotspot");
            mr.enabled = true;
            other.GetComponentInParent<AbilityManager>().AddRandomAbility();

            if (hsManager)
            {
                if (!isDisabled)
                {
                    hsManager.AddKey();
                    // Enable dialogue object.
                    // TODO: Debug.Log("Removing hotspot from clue pool");
                    // TODO: hsManager.PopHotSpot(this);
                    isDisabled = true;
                }
            }
            else
            {
                Debug.LogWarning("Hotspot does not have reference to a HotspotManager");
            }
        }
    }
}
