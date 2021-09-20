using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleScript : MonoBehaviour
{
   
    void Update()
    {
        if (transform.position.z<PlayerScript.rb.transform.position.z-25)
        {
            Destroy(gameObject);
        }
    }
}
