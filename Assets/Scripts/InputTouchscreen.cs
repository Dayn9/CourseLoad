using UnityEngine;
using UnityEngine.EventSystems;

public class InputTouchscreen : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //Open the keyboard for IOS and Android
#if UNITY_IOS || UNITY_ANDROID
        if (!TouchScreenKeyboard.visible)
        {
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }
#endif
    }
}
