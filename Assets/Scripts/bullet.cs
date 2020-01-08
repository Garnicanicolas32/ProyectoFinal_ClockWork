using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public float speed;
    public Rigidbody2D rb;
    public GameObject ImpactEffect;
    public Vector3 FirePoint;
    public string objectTag;
    public int multiplier;
    public int Push;
    public FollowCharacter cam;
    public float Range;
    public float ProjectileSpread;
    public bool first = true;
    public bool withRaycast = false;
    public GameObject sound;

    /*public bullet(GameObject Im, Transform Fp, string Ot, int M, int P, FollowCharacter C, float R, float Ps)
    {
        Range = R;
        ImpactEffect = Im;
        FirePoint = Fp.position;
        objectTag = Ot;
        multiplier = M;
        Push = P;
        cam = C;
        ProjectileSpread = Ps;
    }*/
    // Use this for initialization
    private void Awake()
    {
        Vector3 temporal = new Vector3(0, 0, 0);
        GameObject temp = Instantiate(sound, temporal, Quaternion.identity);
    }
    void Start () {     

        float random = Random.Range(-ProjectileSpread, ProjectileSpread);
        Vector2 direction = GetDirectionVector2D(random);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * multiplier * speed;
        first = true;
        FirePoint = transform.position;
    }

    public void Update()
    {
        if (withRaycast)
        { 
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, -multiplier * Range);
            if (hitInfo)
            {
                Movement enemy = hitInfo.transform.GetComponent<Movement>();
                if (enemy != null && !enemy.CompareTag(objectTag))
                {
                 cam.TryShake(0.1f, 0.1f);
                 enemy.TakeDamage(true, Push);
                }
            }
        }

        if (Vector2.Distance(FirePoint, transform.position) > Range)
        {
         Destroy(gameObject);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ImpactEffect != null)
        {
            Instantiate(ImpactEffect, FirePoint, Quaternion.identity);
        }

        Movement enemy = collision.GetComponent<Movement>();

        if (enemy != null && !enemy.CompareTag(objectTag))
        {
            cam.TryShake(0.1f, 0.1f);
            enemy.TakeDamage(true, Push);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (withRaycast)
        {
            Vector3 yikes = transform.position;
            yikes.x += Range * -multiplier;

            Gizmos.DrawLine(transform.position, yikes);
        }
    }

        public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }
}
