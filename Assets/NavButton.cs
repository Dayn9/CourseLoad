using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavButton : MonoBehaviour
{
    [SerializeField] private ScreenController screenController;
    [SerializeField] private TaskList taskList;

    private const float animTime = 1;

    private RectTransform icon;


    private void Awake()
    {
        icon = (RectTransform)transform.GetChild(0);

        //On Button Click
        GetComponent<Button>().onClick.AddListener(() => {
            switch(screenController.CurrentScreen){
                //Task screen -> Create Screen
                case ScreenUI.Task:
                    screenController.SetScreen(ScreenUI.Create);
                    taskList.StopEdit();
                    break;
                //Create screen -> Task Screen
                case ScreenUI.Create:
                    screenController.SetScreen(ScreenUI.Task);
                    break;
                //Settings screen -> Task Screen
                case ScreenUI.Settings:
                    screenController.SetScreen(ScreenUI.Task);
                    break;
                //Info screen -> Task Screen
                case ScreenUI.Info:
                    screenController.SetScreen(ScreenUI.Task);
                    break;
            }
        });

        screenController.OnScreenChange += () => StartCoroutine(AnimateUI());
    }

    public IEnumerator AnimateUI()
    {
        float targetAngle = (screenController.CurrentScreen == ScreenUI.Task) ? 0 : 45;
        float t = 0, p = 0;

        while (t < animTime)
        {
            t += Time.deltaTime;
            p = t / animTime;

            //p = p * p;
            p = p * p * (3 - 2 * p);

            //interpolate and set new icon rotation
            float z = Mathf.Lerp(icon.eulerAngles.z, targetAngle, p);
            icon.eulerAngles = new Vector3(0, 0, z);

            yield return new WaitForEndOfFrame();
        }
    }
}
