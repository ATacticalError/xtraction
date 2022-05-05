using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotspot : MonoBehaviour
{
    public GameObject areaIndicator;
    public float areaIndicatorRadius = 2f;
    public bool isDisabled;
    private MeshRenderer mr;

    void Awake()
    {
        mr = this.GetComponent<MeshRenderer>();
        mr.enabled = false;
        float indicatorX = areaIndicator.transform.localPosition.x;
        float indicatorY = areaIndicator.transform.localPosition.y;
        float indicatorZ = areaIndicator.transform.localPosition.z;
        areaIndicator.transform.localPosition = new Vector3(
            indicatorX + Random.Range(-areaIndicatorRadius, areaIndicatorRadius),
            indicatorY,
            indicatorZ + Random.Range(-areaIndicatorRadius, areaIndicatorRadius));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player has passed through hotspot");
            mr.enabled = true;
        }
    }
}
