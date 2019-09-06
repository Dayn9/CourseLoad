using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TagSelect : MonoBehaviour
{
    [SerializeField] private Tag[] tags;

    private Dictionary<string, Tag> tagLookup;

    private Tag selectedTag;

    private const string GameSaveFileName = "/TagsData";
    private const string FileExtension = ".dat";

    public Tag SelectedTag { get { return selectedTag; } }

    public Tag[] Tags { get { return tags; } }

    private void Awake()
    {
        LoadData();

        if (tagLookup == null)
        {
            tagLookup = new Dictionary<string, Tag>();
            foreach (Tag tag in tags)
            {
                tagLookup.Add(tag.name, tag);
            }
        }

        SetSelectionUI();
    }

    public Tag GetTag(string tagName)
    {
        //return a default tag if tag lookup is empty for some reason
        if(tagLookup.Count == 0)
        {
            return new Tag();
        }

        //return the tag from lookup if it exists
        if (tagLookup.ContainsKey(tagName))
        {
            return tagLookup[tagName];
        }

        //otherwise return the Other Tag
        return tagLookup["Other"];
    }

    public void UpdateTag(string oldName, string newName)
    {
        Tag t = tagLookup[oldName];
        t.name = newName;
        tagLookup.Remove(oldName);
        tagLookup.Add(newName, t);
    }

    public void SetSelectionUI()
    {
        foreach (Tag tag in tags)
        {
            tag.toggle.transform.GetComponentInChildren<Image>().color = tag.color;
            tag.toggle.GetComponentInChildren<Text>().text = tag.name;
        }

        SaveData();
    }
    /*
    private void Start()
    {
        SetToggle("Other");
    }*/

    public void SetToggle(string name)
    {
        //toggle off the selected tag
        if(selectedTag != null)
        {
            selectedTag.toggle.GetComponent<TagToggle>().SelectTag(false);
        }

        //toggle on the new tag
        if (tagLookup.ContainsKey(name))
        {
            tagLookup[name].toggle.GetComponent<TagToggle>().SelectTag(true);
        }
        else
        {
            //default to other
            tagLookup["Other"].toggle.GetComponent<TagToggle>().SelectTag(true);
        }

    }

    public void SetSelected(string name)
    {
        if (tagLookup.ContainsKey(name))
        {
            selectedTag = tagLookup[name];
        }
        else
        {
            //default to other
            selectedTag = tagLookup["Other"];
        }
    }

    public void SaveData()
    {
        TagData[] tagData = new TagData[tags.Length];

        for (int i = 0; i < tagData.Length; i++)
        {
            tagData[i] = new TagData();
            tagData[i].name = tags[i].name;
            tagData[i].color = ColorToArr(tags[i].color);
        }    
        Save(tagData);
    }

    public void LoadData()
    {
        TagData[] tagData = Load();
        if (tagData != null)
        {
            for(int i = 0; i<tagData.Length; i++)
            {
                tags[i].name = tagData[i].name;
                tags[i].color = ArrToColor(tagData[i].color);
            }
        }
    }

    private void Save(TagData[] saveData)
    {
        string dataPath = Application.persistentDataPath + GameSaveFileName + FileExtension;
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(dataPath, FileMode.OpenOrCreate);

        try
        {
            binaryFormatter.Serialize(fileStream, saveData);
        }
        catch (System.Exception e)
        {
            Debug.Log("Serialization Failed: " + e.Message);
        }
        finally
        {
            //always close the fileStream
            fileStream.Close();
        }
    }

    private TagData[] Load()
    {
        TagData[] tagData = null;
        string dataPath = Application.persistentDataPath + GameSaveFileName + FileExtension;

        //make sure the file actually exists
        if (File.Exists(dataPath))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(dataPath, FileMode.Open);

            try
            {
                tagData = (TagData[])binaryFormatter.Deserialize(fileStream);
            }
            catch (System.Exception e)
            {
                Debug.Log("Game Deserialization Failed: " + e.Message);
            }
            finally
            {
                //always close the fileStream
                fileStream.Close();
            }
        }

        return tagData;
    }

    private float[] ColorToArr(Color col)
    {
        return new float[4] { col.r, col.g, col.b, col.a };
    }

    private Color ArrToColor(float[] col)
    {
        return new Color(col[0], col[1], col[2], col[3]);
    }
}

[System.Serializable]
public class TagData
{
    public string name;
    public float[] color;
}

[System.Serializable]
public class Tag
{
    public string name;
    public Color color;
    public Toggle toggle;
}

