using MilkShake;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class MonsterTruckScript : MonoBehaviour
{
    public ShakePreset throwShake;
    public ShakePreset crashShake;
    public GameObject dustFX;
    public AudioClip crashVX;
    public AudioClip obstacleVX;
    public GameObject[] coinfFX;
    public GameObject waterFX;
    public AudioClip waterSound;

    void Update()
    {
        PlayerScript.rb.velocity = new Vector3(PlayerScript.rb.velocity.x, PlayerScript.rb.velocity.y, (PlayerScript.playerSpeed + 300) * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Obstacle"))
        {
            GameManager.inGameScore += 50;
            AudioSource.PlayClipAtPoint(obstacleVX, transform.position);
            MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            Instantiate(coinfFX[0], other.transform.position + (Vector3.up * 2), Quaternion.identity);
            Quaternion rotation = Quaternion.Euler(-180, 0, 0);
            Shaker.ShakeAll(throwShake);
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 300 * Time.deltaTime, ForceMode.Impulse);
            other.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 15);
            Destroy(other.gameObject, 2);
        }
        if (other.gameObject.CompareTag("Car"))
        {
            GameManager.inGameScore += 200;
            MMVibrationManager.Haptic(HapticTypes.Warning);
            Instantiate(dustFX, other.transform.position + (Vector3.up * 2), Quaternion.identity);
            Instantiate(coinfFX[0], other.transform.position + Vector3.up, Quaternion.identity);
            AudioSource.PlayClipAtPoint(crashVX, transform.position);
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.transform.localScale = new Vector3(1, 0.3f, 1);
            Shaker.ShakeAll(crashShake);
            Destroy(other.gameObject, 2);
        }
        if (other.gameObject.CompareTag("musluk"))
        {
            Quaternion waterRotation = Quaternion.Euler(-90, 0, 0);
            GameManager.inGameScore += 50;
            AudioSource.PlayClipAtPoint(obstacleVX, transform.position);
            AudioSource.PlayClipAtPoint(waterSound, transform.position);
            MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            Instantiate(coinfFX[0], other.transform.position + (Vector3.up * 2), Quaternion.identity);
            Instantiate(waterFX, other.transform.position, waterRotation);
            Quaternion rotation = Quaternion.Euler(-180, 0, 0);
            Shaker.ShakeAll(throwShake);
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * 300 * Time.deltaTime, ForceMode.Impulse);
            other.transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 15);
            Destroy(other.gameObject, 2);
        }
    }
}
