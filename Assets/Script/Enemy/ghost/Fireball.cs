using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Transform target;
    private void Start()
    {
        target = GameObject.Find("OVRPlayerController").transform;
    }
    private void Update()
    {
        transform.LookAt(target.position);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") || other.CompareTag("LongSword") || !other.CompareTag("fireball"))
        {
            Destroy(transform.root.gameObject);
        }
        if(other.gameObject.name == "OVRPlayerController")
        {
            PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
            GhostStatus ghostStatus = FindObjectOfType<GhostStatus>();
            // HP를 감소시키고 체크
            if (ghostStatus != null) { playerStatus.TakeDamage(ghostStatus.enemyDamage); }
        }
    }
}
