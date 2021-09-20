using MilkShake;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class LeftColliderScript : MonoBehaviour
{
    public ShakePreset crashShake;
    public ShakePreset throwShake;
    public AudioClip[] carHorns;
    public AudioClip crashVX;
    public AudioClip obstacleVX;
    public GameObject[] coinfFX;
    public GameObject waterFX;
    public AudioClip waterSound;
    private void OnCollisionEnter(Collision collision)
    {
        
        Quaternion rollLeft = Quaternion.Euler(0, 0, -180);
        if ( collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.inGameScore += 50;
            AudioSource.PlayClipAtPoint(obstacleVX, transform.position);
            MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            Instantiate(coinfFX[0], collision.transform.position + (Vector3.up*2), Quaternion.identity);
            Shaker.ShakeAll(throwShake);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(-2, 0, 2) * 500 * Time.deltaTime, ForceMode.Impulse);
            collision.transform.rotation = Quaternion.RotateTowards(transform.rotation, rollLeft, 15);
            Destroy(collision.gameObject, 2);
        }
        if (collision.gameObject.CompareTag("Car"))
        {
            GameManager.inGameScore += 200;
            int i = Random.Range(0, 2);
            AudioSource.PlayClipAtPoint(crashVX, transform.position);
            Instantiate(coinfFX[1], collision.transform.position + (Vector3.up * 2), Quaternion.identity);
            AudioSource.PlayClipAtPoint(carHorns[i], transform.position);
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            Shaker.ShakeAll(crashShake);
        }
        if (collision.gameObject.CompareTag("musluk"))
        {
            Quaternion waterRotation = Quaternion.Euler(-90, 0, 0);
            GameManager.inGameScore += 50;
            AudioSource.PlayClipAtPoint(obstacleVX, transform.position);
            AudioSource.PlayClipAtPoint(waterSound, transform.position);
            MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            Instantiate(coinfFX[0], collision.transform.position + (Vector3.up * 2), Quaternion.identity);
            Instantiate(waterFX, collision.transform.position , waterRotation);
            Shaker.ShakeAll(throwShake);
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(-2, 0, 2) * 500 * Time.deltaTime, ForceMode.Impulse);
            collision.transform.rotation = Quaternion.RotateTowards(transform.rotation, rollLeft, 15);
            Destroy(collision.gameObject, 2);
        }


    }
}
