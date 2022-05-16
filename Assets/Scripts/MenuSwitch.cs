using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour
{
    public GameObject MainObject;
    public GameObject OptionsObject;
    public GameObject AboutObject;

    public void Options()
    {
        MainObject.SetActive(false);
        AboutObject.SetActive(false);
        OptionsObject.SetActive(true);
    }
    public void Main()
    {
        MainObject.SetActive(true);
        AboutObject.SetActive(false);
        OptionsObject.SetActive(false);
    }
    public void About()
    {
        MainObject.SetActive(false);
        AboutObject.SetActive(true);
        OptionsObject.SetActive(false);
    }
}
