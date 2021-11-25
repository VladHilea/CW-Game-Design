using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShooting : MonoBehaviour
{
    private string fireName;
    private string player;
    public Rigidbody projectile;
    public Transform shootingPosition;
    public float projectileSpeed;
    private int shots;
    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        shots = 3;
        cooldown = 0f;

        if(gameObject.CompareTag("P1Target")) {
            fireName = "Fire1";
            player = "P1";
        }

        else if(gameObject.CompareTag("P2Target")) {
            fireName = "Fire2";
            player = "P2";
        } 
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;

        if(cooldown >= 1f) {
            cooldown = 0f;

            if(shots < 3)
                shots++;
        }

        if(Input.GetKeyDown(KeyCode.Space))
            if(player == "P1" && Time.timeScale == 1f && shots > 0) {
                Fire();
            }

        if(Input.GetKeyDown(KeyCode.Return))
            if(player == "P2" && Time.timeScale == 1f && shots > 0) {
                Fire();
            }
    }

    void Fire() {
        
        Rigidbody projectileInstance = Instantiate(projectile, shootingPosition.position, shootingPosition.rotation) as Rigidbody;
        projectileInstance.velocity = projectileSpeed * shootingPosition.forward;
        shots--;
    }
}
