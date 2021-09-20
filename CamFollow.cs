
using UnityEngine;
public class CamFollow : MonoBehaviour
{
    private Transform target;
    public float Speed = 0.5f;
    public static float posX = 0;
    public static float posY = 8;
    public static float posZ = 0;


    void Start()
    {

        target = GameObject.Find("Player").transform;

    }
    void LateUpdate()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion exitStateRotation = Quaternion.Euler(16, -17, 0);
        Quaternion garbageStateRotation = Quaternion.Euler(30, 0, 0);



        if (GameManager.currentState == 0) //Police State
        {
            Speed = 1.5f;
            Vector3 stateChangePos = new Vector3(target.transform.position.x + posX + 4, target.position.y + posY, target.transform.position.z + posZ-1 );
            transform.rotation = Quaternion.RotateTowards(currentRotation, exitStateRotation, 1);
            gameObject.transform.position = Vector3.Lerp(transform.position, stateChangePos, Speed * Time.deltaTime);

        }
        else if (GameManager.currentState == 1)// Ice Cream State
        {
            Speed = 0.7f;
            Vector3 stateChangePos = new Vector3(target.transform.position.x + posX, target.position.y + posY, target.transform.position.z + posZ);
            transform.rotation = Quaternion.RotateTowards(currentRotation, garbageStateRotation, 1);
            gameObject.transform.position = Vector3.Lerp(transform.position, stateChangePos, Speed * Time.deltaTime);

        }
        else if (GameManager.currentState == 2)//Garbage State
        {

            Speed = 1;
            Vector3 targetPos = new Vector3(target.transform.position.x + posX, target.position.y + posY, target.transform.position.z + posZ + 5);
            transform.rotation = Quaternion.RotateTowards(currentRotation, garbageStateRotation, 1);
            gameObject.transform.position = Vector3.Lerp(transform.position, targetPos, Speed * Time.deltaTime);


        }
        else if (GameManager.currentState == 3) //Chaos State
        {
            Speed = 1;
            Vector3 stateChangePos = new Vector3(target.transform.position.x + posX + 4, target.position.y + 8, target.transform.position.z - 5);
            transform.rotation = Quaternion.RotateTowards(currentRotation, exitStateRotation, 1);
            gameObject.transform.position = Vector3.Lerp(transform.position, stateChangePos, Speed * Time.deltaTime);

        }







    }
}
