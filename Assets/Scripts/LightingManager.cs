using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light[] sceneLights;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color warmColor = new Color(1f, 0.6f, 0.2f);

    private int state = 0; // 0 = on, 1 = warm, 2 = off
    public bool LightsOn => state != 2;

    public void CycleState()
    {
        state = (state + 1) % 3;
        foreach (Light l in sceneLights)
        {
            switch (state)
            {
                case 0:
                    l.enabled = true;
                    l.color = normalColor;
                    break;
                case 1:
                    l.enabled = true;
                    l.color = warmColor;
                    break;
                case 2:
                    l.enabled = false;
                    break;
            }
        }
    }
}