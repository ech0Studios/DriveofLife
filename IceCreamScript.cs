
using UnityEngine;

public class IceCreamScript : MonoBehaviour
{
    public GameObject iceCreamFX;
    public GameObject[] emojiFX;
    public AudioClip[] funnyYay;
    public AudioClip splashSound;
    
    
    void Update()
    {
        
        if (transform.position.y < -1)
        {
            Destroy(gameObject);          
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Plane"))
        {
            AudioSource.PlayClipAtPoint(splashSound, transform.position);
            Instantiate(iceCreamFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
       
        if (other.gameObject.CompareTag("Untagged"))
        {
            Instantiate(iceCreamFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("People"))
        {
            GameManager.inGameScore += 50;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;            
            
            int y = Random.Range(0, 4);
            AudioSource.PlayClipAtPoint(funnyYay[y], transform.position);

            int i = Random.Range(0, 6);
            Instantiate(emojiFX[i], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    
}
