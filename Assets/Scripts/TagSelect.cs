using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class TagSelect : MonoBehaviour
{
    [SerializeField] private Tag[] tags;

    public static Dictionary<string, Tag> tagLookup;

    private Tag selectedTag;

    private const string GameSaveFileName = "/TagData";
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

    private void Start()
    {
        tags[0].toggle.GetComponent<TagToggle>().SelectTag(true);
        selectedTag = tags[0];
    }

    public void TagSelected(string name)
    {
        selectedTag = tagLookup[name];
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
public struct Tag
{
    public string name;
    public Color color;
    public Toggle toggle;
}

