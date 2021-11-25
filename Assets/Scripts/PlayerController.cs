using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;

public class PlayerController : MonoBehaviour
{

    public float speed;
    private float SPEED;
    private Rigidbody rb;
    private GameObject obj;
    private GameObject objToFind;
    private GameObject trail;
    public GameObject radar;
    public GameObject cannon;
    public GameObject shield;
    public GameObject target;
    private GameObject activeWeapon;
    public float TurnSpeed = 180f;                 
    private float MovementInputValue;    
    private float TurnInputValue;
    private string TurnAxisName;
    private string MovementAxisName;
    private float powerupTime;
    private bool invisible;
    private string ownShield;
    private AudioSource audioSource;
    private AudioSource audioPortal;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null){
            audioSource.Play();
        }
        audioPortal=GameObject.FindGameObjectWithTag("Teleporters").GetComponent<AudioSource>();
        if(gameObject.CompareTag("P1")) {
            MovementAxisName = "Vertical1";
            TurnAxisName = "Horizontal1";
            ownShield = "P1Shield";
        }

        else if(gameObject.CompareTag("P2")) {
            MovementAxisName = "Vertical2";
            TurnAxisName = "Horizontal2";
            ownShield = "P2Shield";
        }  

        rb = GetComponent<Rigidbody>();
        powerupTime = 0f;
        invisible = false;
        speed = 7;
        SPEED = 7;
        radar.SetActive(false);
        shield.SetActive(false);
        target.SetActive(false);
        activeWeapon = cannon;
    }

    void Update() {
        MovementInputValue = Input.GetAxis(MovementAxisName);
        TurnInputValue = Input.GetAxis(TurnAxisName);
        powerupTime += Time.deltaTime;

        if(invisible && powerupTime >= 5f) {
            for (int i = 0; i < 13; i++) {
                objToFind = transform.GetChild(i).gameObject;
                objToFind.SetActive(true);  
            }

            activateWeapon(activeWeapon);

            invisible = false;
            //trail.SetActive(false);
        }

        if(powerupTime >= 5f) {
            speed = SPEED;
        }

        if(activeWeapon == radar && powerupTime >= 10f) {
            activeWeapon = cannon;
            activateWeapon(activeWeapon);
            deactivateWeapon(radar);
            shield.SetActive(false);
            speed = SPEED;
            target.SetActive(false);
            target.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if(powerupTime >= 5f && shield.activeSelf == true && activeWeapon != radar) {
            shield.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        Move();
        Turn();

        rb.velocity = new Vector3(0, 0, 0);
        //this line below stops the mechines from twisting upon hitting an obstacle
        rb.angularVelocity = new Vector3(0, 0, 0);
    }

    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = transform.forward * MovementInputValue * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement); 
    }


    private void Turn()
    {
        float turn = TurnInputValue * TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void activateWeapon(GameObject weapon) {
        weapon.SetActive(true);
    }

    private void deactivateWeapon(GameObject weapon) {
        weapon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BlueTele0"))
        {
            Vector3 newPosition = new Vector3();
            newPosition = GameObject.FindGameObjectWithTag("BlueTele1").gameObject.transform.position;
            newPosition.x = newPosition.x + 2f;
            newPosition.y = 0.7f;
            transform.position = newPosition;
            Collider a = GameObject.FindGameObjectWithTag("BlueTele1").GetComponent<Collider>();
            audioPortal.Play();

            StartCoroutine(Text1());

            IEnumerator Text1()  //  <-  its a standalone method
            {
                a.enabled=false;

                yield return new WaitForSeconds(4);
                a.enabled=true;
                
                          }
        }

        if(other.gameObject.CompareTag("BlueTele1"))
        {
            Vector3 newPosition = new Vector3();
            newPosition = GameObject.FindGameObjectWithTag("BlueTele0").gameObject.transform.position;
            newPosition.x = newPosition.x - 2f;
            newPosition.y = 0.7f;
            transform.position = newPosition;
            Collider a = GameObject.FindGameObjectWithTag("BlueTele0").GetComponent<Collider>();  
            audioPortal.Play();
            StartCoroutine(Text1());

            IEnumerator Text1()  //  <-  its a standalone method
            {
                a.enabled=false;

                yield return new WaitForSeconds(4);
                a.enabled=true;
                
                          }  
        }

        if(other.gameObject.CompareTag("BlueTele2"))
        {
            Vector3 newPosition = new Vector3();
            newPosition = GameObject.FindGameObjectWithTag("BlueTele3").gameObject.transform.position;
            newPosition.z = newPosition.z + 2f;
            newPosition.y = 0.7f;
            transform.position = newPosition;
            Collider a = GameObject.FindGameObjectWithTag("BlueTele3").GetComponent<Collider>();
            audioPortal.Play();
            StartCoroutine(Text1());

            IEnumerator Text1()  //  <-  its a standalone method
            {
                a.enabled=false;

                yield return new WaitForSeconds(4);
                a.enabled=true;
                
                          }
        }

        if(other.gameObject.CompareTag("BlueTele3"))
        {
            Vector3 newPosition = new Vector3();
            newPosition = GameObject.FindGameObjectWithTag("BlueTele2").gameObject.transform.position;
            newPosition.z = newPosition.z - 2f;
            newPosition.y = 0.7f;
            transform.position = newPosition;
            Collider a = GameObject.FindGameObjectWithTag("BlueTele2").GetComponent<Collider>();  
            audioPortal.Play();  
            StartCoroutine(Text1());

            IEnumerator Text1()  //  <-  its a standalone method
            {
                a.enabled=false;

                yield return new WaitForSeconds(4);
                a.enabled=true;
                
                          }
        }

        if(other.gameObject.CompareTag("Powerup1") || other.gameObject.CompareTag("Powerup2"))
        {
            int num = Random.Range(0,4);
            Debug.Log(num);
            //the line below will fully deactivate the machines (they won't move anymore)
            //gameObject.SetActive(false);

            if(num == 0) {
                invisible = true;

                for (int i = 0; i < 15; i++) {
                    objToFind = transform.GetChild(i).gameObject;
                    objToFind.SetActive(false);  
                }  
            }

            else if(num == 1) {
                speed = 1.5f * speed;     
            }

            else if(num == 2) {
                activeWeapon = radar;
                activateWeapon(activeWeapon);
                deactivateWeapon(cannon);
                shield.SetActive(true);
                speed = 0.25f * speed;
                target.SetActive(true);
            }

            else if(num == 3) {
                shield.SetActive(true);  
            }

            powerupTime = 0f;
        }
    }
}
