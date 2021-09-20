using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDestroyerScript : MonoBehaviour
{

    void Update()
    {
        if (PlayerScript.onStateChange)
        {
            Destroy(gameObject);
        }
    }
}
