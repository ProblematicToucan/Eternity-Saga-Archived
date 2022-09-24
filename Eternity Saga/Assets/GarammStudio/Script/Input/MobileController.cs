using UnityEngine;

public class MobileController : MonoBehaviour
{
    [SerializeField] private RectTransform rightStick;
    [SerializeField, Tooltip("Show right stick in editor")] private bool showRightStick = false;
    [SerializeField] private RectTransform leftStick;
    [SerializeField, Tooltip("Show left stick in editor")] private bool showLeftStick = false;
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            rightStick.gameObject.SetActive(showRightStick);
            leftStick.gameObject.SetActive(showLeftStick);
        }
    }
}