using MilkShake;
using UnityEngine;
using MoreMountains.NiceVibrations;
public class GarbageTruckScript : MonoBehaviour
{
    private bool changeLane = false;
    public AudioClip[] horn;
    public AudioClip crash;
    public GameObject garbage;
    public ShakePreset crashShake;


    void Start()
    {
        PlayerScript.rb.interpolation = RigidbodyInterpolation.None;
    }


    void Update()
    {

        PlayerScript.rb.transform.position += Vector3.forward * (PlayerScript.playerSpeed - 675) * Time.deltaTime;


        if (Input.GetMouseButtonDown(0))
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            changeLane = !changeLane;
        }
        else if (!changeLane)
        {
            PlayerScript.rb.transform.position = Vector3.Lerp(PlayerScript.rb.transform.position, new Vector3(0.5f, PlayerScript.rb.transform.position.y, PlayerScript.rb.transform.position.z), 0.1f);
        }
        else if (changeLane)
        {
            PlayerScript.rb.transform.position = Vector3.Lerp(PlayerScript.rb.transform.position, new Vector3(-4.4f, PlayerScript.rb.transform.position.y, PlayerScript.rb.transform.position.z), 0.1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            int i = Random.Range(0, 2);
            MMVibrationManager.Haptic(HapticTypes.Warning);
            Instantiate(garbage, transform.position + new Vector3(0, 2, -1), Quaternion.identity);
            AudioSource.PlayClipAtPoint(horn[i], transform.position);
            AudioSource.PlayClipAtPoint(crash, transform.position);
            Destroy(collision.gameObject, 4);
        }
    }
}
