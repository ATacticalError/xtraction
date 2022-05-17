using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitch : MonoBehaviour
{
    public GameObject MainObject;
    public GameObject OptionsObject;
    public GameObject AboutObject;
    public GameObject CreditsObject;

    public void Options()
    {
        MainObject.SetActive(false);
        AboutObject.SetActive(false);
        OptionsObject.SetActive(true);
        CreditsObject.SetActive(false);
    }
    public void Main()
    {
        MainObject.SetActive(true);
        AboutObject.SetActive(false);
        OptionsObject.SetActive(false);
        CreditsObject.SetActive(false);
    }
    public void About()
    {
        MainObject.SetActive(false);
        AboutObject.SetActive(true);
        OptionsObject.SetActive(false);
        CreditsObject.SetActive(false);
    }
    public void Credits()
    {
        MainObject.SetActive(false);
        AboutObject.SetActive(false);
        OptionsObject.SetActive(false);
        CreditsObject.SetActive(true);
    }
}
