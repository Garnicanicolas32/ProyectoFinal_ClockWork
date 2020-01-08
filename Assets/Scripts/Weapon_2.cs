using System.Collections;
using UnityEngine;

public class Weapon_2 : MonoBehaviour
{
    [Header("Properties")]
    public bool WantAutomatic = false;
    public bool wantRaycast = false;
    public bool wantSeeDistance = false;
    public LineRenderer LineDistance;


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
    public GameObject bulletPrefab;
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
    //[Range(0, 100)]
    public float Range = 10;

    [Space(6)]
    [Header("Timing and effects")]
    public GameObject FloatingText;
    public float speed = 20f;
    public float knockback = 1.5f;
    [Range(0, 10)]
    public float TimeBetweenShots;
    [Range(10, 30)]
    public int TimeForDespawn = 30;
    public int Push = 50;
    public GameObject ImpactEffect;
    public GameObject EmptyEffect;
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
    public TextMesh AD;
    public int ad;
    public Vector3 standPos;
    private FollowCharacter cam;
    public CharacterController2D control;
    private Rigidbody2D Gravity;
    private LayerMask Whatissolid;
    private LayerMask WhatissolidGranade;
    private Vector3 scaleText;
    private void Awake()
    {
        if (FloatingText != null)
        {
            scaleText = FloatingText.transform.localScale;
            FloatingText.GetComponent<TextMesh>().text = MaxAmmo.ToString();
        }
        Gravity = GetComponent<Rigidbody2D>();        
    }

    private void Start()
    {
        
        if (FloatingText != null)
        {
            scaleText = FloatingText.transform.localScale;
            FloatingText.GetComponent<TextMesh>().text = MaxAmmo.ToString();
        }
        cam = Camera.main.GetComponent<FollowCharacter>();
        first = true;
        recoverytimedespawn = TimeForDespawn;

        WhatissolidGranade = LayerMask.GetMask("Player");
        Whatissolid = LayerMask.GetMask("Player", "Platform");

        Ammo = MaxAmmo;
    }
    
    private void Update()
    {
        if (wantSeeDistance)
        {
            LineDistance.enabled = false;
        }

        if (IsGrabbed)
        {
            transform.position = weaponslot.position;
            if (control.m_FacingRight)
            {
                if (FloatingText != null)
                {
                    FloatingText.transform.localScale = new Vector3(scaleText.x * 1, scaleText.y, scaleText.z);
                }
                
                multiplier = 1;
            }
            else
            {
                if (FloatingText != null)
                {
                    FloatingText.transform.localScale = new Vector3(scaleText.x * -1, scaleText.y, scaleText.z);
                }
                multiplier = -1;
            }

            
            if (wantSeeDistance)
            {
                LineDistance.enabled = true;
                Vector3 pos = FirePoint.position;
                pos.x += multiplier * Range;
                LineDistance.SetPosition(0, FirePoint.position);
                LineDistance.SetPosition(1, pos);
            }
            
        }

        
    }

    void FixedUpdate()
    {
        if (Ammo < 1)
        {
            if (wantSeeDistance)
            {
                wantSeeDistance = false;
                LineDistance.enabled = false;
            }
        }
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
            Vector3 ndeah = FirePoint.position;
            ndeah.z -= 50;

            Ammo--;
            if (FloatingText != null)
            {
                FloatingText.GetComponent<TextMesh>().text = Ammo.ToString();
            }
            if (!IsGranade)
            {
                if (cam != null)
                {
                    cam.TryShake(0.1f, 0.1f);
                }
                bullet Script = bulletPrefab.GetComponent<bullet>();
                Script.ImpactEffect = ImpactEffect; Script.objectTag = objectTag; Script.speed = speed; Script.multiplier = multiplier;
                Script.Push = Push; Script.cam = cam; Script.Range = Range; Script.ProjectileSpread = ProjectileSpread * 10;
                Script.withRaycast = wantRaycast;

                float random = Random.Range(-ProjectileSpread, ProjectileSpread);
                Quaternion quat = new Quaternion(0,0, random,0);
                Instantiate(bulletPrefab, FirePoint.position, quat);

                Vector2 vel = control.GetComponent<Rigidbody2D>().velocity;
                vel.x -= knockback * multiplier;
                vel.y -= 0.1f * multiplier;
                control.GetComponent<Rigidbody2D>().velocity = vel;

                if (ImpactEffect != null)
                {
                    Instantiate(ImpactEffect, ndeah, Quaternion.identity);
                }
            }
            else
            {
                if (first)
                {
                    first = false;
                    yield return new WaitForSeconds(GranadeTime);
                    if (ImpactEffect != null)
                    {
                        Instantiate(ImpactEffect, ndeah, Quaternion.identity);
                    }
                    Collider2D[] enemys = Physics2D.OverlapCircleAll(FirePoint.position, Range, WhatissolidGranade);
                    for (int i = 0; i < enemys.Length; i++)
                    {
                        enemys[i].GetComponent<Movement>().TakeDamage(true,Push);
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
        else
        {
            //Sin municion
            if (EmptyEffect != null)
            {
                Vector3 ndeah = FirePoint.position;
                //ndeah.z -= 50;
                Instantiate(EmptyEffect, ndeah, Quaternion.identity);
            }
            //EmptyEffect()
            
        }
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
    }
}