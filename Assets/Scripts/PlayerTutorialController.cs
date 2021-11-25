using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTutorialController : MonoBehaviour
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
    private int spaceCounter=0;
    public static int hitCube=0;
    public static int numOfCubes=0;

    private GameObject textMove;
    private GameObject textShoot;
    private GameObject textGoodJob;
    private GameObject textShootEnemies;
    private GameObject textPortal;
    private GameObject teleporters;
    private GameObject targetCube;
    private GameObject cube1;
    private GameObject cube2;
    private GameObject cube3;
    private GameObject cube4;
    private GameObject textMultipleEnemies;
    private GameObject textWelcome;
    private GameObject textFinish;
    private GameObject powerup1;
    private GameObject powerup2;
    private GameObject powerup3;
    private GameObject powerup4;
    private GameObject textPowerups;
    private GameObject textInvisible;
    private GameObject textSpeed;
    private GameObject textAirStrike;
    private GameObject textShield;

    private AudioSource audioSource;
    private AudioSource audioPortal;




    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioPortal=GameObject.FindGameObjectWithTag("Teleporters").GetComponent<AudioSource>();

        if(audioSource != null){
            audioSource.Play();
        }
         GameObject.FindGameObjectWithTag("Zone2").GetComponent<Collider>().enabled = false;
         GameObject.FindGameObjectWithTag("Zone2").GetComponent<Renderer>().enabled = false;

         GameObject.FindGameObjectWithTag("Zone3").GetComponent<Collider>().enabled = false;
         GameObject.FindGameObjectWithTag("Zone3").GetComponent<Renderer>().enabled = false;

        GameObject.FindGameObjectWithTag("Zone4").GetComponent<Collider>().enabled = false;
         GameObject.FindGameObjectWithTag("Zone4").GetComponent<Renderer>().enabled = false;



         textMove = GameObject.FindGameObjectWithTag("TextMove");
         textShoot = GameObject.FindGameObjectWithTag("TextShoot");
         textGoodJob = GameObject.FindGameObjectWithTag("TextGoodJob");
         textShootEnemies = GameObject.FindGameObjectWithTag("TextShootEnemies");
         textPortal = GameObject.FindGameObjectWithTag("TextPortal");
        teleporters = GameObject.FindGameObjectWithTag("Teleporters");
        targetCube = GameObject.FindGameObjectWithTag("TargetCube");
        cube1 = GameObject.FindGameObjectWithTag("FragileCube");
        cube2 = GameObject.FindGameObjectWithTag("FragileCube1");
        cube3 = GameObject.FindGameObjectWithTag("FragileCube2");
        cube4 = GameObject.FindGameObjectWithTag("FragileCube3");
        textMultipleEnemies = GameObject.FindGameObjectWithTag("TextMultipleEnemies");
        textWelcome = GameObject.FindGameObjectWithTag("TextWelcome");
        textFinish = GameObject.FindGameObjectWithTag("TextFinish");
        textPowerups = GameObject.FindGameObjectWithTag("TextPowerups");
        textInvisible = GameObject.FindGameObjectWithTag("TextInvisible");
        textSpeed = GameObject.FindGameObjectWithTag("TextSpeed");
        textAirStrike = GameObject.FindGameObjectWithTag("TextAirStrike");
        textShield = GameObject.FindGameObjectWithTag("TextShield");
        powerup1 = GameObject.FindGameObjectWithTag("Powerup1");
        powerup2 = GameObject.FindGameObjectWithTag("Powerup2");
        powerup3 = GameObject.FindGameObjectWithTag("Powerup3");
        powerup4 = GameObject.FindGameObjectWithTag("Powerup4");

        


        targetCube.SetActive(false);         
         teleporters.SetActive(false);


         textMove.SetActive(false);
         textShoot.SetActive(false);
         textGoodJob.SetActive(false);
         textShootEnemies.SetActive(false);
         textPortal.SetActive(false);
         textMultipleEnemies.SetActive(false);
         textWelcome.SetActive(false);
         textFinish.SetActive(false);
         textPowerups.SetActive(false);
         textInvisible.SetActive(false);
         textSpeed.SetActive(false);
         textAirStrike.SetActive(false);
         textShield.SetActive(false);

         powerup1.SetActive(false);
         powerup2.SetActive(false);
         powerup3.SetActive(false);
         powerup4.SetActive(false);

         
         cube1.SetActive(false);
         cube2.SetActive(false);
         cube3.SetActive(false);
         cube4.SetActive(false);

         StartCoroutine(Text());

         IEnumerator Text()  //  <-  its a standalone method
        {
            yield return new WaitForSeconds(0.5f);
            textWelcome.SetActive(true);
            yield return new WaitForSeconds(4);
            textWelcome.SetActive(false);
            textMove.SetActive(true);
        }
        
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



        Scene currentScene = SceneManager.GetActiveScene();
        string SceneName = currentScene.name;
        Debug.Log(SceneName);
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

        if(Input.GetKeyDown(KeyCode.Space)&& textShoot.active==true && spaceCounter<5){
            spaceCounter++;
            Debug.Log(spaceCounter);
        }

        if(spaceCounter==5 && textShoot.active==true){
            disableSpace();
            spaceCounter++;
        }

        if(hitCube==5){
            hitCube++;
                targetCube.SetActive(false);
                textShootEnemies.SetActive(false);

                 StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(0.5f);
                textGoodJob.SetActive(true);
                yield return new WaitForSeconds(3);
                textGoodJob.SetActive(false);
                yield return new WaitForSeconds(3);
                textMultipleEnemies.SetActive(true);

                cube1.SetActive(true);
                cube2.SetActive(true);
                cube3.SetActive(true);
                cube4.SetActive(true);
            }
            }
        if(numOfCubes==4){
            numOfCubes++;
            textMultipleEnemies.SetActive(false);
            cube1.SetActive(false);
                cube2.SetActive(false);
                cube3.SetActive(false);
                cube4.SetActive(false);

                StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(0.5f);
                textGoodJob.SetActive(true);
                yield return new WaitForSeconds(3);
                textGoodJob.SetActive(false);
                yield return new WaitForSeconds(3);
                textPowerups.SetActive(true);
                yield return new WaitForSeconds(2);
                powerup1.SetActive(true);


                
            }
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

    private void disableSpace (){
       
       textShoot.SetActive(false);

        
                StartCoroutine(Text1());

            IEnumerator Text1()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(0.5f);
                textGoodJob.SetActive(true);
                yield return new WaitForSeconds(3);
                textGoodJob.SetActive(false);
                yield return new WaitForSeconds(1);
                textPortal.SetActive(true);  
                teleporters.SetActive(true);
                          }
            
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Zone1")){
            GameObject.FindGameObjectWithTag("Zone2").GetComponent<Collider>().enabled = true;
            GameObject.FindGameObjectWithTag("Zone2").GetComponent<Renderer>().enabled = true;

            GameObject.FindGameObjectWithTag("Zone1").GetComponent<Collider>().enabled = false;
            GameObject.FindGameObjectWithTag("Zone1").GetComponent<Renderer>().enabled = false;

            GameObject.FindGameObjectWithTag("Zone1").SetActive(false);
        }
        if(other.gameObject.CompareTag("Zone2")){
            GameObject.FindGameObjectWithTag("Zone2").GetComponent<Collider>().enabled = false;
            GameObject.FindGameObjectWithTag("Zone2").GetComponent<Renderer>().enabled = false;

            GameObject.FindGameObjectWithTag("Zone3").GetComponent<Collider>().enabled = true;
            GameObject.FindGameObjectWithTag("Zone3").GetComponent<Renderer>().enabled = true;
           
        }
        if(other.gameObject.CompareTag("Zone3")){
            GameObject.FindGameObjectWithTag("Zone3").GetComponent<Collider>().enabled = false;
            GameObject.FindGameObjectWithTag("Zone3").GetComponent<Renderer>().enabled = false;

            GameObject.FindGameObjectWithTag("Zone4").GetComponent<Collider>().enabled = true;
            GameObject.FindGameObjectWithTag("Zone4").GetComponent<Renderer>().enabled = true;
           
        }
        if(other.gameObject.CompareTag("Zone4")){
            GameObject.FindGameObjectWithTag("Zone4").GetComponent<Collider>().enabled = false;
            GameObject.FindGameObjectWithTag("Zone4").GetComponent<Renderer>().enabled = false;

            //GameObject.FindGameObjectWithTag("TextMove").enabled = false;
            textMove.SetActive (false);

            StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(0.5f);
                textGoodJob.SetActive(true);
                yield return new WaitForSeconds(3);
                textGoodJob.SetActive(false);
                yield return new WaitForSeconds(1);
                textShoot.SetActive(true);

            }

            
           
        }

        


        




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

            if(textPortal.active==true){
                textPortal.SetActive(false);

                StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(0.5f);
                textGoodJob.SetActive(true);
                yield return new WaitForSeconds(3);
                textGoodJob.SetActive(false);
                yield return new WaitForSeconds(1);
                textShootEnemies.SetActive(true);
                targetCube.SetActive(true);
            }
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

             if(textPortal.active==true){
                textPortal.SetActive(false);

                StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(0.5f);
                textGoodJob.SetActive(true);
                yield return new WaitForSeconds(3);
                textGoodJob.SetActive(false);
                yield return new WaitForSeconds(1);
                textShootEnemies.SetActive(true);
                targetCube.SetActive(true);
            }
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

             if(textPortal.active==true){
                textPortal.SetActive(false);

                StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(0.5f);
                textGoodJob.SetActive(true);
                yield return new WaitForSeconds(3);
                textGoodJob.SetActive(false);
                yield return new WaitForSeconds(1);
                textShootEnemies.SetActive(true);
                targetCube.SetActive(true);
            }
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

             if(textPortal.active==true){
                textPortal.SetActive(false);

                StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(0.5f);
                textGoodJob.SetActive(true);
                yield return new WaitForSeconds(3);
                textGoodJob.SetActive(false);
                yield return new WaitForSeconds(1);
                textShootEnemies.SetActive(true);
                targetCube.SetActive(true);
            }
            }  
        }

        

        if(other.gameObject.CompareTag("Powerup1"))
        {
            textPowerups.SetActive(false);
            textInvisible.SetActive(true);
            powerup1.SetActive(false);

            
                invisible = true;

                for (int i = 0; i < 15; i++) {
                    
                    objToFind = transform.GetChild(i).gameObject;
                    objToFind.SetActive(false);  
                    
                }  
            

             StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(6f);
                powerup2.SetActive(true);
                textInvisible.SetActive(false);
                
                
            }
            

            powerupTime = 0f;
        }

        if(other.gameObject.CompareTag("Powerup2"))
        {

            
            textSpeed.SetActive(true);
            powerup2.SetActive(false);
           
                speed = 1.5f * speed;     
           
           StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(6f);
                powerup3.SetActive(true);
                textSpeed.SetActive(false);
                
                
            }

            powerupTime = 0f;
        }

        if(other.gameObject.CompareTag("Powerup3"))
        {

            textAirStrike.SetActive(true);
            powerup3.SetActive(false);
            
                activeWeapon = radar;
                activateWeapon(activeWeapon);
                deactivateWeapon(cannon);
                shield.SetActive(true);
                speed = 0.25f * speed;
                target.SetActive(true);


             StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(6f);
                powerup4.SetActive(true);
                textAirStrike.SetActive(false);
                
                
            }
          
            powerupTime = 0f;
        }

        if(other.gameObject.CompareTag("Powerup4"))
        {
            textShield.SetActive(true);
            powerup4.SetActive(false);
            
                shield.SetActive(true);  
            
             StartCoroutine(Text());

            IEnumerator Text()  //  <-  its a standalone method
            {
                yield return new WaitForSeconds(6f);
                textShield.SetActive(false);
                yield return new WaitForSeconds(1);
                textGoodJob.SetActive(true);
                yield return new WaitForSeconds(3);
                textGoodJob.SetActive(false);
                yield return new WaitForSeconds(1);
                textFinish.SetActive(true);
                yield return new WaitForSeconds(4);
                LoadMenu();

                
                
            }

            powerupTime = 0f;
        }
    }

    public static void IncreaseHitCube(){
        hitCube++;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu Scene");
    }
}
