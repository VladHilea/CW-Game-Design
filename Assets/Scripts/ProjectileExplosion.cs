using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileExplosion : MonoBehaviour
{
    public LayerMask layer;
    public int minDamage;
    public int maxDamage;
    public float radius;

    private int hitCube=0;
    private GameObject targetCube;
    private GameObject textShootEnemies;
    private GameObject textGoodJob;

    string SceneName;

    

    private void Start()
    {

        targetCube = GameObject.FindGameObjectWithTag("TargetCube");
    textShootEnemies = GameObject.FindGameObjectWithTag("TextShootEnemies");
    textGoodJob = GameObject.FindGameObjectWithTag("TextGoodJob");
        Destroy(gameObject, 2f);

        Scene currentScene = SceneManager.GetActiveScene();
        SceneName = currentScene.name;
    }

    private void OnTriggerEnter(Collider other) {
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layer);

        for(int i=0; i<colliders.Length; i++) {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if(!targetRigidbody)
                continue;

            PlayerHealth targetHealth = targetRigidbody.GetComponent<PlayerHealth>();

            if(!targetHealth)
                continue;

            int damage = Random.Range(minDamage, maxDamage + 1);
            targetHealth.TakeDamage(damage);
        }

        if(SceneName.Equals("Tutorial Scene")){
            Debug.Log("acasca");

        if(other.gameObject.CompareTag("TargetCube")){
            PlayerTutorialController.IncreaseHitCube();
            
        }

        if(other.gameObject.CompareTag("FragileCube")){
            other.gameObject.SetActive(false);
            PlayerTutorialController.numOfCubes++;
        }
        if(other.gameObject.CompareTag("FragileCube1")){
            other.gameObject.SetActive(false);
            PlayerTutorialController.numOfCubes++;
        }
        if(other.gameObject.CompareTag("FragileCube2")){
            other.gameObject.SetActive(false);
            PlayerTutorialController.numOfCubes++;
        }
        if(other.gameObject.CompareTag("FragileCube3")){
            other.gameObject.SetActive(false);
            PlayerTutorialController.numOfCubes++;
        }
        }

        /*m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();*/

        //Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        Debug.Log(other.GetComponent<Collider>().name + " hit");
        Destroy(gameObject);

    }
}
