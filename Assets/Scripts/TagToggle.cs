using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagToggle : MonoBehaviour
{
    private static TagSelect tagSelect;

    private Toggle toggle;
    private Text nameText;

    private void Awake()
    {
        if (tagSelect == null)
        {
            tagSelect = FindObjectOfType<TagSelect>();
        }

        toggle = GetComponent<Toggle>();
        nameText = GetComponentInChildren<Text>();

        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(SelectTag);
    }

    public void SelectTag(bool tog)
    {
        if (tog)
        {
            tagSelect.SetSelected(nameText.text);
        }
        toggle.isOn = tog;
    }
}
