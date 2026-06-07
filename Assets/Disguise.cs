using UnityEngine;
using TMPro;

public class DisguiseItem : MonoBehaviour
{
    [SerializeField] private GameObject disguiseText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ActivateDisguise(30f);
            if (disguiseText != null)
                disguiseText.SetActive(true);
            Destroy(gameObject);
        }
    }
}