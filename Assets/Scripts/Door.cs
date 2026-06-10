using UnityEngine;
using UnityEngine.AI;

public class DoorController : MonoBehaviour
{
    [SerializeField] private string requiredKeyID = "DoorKey";
    [SerializeField] private bool consumeKey = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.HasKey(requiredKeyID))
            {
                if (consumeKey)
                    inventory.RemoveKey(requiredKeyID);

                Destroy(gameObject);
                SFXScript.Instance.PlayEffect(SFXScript.Instance.unlock);
            }
        }
    }
}