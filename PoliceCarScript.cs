using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.AI;
using MilkShake;

public class PoliceCarScript : MonoBehaviour
{
    public static float SpeedUp = 75;
    public GameObject[] coinFX;
    public ShakePreset crashShake;
    public ShakePreset nitroShake;
    public AudioClip[] heySounds;
    public AudioClip[] carHorns;
    public AudioClip policeSiren;
    public AudioClip nitroSound;
    public AudioClip thiefCrashSound;
    public AudioClip crashSound;

    void Start()
    {
        AudioSource.PlayClipAtPoint(policeSiren, transform.position);
        PlayerScript.rb.interpolation = RigidbodyInterpolation.Interpolate;
        PlayerScript.playerSpeed = 700;
    }
    void Update()
    {
        PlayerScript.rb.velocity = new Vector3(PlayerScript.rb.velocity.x, PlayerScript.rb.velocity.y, PlayerScript.playerSpeed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Nitro"))
        {
            GameManager.inGameScore += 50;
            MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            AudioSource.PlayClipAtPoint(nitroSound, transform.position);
            Instantiate(coinFX[0], other.transform.position + Vector3.up, Quaternion.identity);
            Shaker.ShakeAll(nitroShake);
            PlayerScript.playerSpeed += SpeedUp;
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Thief"))
        {
            GameManager.inGameScore += 1000;
            MMVibrationManager.Haptic(HapticTypes.Success);
            AudioSource.PlayClipAtPoint(thiefCrashSound, transform.position);
            AudioSource.PlayClipAtPoint(crashSound, transform.position);
            Instantiate(coinFX[1], collision.transform.position + Vector3.up, Quaternion.identity);
            Shaker.ShakeAll(crashShake);
            collision.transform.GetComponent<NavMeshAgent>().enabled = false;
            collision.transform.GetChild(1).gameObject.SetActive(true);
            Destroy(collision.gameObject, 2);
        }
        if (collision.gameObject.CompareTag("Car"))
        {
            int i = Random.Range(0, 8);
            int h = Random.Range(0, 2);

            Shaker.ShakeAll(crashShake);
            MMVibrationManager.Haptic(HapticTypes.Warning);
            AudioSource.PlayClipAtPoint(carHorns[h], transform.position);
            AudioSource.PlayClipAtPoint(heySounds[i], transform.position);
            if (PlayerScript.playerSpeed >= 700)
            {
                PlayerScript.playerSpeed -= SpeedUp;
            }
            Destroy(collision.gameObject, 2);
        }
    }
}
