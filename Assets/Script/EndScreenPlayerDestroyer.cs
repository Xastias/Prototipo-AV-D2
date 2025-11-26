<<<<<<< Updated upstream
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
=======
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
>>>>>>> Stashed changes
}