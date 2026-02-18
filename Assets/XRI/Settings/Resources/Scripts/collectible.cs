using UnityEngine;

public class Collectible : MonoBehaviour
{
    public EscapeGameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // VR hands OR player body
        if (other.CompareTag("Player") || other.CompareTag("Hand"))
        {
            gameManager.CollectItem();
            Destroy(gameObject);
        }
    }
}