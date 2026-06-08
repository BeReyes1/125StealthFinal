using UnityEngine;
using TMPro;

public class DisguiseItem : MonoBehaviour
{
    [SerializeField] private GameObject disguiseText;
    [SerializeField] private float disguiseTime = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ActivateDisguise(disguiseTime);
            if (disguiseText != null)
                disguiseText.SetActive(true);
            Destroy(gameObject);
        }
    }
}