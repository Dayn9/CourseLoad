using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagInput : MonoBehaviour
{
    [SerializeField] private Image colorImage;
    [SerializeField] private Button colorButton;

    private static TagEdit tagEdit;

    private void Awake()
    {
        if(tagEdit == null)
        {
            tagEdit = FindObjectOfType<TagEdit>();
        }

        colorButton.onClick.RemoveAllListeners();
        colorButton.onClick.AddListener(Click);
    }

    public void Click()
    {
        tagEdit.EditColor(colorImage);
    }
}
