using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagEdit : MonoBehaviour
{
    [SerializeField] private ScreenController screenControl;

    [SerializeField] private TagSelect tagSelect;

    [SerializeField] private EditTag[] editTags;

    private void OnEnable()
    {
        //read in the color and name of each of the tags
        Tag[] tags = tagSelect.Tags;
        for(int i = 0; i<tags.Length; i++)
        {
            editTags[i].image.color = tags[i].color;
            editTags[i].input.text = tags[i].name;
        }
    }


    public void ApplyChanges()
    {
        Tag[] tags = tagSelect.Tags;
        for (int i = 0; i < editTags.Length; i++)
        {
            tags[i].color = editTags[i].image.color;
            if(editTags[i].input.text.Trim() != "")
            {
                tags[i].name = editTags[i].input.text;
            }         
        }

        screenControl.ShowTaskScreen();
    }
}

[System.Serializable]
public class EditTag
{
    public Image image;
    public InputField input;
}
