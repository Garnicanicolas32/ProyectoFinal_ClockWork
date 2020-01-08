using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int Healt;
    public float speed;

    public GameObject EfectoHit;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Healt <= 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int Healt)
    {
        Instantiate(EfectoHit, transform.position, Quaternion.identity);
        this.Healt -= Healt;
    }
}
