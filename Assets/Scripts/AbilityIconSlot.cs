using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIconSlot : MonoBehaviour
{
    public Camera cam;
    public Transform iconHolder;
    public GameObject currentModel;

    public Material t1Mat;
    public Material t2Mat;
    public Material t3Mat;

    public void AddIcon(GameObject model, UpgradeTier tier)
    {
        // Instantiate model inside ability holder
        currentModel = GameObject.Instantiate(model, iconHolder);
        // Set layer to AbilityIcon
        foreach (Transform t in currentModel.GetComponentsInChildren<Transform>(true))
        {
            t.gameObject.layer = LayerMask.NameToLayer("AbilityIcon");
        }
        // Change ability background to rarity
        GameObject basePlate;
        foreach (Transform child in currentModel.transform)
        {
            if (child.tag == "AbilityBase")
            {
                basePlate = child.gameObject;
                Material mat = t1Mat;
                switch (tier)
                {
                    case UpgradeTier.Tier2:
                        mat = t2Mat;
                        break;
                    case UpgradeTier.Tier3:
                        mat = t3Mat;
                        break;
                }
                basePlate.GetComponent<Renderer>().material = mat;
                break;
            }
        }

    }

    public void ClearIcon()
    {
        currentModel.gameObject.Destroy();
        currentModel = null;
    }
}
