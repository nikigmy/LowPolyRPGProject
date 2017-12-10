using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBallMovement : MonoBehaviour
{
    public float FlySpeed;
    public GameObject target = null;
    public GameObject ExplosionParticle;
    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            var vector = Quaternion.Slerp(transform.rotation, target.transform.rotation, 1);
            transform.Translate(vector.eulerAngles * Time.deltaTime * FlySpeed, Space.World);
        }
        else
        {
            transform.Translate(transform.forward * Time.deltaTime * FlySpeed, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Debug.Log(transform.position);
            var explosionParticle = Instantiate(ExplosionParticle, transform.position, Quaternion.identity);
            Destroy(explosionParticle, 1);
            Destroy(gameObject);
        }
    }
}
