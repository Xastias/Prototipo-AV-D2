using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenPlayerDestroyer : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }
    }
}