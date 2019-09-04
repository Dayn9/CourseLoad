using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagEdit : MonoBehaviour
{
    [SerializeField] private ScreenController screenControl;
    [SerializeField] private TagSelect tagSelect;
    [SerializeField] private TaskList taskList;

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
        //loop over all edits and check for changes
        for (int i = 0; i < editTags.Length; i++)
        {
            tagSelect.Tags[i].color = editTags[i].image.color;

            string currentTag = tagSelect.Tags[i].name;
            string newTag = editTags[i].input.text;
            //check if the input is not empty or already set to that value
            if (newTag.Trim() != "" && !newTag.Equals(currentTag))
            {
                //update the taskUI's in the task list
                tagSelect.UpdateTag(currentTag, newTag);
                taskList.UpdateTag(currentTag, newTag);
                //update the tag name stored in tag select
                tagSelect.Tags[i].name = editTags[i].input.text;
            }         
        }
        //refresh the tags to update labels in creation window
        tagSelect.SetSelectionUI(); 

        screenControl.ShowTaskScreen();
    }
}

[System.Serializable]
public class EditTag
{
    public Image image;
    public InputField input;
}
