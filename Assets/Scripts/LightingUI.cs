using UnityEngine;

public class LightingUI : MonoBehaviour
{
    [SerializeField] private GameObject lightingText;
    [SerializeField] private ButtonSwitch buttonSwitch;

    private void OnEnable()
    {
        buttonSwitch.OnEnterLight += TurnOnTip;
        buttonSwitch.OnExitLight += TurnOffTip;
    }

    private void OnDisable()
    {
        buttonSwitch.OnEnterLight -= TurnOnTip;
        buttonSwitch.OnExitLight -= TurnOffTip;
    }

    private void TurnOnTip()
    {
        lightingText.SetActive(true);
    }

    private void TurnOffTip()
    {
        lightingText.SetActive(false);
    }
}
