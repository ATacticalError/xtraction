using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurShaderModifier : MonoBehaviour
{
    public GameObject BlurObject;
    private Image BlurRender;
    public Color AdditiveTint;
    public Color MultiplyTint;
    public float Blur;
    // Start is called before the first frame update
    void Start()
    {
        BlurRender = BlurObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        BlurRender.material.SetColor("_AdditiveColor", AdditiveTint);
        BlurRender.material.SetColor("_MultiplyColor", MultiplyTint);
        BlurRender.material.SetFloat("_Size", Blur);
    }
}
