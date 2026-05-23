using UnityEngine;

public class WinBoxScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   [SerializeField] private GameObject Player;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.GetComponent<PlayerController>().Win();
        }
    }
}
