using UnityEngine;

public class WinBoxScript : MonoBehaviour
{
   [SerializeField] private GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.GetComponent<PlayerController>().Win();
        }
    }
}
