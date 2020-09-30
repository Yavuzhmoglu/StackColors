using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitchController : MonoBehaviour
{
    public Color[] colors;
    int br_Color;
    Color SecondColor;
    public Material GroundMaterial;
   
    void Start()
    {
        br_Color = Random.Range(0, colors.Length);


        GroundMaterial.color = colors[br_Color];
        Camera.main.backgroundColor = colors[br_Color];



        SecondColor = colors[SecondColorSwitch()];
    }
    int SecondColorSwitch()
    {
        int ik_Color;
        if (colors.Length <= 1)
        {
            ik_Color = br_Color;
            return ik_Color;
        }
        ik_Color = Random.Range(0, colors.Length);
        while (ik_Color == br_Color)
        {
            ik_Color = Random.Range(0, colors.Length);
        }
        return ik_Color;
    }
    // Update is called once per frame
    void Update()
    {
        Color fark = GroundMaterial.color - SecondColor;
        if (Mathf.Abs(fark.r) + Mathf.Abs(fark.g) + Mathf.Abs(fark.b) < 0.02f)
        {
            SecondColor = colors[SecondColorSwitch()];
        }
        GroundMaterial.color = Color.Lerp(GroundMaterial.color, SecondColor, 0.003f);
        //highscore.color = Color.Lerp(highscore.color, ikinciRenk, 0.003f);
        Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, SecondColor, 0.007f);


    }
}
