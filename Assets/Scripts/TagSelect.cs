using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagSelect : MonoBehaviour
{
    [SerializeField] private Tag[] tags;

    public static Dictionary<string, Tag> tagLookup;

    private Tag selectedTag;

    public Tag SelectedTag { get { return selectedTag; } }

    private void Awake()
    {
        if(tagLookup == null)
        {
            tagLookup = new Dictionary<string, Tag>();
            foreach (Tag tag in tags)
            {
                tagLookup.Add(tag.name, tag);
            }
        }

        foreach(Tag tag in tags)
        {
            tag.toggle.transform.GetComponentInChildren<Image>().color = tag.color;
            tag.toggle.GetComponentInChildren<Text>().text = tag.name;
        }
    }

    private void Start()
    {
        tags[0].toggle.GetComponent<TagToggle>().SelectTag(true);
        selectedTag = tags[0];
    }

    public void TagSelected(string name)
    {
        selectedTag = tagLookup[name];
    }
}

[System.Serializable]
public struct Tag
{
    public string name;
    public Color color;
    public Toggle toggle;
}
