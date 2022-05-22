using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePoint : MonoBehaviour
{
    public HotspotManager hsManager;

    void Awake() {
        hsManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<HotspotManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        Debug.Log("Player has passed through clue");
        hsManager.ClueFound();
        hsManager.PlayClueFoundAudio();
        this.gameObject.SetActive(false);
    }
}
