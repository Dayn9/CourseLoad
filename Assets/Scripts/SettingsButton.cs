using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private RectTransform optionsMenu;

    private RectTransform gearIcon;

    private const float animTime = 1;

    private HorizontalLayoutGroup horizontalLayout;
    private float iconWidth;

    private bool visible = false; 

    private void Awake()
    {
        gearIcon = transform.GetChild(0).GetComponent<RectTransform>();
        gearIcon.eulerAngles = new Vector3(0, 0, 0);

        horizontalLayout = optionsMenu.GetComponent<HorizontalLayoutGroup>();
        iconWidth = ((RectTransform)gearIcon.transform.parent).sizeDelta.y;
        horizontalLayout.spacing = -iconWidth;
    }

    public void ToggleOptions()
    {
        visible = !visible;
        StartCoroutine(AnimateUI());
    }

    public IEnumerator AnimateUI()
    {
        float targetAngle = visible ? 90 : 0;
        float targetSpacing = visible ? 25 : -iconWidth;
        float t = 0, p = 0;

        while(t < 1)
        {
            t += Time.deltaTime;
            p = t / animTime;

            //p = p * p;
            p = p * p * (3 - 2 * p);

            //interpolate and set new gear rotation
            float z = Mathf.Lerp(gearIcon.eulerAngles.z, targetAngle, p);
            gearIcon.eulerAngles = new Vector3(0, 0, z);

            //interpolate and set new aspect ratios
            horizontalLayout.spacing = Mathf.Lerp(horizontalLayout.spacing, targetSpacing, p);

            yield return new WaitForEndOfFrame();
        }
    }

}
