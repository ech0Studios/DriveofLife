
using UnityEngine;
using MoreMountains.NiceVibrations;

public class GarbageScript : MonoBehaviour
{
    public GameObject sparkleFX;
    public AudioClip garbageVX;
    public GameObject coinFX;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GarbageTrigger"))
        {
            GameManager.inGameScore += 50;
            MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            Instantiate(coinFX, other.transform.position + Vector3.up, Quaternion.identity);
            transform.GetComponent<BoxCollider>().enabled = false;
            Instantiate(sparkleFX, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(garbageVX, transform.position);          
            Destroy(gameObject);
        }
    }
     void Update()
    {
        if (transform.position.z < PlayerScript.rb.position.z - 15)
        {
            Destroy(gameObject);
        }
    }
}
