using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private string keyID = "DoorKey";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddKey(keyID);
                Destroy(gameObject); 
            }
            SFXScript.Instance.PlayEffect(SFXScript.Instance.pickup);
        }
    }
}