using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagEdit : MonoBehaviour
{
    [SerializeField] private ScreenController screenControl;
    [SerializeField] private TagSelect tagSelect;
    [SerializeField] private ColorSelect colorSelect;
    [SerializeField] private TaskList taskList;

    [SerializeField] private EditTag[] editTags;

    private void OnEnable()
    {
        //read in the color and name of each of the tags
        Tag[] tags = tagSelect.Tags;
        int[] counts = taskList.GetRemainingCounts();
        for (int i = 0; i<tags.Length; i++)
        {
            editTags[i].image.color = tags[i].color;
            editTags[i].input.text = tags[i].name;
            editTags[i].remaining.text = counts[i].ToString();
        }

        colorSelect.gameObject.SetActive(false);
    }

    public void EditColor(Image target)
    {
        colorSelect.gameObject.SetActive(true);
        colorSelect.SetTargetImage(target);
    }

    public void ApplyChanges()
    {
        //loop over all edits and check for changes
        for (int i = 0; i < editTags.Length; i++)
        {
            tagSelect.Tags[i].color = editTags[i].image.color;

            string currentTag = tagSelect.Tags[i].name;
            string newTag = editTags[i].input.text;
            //check if the input is not empty or already set to that value
            if (newTag.Trim() != "" && !newTag.Equals(currentTag))
            {
                //update the tag lookup
                tagSelect.UpdateTag(currentTag, newTag);
                
                tagSelect.Tags[i].name = newTag;
            }         
        }
        //refresh the tags to update labels in creation window
        tagSelect.SetSelectionUI();
        taskList.RefreshTags();
        //save the new names
        taskList.SaveData();

        screenControl.SetScreen(ScreenUI.Task);
    }
}

[System.Serializable]
public class EditTag
{
    public Image image;
    public InputField input;
    public Text remaining;
}
