using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{

    public float timer = -10;
    public float range;
    public int fforce;
    public GameObject ImpactEffect;
    public LayerMask WhatissolidGranade;
    private Rigidbody2D Gravity;
    public float multiplier;
    public int which;
    private void Awake()
    {
        Gravity = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        WhatissolidGranade = LayerMask.GetMask("Player");
        Gravity = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer != -10)
        {
            if (timer <= 0)
            {
                Instantiate(ImpactEffect, transform.position, Quaternion.identity);
                Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, range, WhatissolidGranade);
                for (int i = 0; i < enemys.Length; i++)
                {
                    enemys[i].GetComponent<Movement>().TakeDamage(true, fforce);
                }
                Destroy(gameObject);
            }
            else { timer -= Time.deltaTime; }
        }
    }

    public void Throw()
    {       
        Gravity.velocity = new Vector2(fforce * multiplier, fforce * which*3);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
        


    
}