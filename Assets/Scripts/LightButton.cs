using UnityEngine;
using UnityEngine.InputSystem;
//The physical button
public class ButtonSwitch : MonoBehaviour
{
    [SerializeField] private LightingManager lightingManager;
    [SerializeField] private MeshRenderer buttonMesh;

    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            lightingManager.CycleState();
            buttonMesh.material.color = lightingManager.LightsOn ? Color.green : Color.red;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerNearby = false;
    }
}