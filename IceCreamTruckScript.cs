
using UnityEngine;
using MoreMountains.NiceVibrations;

public class IceCreamTruckScript : MonoBehaviour
{
    public AudioClip IceCreamTruckSound;
    public GameObject IceCreamChangeLanePanel;
    void Start()
    {
        AudioSource.PlayClipAtPoint(IceCreamTruckSound, transform.position);
    }
    void Update()
    {
        PlayerScript.rb.velocity = new Vector3(PlayerScript.rb.velocity.x, PlayerScript.rb.velocity.y, PlayerScript.playerSpeed * Time.fixedDeltaTime);

         if (Input.GetMouseButtonDown(0) && PlayerScript.onIceCreamSelect && GameManager.currentState == 1) //Ice Cream Movement To Select A Gate
        {
            IceCreamChangeLanePanel.SetActive(false);
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            PlayerScript.changeLane = !PlayerScript.changeLane;
        }
        else if (PlayerScript.changeLane && PlayerScript.onIceCreamSelect)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(2, transform.position.y, transform.position.z), 0.1f);
        }
        else if (!PlayerScript.changeLane && PlayerScript.onIceCreamSelect)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(-2, transform.position.y, transform.position.z), 0.1f);
        }
    }
}
