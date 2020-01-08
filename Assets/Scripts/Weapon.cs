using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{/*
    [Header("Properties")]
    public bool WantAutomatic = false;   

    [Space(6)]
    [Header("Granade")]
    public bool IsGranade = false;
    public float GranadeTime = 0;
    public float ForceRight, ForceUp;
    public PhysicsMaterial2D mat;

    [HideInInspector]
    public bool first = true;
    [HideInInspector]
    public Movement pj;

    [Space(6)]
    [Header("Ammo"), Multiline(3)]
    [Range(0, 300)]
    public int MaxAmmo;
    [Range(0, 300)]
    public int Ammo;
    [Range(1, 10)]
    public int AmmoPerShot = 1;    

    [Space(6)]
    [Header("Trajectory")]
    [Range(0, 3)]
    public float ProjectileSpread = 0;
    [Range(0, 100)]
    public float Range = 10;


    [Space(6)]
    [Header("Line")]
    public float LineLifeTime = 0.02f;
    public bool LifeTimeCanBigger = false;
  

    [Header("Get Line")]
    public GameObject Line;

    [Space(6)]
    [Header("Timing and effects")]
    public float knockback = 1.5f;
    [Range(0, 10)]
    public float TimeBetweenShots;
    [Range(10, 30)]
    public int TimeForDespawn = 30;
    public int Push = 50;
    public GameObject ImpactEffect;
    //public shot effect

    [Space(6)]
    [Header("Transforms")]
    public Transform FirePoint;

    //Function
    [HideInInspector]
    public int multiplier;

    private float Recovery, recoverytimedespawn;

    [HideInInspector]
    public string objectTag = "Player 1";
    private string StrFire = "Fire";

    
    public bool IsGrabbed = false, onStand = false;
    private bool Facing = true;

    [HideInInspector]
    public Transform weaponslot;

    [HideInInspector]
    public Vector3 standPos;
    private FollowCharacter cam;
    public CharacterController2D control;
    private Rigidbody2D Gravity;
    private LayerMask Whatissolid;
    private LayerMask WhatissolidGranade;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        Gravity = GetComponent<Rigidbody2D>();        
    }

    private void Start()
    {
        cam = Camera.main.GetComponent<FollowCharacter>();
        first = true;
        recoverytimedespawn = TimeForDespawn;

        WhatissolidGranade = LayerMask.GetMask("Player");
        Whatissolid = LayerMask.GetMask("Player", "Platform");
        lineRenderer = Line.GetComponent<LineRenderer>();
        if (!LifeTimeCanBigger)
        {
            if (LineLifeTime > TimeBetweenShots)
            {
                LineLifeTime = TimeBetweenShots;
            }
        }

        Ammo = MaxAmmo;
    }
    
    private void Update()
    {
        if (IsGrabbed)
        {
            transform.position = weaponslot.position;
        }
        if (IsGrabbed)
        {
            if (control.m_FacingRight)
            {
                multiplier = 1;
            }
            else
            {
                multiplier = -1;
            }
        }
    }

    void FixedUpdate()
    {
        if (objectTag == "Player 2")
        {
            StrFire = "Fire 2";
        }
        else
        {
            StrFire = "Fire";
        }

        if (onStand)
        {
            transform.position = standPos;
            transform.rotation = Quaternion.identity;
            Gravity.gravityScale = 0;
            recoverytimedespawn = TimeForDespawn;
        }

        if (IsGrabbed)
        {
            recoverytimedespawn = TimeForDespawn;
            onStand = false;
            
            transform.rotation = new Quaternion(0, 0, 0, 0);
            Gravity.gravityScale = 0;

            if (Recovery <= 0)
            {
                if (WantAutomatic)
                {
                    if (Input.GetButton(StrFire))
                    {
                        shoot();

                    }
                    if (Input.GetButtonUp(StrFire))
                    {
                        shoot();
                    }
                }
                else
                {
                    if (Input.GetButtonDown(StrFire))
                    {
                        shoot();
                    }
                }
            }
            else
            {
                Recovery -= Time.deltaTime;
            }            
        }
        else
        {
            if (!onStand)
            {
                recoverytimedespawn -= Time.deltaTime;
                Gravity.gravityScale = 2.01f;
            }
            Recovery = 0;
            if (recoverytimedespawn <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Flip()
    {
        Facing = !Facing;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    public void Throw(float AForceRight, float AForceUp)
    {
        if (!IsGranade)
        {
            Gravity.velocity = new Vector2(AForceRight, AForceUp);
        }
    }

    IEnumerator Shoot()
    {        
        if (Ammo > 0)
        {
            //(new Vector2(knockback, 0.1f));        

            Ammo--;
            if (!IsGranade)
            {
                Vector2 vel = control.GetComponent<Rigidbody2D>().velocity;
                vel.x -= knockback * multiplier;
                vel.y -= 0.1f * multiplier;
                control.GetComponent<Rigidbody2D>().velocity = vel;

                float random = Random.Range(-ProjectileSpread, ProjectileSpread);
                Vector2 direction = GetDirectionVector2D(random);

                RaycastHit2D hitInfo = Physics2D.Raycast(FirePoint.position, direction, multiplier * Range, Whatissolid);
                if (ImpactEffect != null)
                {
                    Instantiate(ImpactEffect, FirePoint.position, Quaternion.identity);
                }

                GameObject lineDrawPrefab = GameObject.Instantiate(Line) as GameObject;

                if (hitInfo)
                {
                    Movement enemy = hitInfo.transform.GetComponent<Movement>();
                    if (enemy != null && !enemy.CompareTag(objectTag))
                    {
                        cam.TryShake(0.1f, 0.1f);
                        GameObject dummy = enemy.TakeDamage(true);
                        dummy.GetComponent<Rigidbody2D>().AddForce(new Vector2((Push * 10) * multiplier, Push * 5));
                    }

                    if (ImpactEffect != null)
                    {
                        Instantiate(ImpactEffect, hitInfo.point, Quaternion.identity);
                    }

                    lineRenderer.SetPosition(0, FirePoint.position);
                    lineRenderer.SetPosition(1, hitInfo.point);
                    lineRenderer.enabled = true;
                }
                else
                {
                    Vector3 pos = FirePoint.position;
                    pos.y = hitInfo.point.y + random;
                    pos.x = hitInfo.point.x + multiplier * Range;
                    lineRenderer.SetPosition(0, FirePoint.position);
                    lineRenderer.SetPosition(1, FirePoint.position + pos);
                    lineRenderer.enabled = true;
                }

                yield return new WaitForSeconds(LineLifeTime);
                Destroy(lineDrawPrefab);
                lineRenderer.enabled = false;
            }
            else {
                if (first)
                {
                    first = false;
                    yield return new WaitForSeconds(GranadeTime);                    
                    Instantiate(ImpactEffect, FirePoint.position, Quaternion.identity);
                    Collider2D[] enemys = Physics2D.OverlapCircleAll(FirePoint.position, Range, WhatissolidGranade);
                    cam.TryShake(0.1f, 0.1f);
                    for (int i = 0; i < enemys.Length; i++)
                    {
                        GameObject dummy = enemys[i].GetComponent<Movement>().TakeDamage(true);
                        dummy.GetComponent<Rigidbody2D>().AddForce(new Vector2((Push * 10) * multiplier, Push * 5));
                        
                    }
                    Destroy(gameObject);
                }
                else
                {
                    pj.DropWeapon(false);
                    Gravity.velocity = new Vector2(ForceRight * multiplier, ForceUp);
                    gameObject.GetComponent<Collider2D>().sharedMaterial = mat;
                }
            }
        }
        //else
        //{
        //Sin municion
        //}
    }
    

    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;
        if (IsGranade)
        {
            Gizmos.DrawWireSphere(transform.position, Range);
        }
        else
        {
            Vector3 gizm = FirePoint.position;
            gizm.x += Range;
            Gizmos.DrawLine(FirePoint.position, gizm);
            Vector3 x1 = gizm, x2 = gizm;
            x1.y += ProjectileSpread;
            x2.y -= ProjectileSpread;

            Gizmos.DrawLine(x1, x2);
        }

       
    }

    public void shoot()
    {        
        Recovery = TimeBetweenShots;
        for (int x = 0; x < AmmoPerShot; x++)
        {
            StartCoroutine(Shoot());
        }
    }*/
}