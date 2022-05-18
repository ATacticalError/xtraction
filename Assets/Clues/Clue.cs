using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Clues/Clue")]
public class Clue : ScriptableObject
{
    public string clueTitle;
    //    public string clueDescription;
    [TextArea(15,20)]
    public string clueContent;
    public Sprite clueImage;

    public GameObject clueUIPrefab;
    private GameObject clueGO;
    private GameObject cluePanel;
    private Text uiTitle;
    private Text uiContent;
    private Image uiImage;

    public void Initialise(GameObject cluePanel, GameObject inventoryUI)
    {
        inventoryUI.SetActive(true);
        clueGO = GameObject.Instantiate(clueUIPrefab, cluePanel.transform);
        clueGO.transform.SetAsFirstSibling();
        uiTitle = FindChildInTag(clueGO, "ClueTitle").GetComponent<Text>();
        uiContent = FindChildInTag(clueGO, "ClueContent").GetComponent<Text>();
        uiImage = FindChildInTag(clueGO, "ClueImage").GetComponent<Image>();

        uiTitle.text = clueTitle;
        if (clueContent != "")
        {
            uiContent.text = clueContent;
        }
        else
        {
            uiContent.gameObject.SetActive(false);
        }
        if (clueImage)
        {
            uiImage.sprite = clueImage;
        }
        else
        {
            uiImage.gameObject.SetActive(false);
        }
    }

    Transform FindChildInTag(GameObject root, string tag)
    {
        foreach (Transform t in root.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag(tag))
                return t;
        }
        return null;
    }
}
