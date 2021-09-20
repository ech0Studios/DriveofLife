
using UnityEngine;

public class CarBackScript : MonoBehaviour
{

    private Rigidbody rb;
    public static float carSpeed = 12;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }
    void Update()
    {
        transform.position += Vector3.back * (carSpeed + 4) * Time.deltaTime;

        if (transform.position.z < PlayerScript.rb.position.z - 25)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Quaternion rollLeft = Quaternion.Euler(0, 0, -180);
        Quaternion rollRight = Quaternion.Euler(0, 0, 180);

        if (collision.gameObject.CompareTag("LeftCollider"))
        {

            rb.constraints = RigidbodyConstraints.None;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rollLeft, 15);
            rb.AddForce(new Vector3(-2, 0, 2) * 500 * Time.deltaTime, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("RightCollider"))
        {

            rb.constraints = RigidbodyConstraints.None;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rollRight, 15);
            rb.AddForce(new Vector3(2, 0, 2) * 500 * Time.deltaTime, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("FrontTrigger"))
        {
            transform.GetComponent<CarBackScript>().enabled = false;
        }
    }
}
