using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCollider : MonoBehaviour
{
    private float cooldown = 0.0f;
    private float deltaTime = 0.0f;
    public float cooldownTime;
    private float originalY;

    void Start() {
        originalY = transform.position.y;
    }

    void Update() {
        cooldown += Time.deltaTime;

        if(cooldown - deltaTime >= cooldownTime) {
            transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other) {

        if(!other.gameObject.CompareTag("Projectile")) {
            deltaTime = cooldown;
            transform.position = new Vector3(transform.position.x, -1f, transform.position.z);
        }  
    }
}
