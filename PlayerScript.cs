using System.Collections;
using MilkShake;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class PlayerScript : MonoBehaviour
{
    private Vector3 screenPoint;

    public static Rigidbody rb;
    public static bool dontMove = false;
    public static bool changeLane = false;
    public static bool onStateChange = false;
    public static bool onIceCreamSelect = false;
    public static int enteredGate = 0;
    public static float playerSpeed = 700;

    public GameObject[] spawnableStates;
    public GameObject IceCreamChangeLanePanel;
    public GameObject[] panels;
    public GameObject states;
    public GameObject finishGate;
    public GameObject iceCream;
    public ShakePreset crashShake;

    public AudioClip[] stateStartSounds;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        playerSpeed = 700;
        dontMove = false;
        Invoke("ChecktheState", 0.1f);
        onIceCreamSelect = false;
        enteredGate = 0;
    }
    void FixedUpdate()
    {

        if (GameManager.isGameOver == false && GameManager.currentState != 2)
        {          
            // Movement Input For States
            if (transform.position.x > -4.5f && transform.position.x < 4.5f && !dontMove && GameManager.currentState != 3)
            {
                #region Police State

                if (Input.GetMouseButtonDown(0))
                {
                    panels[0].SetActive(false);
                    MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                }
                if (Input.GetMouseButton(0) && GameManager.currentState == 0) // Police State
                {
                    screenPoint = Camera.main.WorldToScreenPoint(transform.position);
                    Vector3 curScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                    transform.position = Vector3.Lerp(transform.position, new Vector3(curScreenPoint.x, transform.position.y, transform.position.z), 0.2f);
                    Quaternion rotation = Quaternion.Euler(0, curScreenPoint.x * 1, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    Quaternion resetRotation = Quaternion.Euler(0, 0, 0);
                    transform.rotation = resetRotation;
                }
                #endregion              
                #region Ice Cream State


              
                else if (Input.GetMouseButtonDown(0) && GameManager.currentState == 1)
                {
                    panels[1].transform.GetChild(2).gameObject.SetActive(false);
                    MMVibrationManager.Haptic(HapticTypes.SoftImpact);

                }
                
                #endregion

                // To Do Not Exit The Road
                else if (transform.position.x > 4.5f)
                {
                    Shaker.ShakeAll(crashShake);
                    transform.position = new Vector3(4.49f, transform.position.y, transform.position.z);
                }
                else if (transform.position.x < -4.5f)
                {
                    Shaker.ShakeAll(crashShake);
                    transform.position = new Vector3(-4.49f, transform.position.y, transform.position.z);
                }
            }

            #region Chaos State
            else if (GameManager.currentState == 3 && !dontMove && transform.position.x > -9.75f && transform.position.x < 5.6f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    panels[3].SetActive(false);
                    MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                }

                if (Input.GetMouseButton(0))
                {
                    screenPoint = Camera.main.WorldToScreenPoint(transform.position);
                    Vector3 curScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
                    transform.position = Vector3.Lerp(transform.position, new Vector3(curScreenPoint.x, transform.position.y, transform.position.z), 0.2f);
                    Quaternion rotation = Quaternion.Euler(0, curScreenPoint.x * 1, 0);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    Quaternion resetRotation = Quaternion.Euler(0, 0, 0);
                    transform.rotation = resetRotation;
                }
            }

            // To Do Not Exit The Road
            else if (transform.position.x > 5.6f && GameManager.currentState == 3)
            {
                Shaker.ShakeAll(crashShake);
                transform.position = new Vector3(5.59f, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < -9.75f && GameManager.currentState == 3)
            {
                Shaker.ShakeAll(crashShake);
                transform.position = new Vector3(-9.74f, transform.position.y, transform.position.z);
            }
            #endregion
        }
        else if (GameManager.currentState == 2) // Garbage State
        {
            if (Input.GetMouseButtonDown(0))
            {
                panels[2].SetActive(false);
                MMVibrationManager.Haptic(HapticTypes.SoftImpact);
            }
        }
    }
    public void RightButton() //Ice Cream Spawning Ice Creams In Left
    {
        Vector3 spawnPos = new Vector3(5, transform.position.y + 5, transform.position.z + 15);
        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        Instantiate(iceCream, spawnPos, Quaternion.identity);

    }
    public void LeftButton() //Ice Cream Spawning Ice Creams In Right
    {
        Vector3 spawnPos = new Vector3(-5, transform.position.y + 5, transform.position.z + 15);
        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        Instantiate(iceCream, spawnPos, Quaternion.identity);
    }

    private void ChecktheState() // 0 = Police | 1 = Ice Cream | 2 = Garbage | 3 = Chaos
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(GameManager.currentState).gameObject.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        Quaternion resetRotation = Quaternion.Euler(0, 0, 0);

        if (other.gameObject.CompareTag("Finish"))
        {
            enteredGate = 0;
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            panels[3].SetActive(false);
            GameManager.Win();
        }
        if (other.gameObject.CompareTag("IceCreamLaneTrigger"))
        {
            if (enteredGate < 1)
            {
                onIceCreamSelect = true;
                IceCreamChangeLanePanel.SetActive(true);
            }
            panels[1].SetActive(false);
        }
        if (other.gameObject.CompareTag("GarbageGateTrigger"))
        {
            dontMove = true;
            onIceCreamSelect = false;
            IceCreamChangeLanePanel.SetActive(false);
            GameManager.currentState = 2;
            StateChange(GameManager.currentState);
            panels[2].SetActive(true);
            transform.rotation = resetRotation;
            StartCoroutine(WaitForInput());
            rb.interpolation = RigidbodyInterpolation.None;
            transform.position = Vector3.zero;
            StartCoroutine(VehicleChange());
        }
        if (other.gameObject.CompareTag("IceCreamGateTrigger"))
        {
            dontMove = true;
            onIceCreamSelect = false;
            IceCreamChangeLanePanel.SetActive(false);
            GameManager.currentState = 1;
            StateChange(GameManager.currentState);
            panels[1].SetActive(true);
            transform.rotation = resetRotation;
            StartCoroutine(WaitForInput());
            AudioSource.PlayClipAtPoint(stateStartSounds[1], transform.position);
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            transform.position = Vector3.zero;
            StartCoroutine(VehicleChange());
        }
        if (other.gameObject.CompareTag("PoliceGateTrigger"))
        {
            dontMove = true;
            onIceCreamSelect = false;
            IceCreamChangeLanePanel.SetActive(false);
            GameManager.currentState = 0;
            StateChange(GameManager.currentState);
            panels[0].SetActive(true);
            transform.rotation = resetRotation;
            StartCoroutine(WaitForInput());
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            transform.position = Vector3.zero;
            StartCoroutine(VehicleChange());
        }
        if (other.gameObject.CompareTag("ChaosGateTrigger"))
        {
            dontMove = true;
            onIceCreamSelect = false;
            IceCreamChangeLanePanel.SetActive(false);
            GameManager.currentState = 3;
            StateChange(GameManager.currentState);
            panels[3].SetActive(true);
            transform.rotation = resetRotation;
            StartCoroutine(WaitForInput());
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            transform.position = Vector3.zero;
            StartCoroutine(VehicleChange());
        }
    }

    private void StateChange(int i)
    {
        states.transform.GetChild(0).gameObject.SetActive(false);
        states.transform.GetChild(1).gameObject.SetActive(false);
        states.transform.GetChild(2).gameObject.SetActive(false);
        states.transform.GetChild(3).gameObject.SetActive(false);
        states.transform.GetChild(i).gameObject.SetActive(true);
        panels[0].SetActive(false);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
        panels[3].SetActive(false);
    }


    IEnumerator WaitForInput()
    {
        rb.drag = 10000;
        enteredGate++;
        playerSpeed = 700;
        onStateChange = true;
        GameManager.isGameOver = true;
        yield return new WaitForSeconds(2);
        if (GameManager.currentState == 0)
        {
            AudioSource.PlayClipAtPoint(stateStartSounds[0], transform.position);
        }
        else if (GameManager.currentState == 1)
        {
            AudioSource.PlayClipAtPoint(stateStartSounds[1], transform.position);
        }
        states.transform.GetChild(0).gameObject.SetActive(false);
        states.transform.GetChild(1).gameObject.SetActive(false);
        states.transform.GetChild(2).gameObject.SetActive(false);
        states.transform.GetChild(3).gameObject.SetActive(false);
        GameManager.isGameOver = false;
        onStateChange = false;
        Instantiate(spawnableStates[GameManager.currentState], Vector3.zero, Quaternion.identity);
        dontMove = false;
        rb.drag = 0;
    }
    IEnumerator VehicleChange()
    {
        yield return new WaitForSeconds(.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(GameManager.currentState).gameObject.SetActive(true);
        if (enteredGate > 0 && GameManager.currentState != 3)
        {
            Instantiate(finishGate, new Vector3(0, 0, transform.position.z + 460), Quaternion.identity);
        }
    }


}
