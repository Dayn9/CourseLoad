using UnityEngine;
using UnityEngine.UI;

public class Version : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = "version " + Application.version;
    }
}
