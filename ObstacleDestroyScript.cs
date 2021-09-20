
using UnityEngine;

public class ObstacleDestroyScript : MonoBehaviour
{

    void Update()
    {
        if (transform.position.z < PlayerScript.rb.position.z - 15)
        {
            Destroy(gameObject);
        }
    }
}
