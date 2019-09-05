using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelect : MonoBehaviour
{
    [SerializeField] private Image preview;

    private Color displayColor = Color.black;

    private Slider sliderR;
    private Slider sliderG;
    private Slider sliderB;

    private InputField numR;
    private InputField numG;
    private InputField numB;

    private Image bgR;
    private Image bgG;
    private Image bgB;

    private void Awake()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();
        InputField[] nums = GetComponentsInChildren<InputField>();
        if (sliders.Length >= 3 && nums.Length >= 3)
        {
            sliderR = sliders[0];
            numR = nums[0];
            bgR = sliderR.GetComponentInChildren<Image>();

            sliderG = sliders[1];
            numG = nums[1];
            bgG = sliderG.GetComponentInChildren<Image>();

            sliderB = sliders[2];
            numB = nums[2];
            bgB = sliderB.GetComponentInChildren<Image>();
        }

        preview.color = displayColor;
    }

    private void SetColor(Color color)
    {
        displayColor = color;

        sliderR.value = color.r;
        sliderG.value = color.g;
        sliderB.value = color.b;

        numR.text = Mathf.FloorToInt(displayColor.r * 255).ToString();
        numG.text = Mathf.FloorToInt(displayColor.g * 255).ToString();
        numB.text = Mathf.FloorToInt(displayColor.b * 255).ToString();
    }

    private Color GetColor()
    {
        return displayColor;
    }

    private void Update()
    {
        displayColor.r = sliderR.value;
        displayColor.g = sliderG.value;
        displayColor.b = sliderB.value;

        numR.text = Mathf.FloorToInt(displayColor.r * 255).ToString();
        numG.text = Mathf.FloorToInt(displayColor.g * 255).ToString();
        numB.text = Mathf.FloorToInt(displayColor.b * 255).ToString();

        bgR.material.SetColor("_Color0", new Color(0, displayColor.g, displayColor.b));
        bgR.material.SetColor("_Color1", new Color(1, displayColor.g, displayColor.b));

        bgG.material.SetColor("_Color0", new Color(displayColor.r, 0, displayColor.b));
        bgG.material.SetColor("_Color1", new Color(displayColor.r, 1, displayColor.b));

        bgB.material.SetColor("_Color0", new Color(displayColor.r, displayColor.g, 0));
        bgB.material.SetColor("_Color1", new Color(displayColor.r, displayColor.g, 1));

        preview.color = displayColor;
    }


}
