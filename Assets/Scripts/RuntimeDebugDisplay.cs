using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeDebugDisplay : MonoBehaviour
{
    public GameObject runtimeHierarchy;
    public GameObject runtimeInspector;
    public GameObject runtimeDebug;
    public GameObject runtimeRandomAbility;
    public GameObject runtimeTouchMovement;

    public void ToggleDebugDisplay()
    {
        runtimeHierarchy.SetActive(!runtimeHierarchy.activeSelf);
        runtimeInspector.SetActive(!runtimeInspector.activeSelf);
        runtimeDebug.SetActive(!runtimeDebug.activeSelf);
        runtimeRandomAbility.SetActive(!runtimeRandomAbility.activeSelf);
        runtimeTouchMovement.SetActive(!runtimeTouchMovement.activeSelf);
    }
}
