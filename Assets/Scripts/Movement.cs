using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController2D control;

    public LayerMask MaskWeapon;
    public enum Character {Mark,Platina};
    public Character MyCharacter;

    public GameObject dummy;
    public Transform TopTransform;
    public Transform BottomTransform;
    public Transform PosSlot;
    private Animator Anim;
    Rigidbody2D rb;

    private bool Grabbing = false;
    bool salto = false;
    private bool first = true;

    public int multiplier;

    private Weapon_2 last_weapon;

    public int Lives = 3;

    public float tilt;
    public float ForceThrowUp = 0.2f;
    public float ForceThrowRight = 0.7f;

    public float GrabRange;

    //public float fallmultiplier = 2.5f;
    public float lowjumpmultiplier = 2f;

    public float VelocidadMovimiento;
    public float MovimientoHorizontal;

    public Powers poderes;

    private string StrPower = "Hability";
    private string StrJump = "Jump";
    private string StrHorizontal = "Horizontal";
    private string StrGrab = "Grab/interact";
    private string StrHab = "Hability";

    private FollowCharacter cam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        first = true;
    }

    void Start()
    {
        first = true;
        cam = Camera.main.GetComponent<FollowCharacter>();
        if (gameObject.tag == "Player 2")
        {
            StrPower += " 2";
            StrHorizontal += " 2";
            StrJump += " 2";
            StrGrab += " 2";
            StrHab += " 2";
        }
        Anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        MovimientoHorizontal = Input.GetAxisRaw(StrHorizontal) * VelocidadMovimiento;
        if (Input.GetButtonDown(StrJump))
        {
            salto = true;
            Anim.SetTrigger("Salto");
        }

        if (control.m_FacingRight)
        {
            multiplier = 1;
        }
        else
        {
            multiplier = -1;
        }
        control.Move(MovimientoHorizontal * Time.fixedDeltaTime, false, salto);
        salto = false;


        /*
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallmultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {*/
        rb.velocity += Vector2.up * Physics2D.gravity.y * (lowjumpmultiplier - 1) * Time.fixedDeltaTime;
        // }
        if (Input.GetButtonDown(StrHab))
        {
            Debug.Log(multiplier);
            if (poderes != null)
            {
                poderes.multiplier = multiplier;

                switch (MyCharacter)
                {
                    case Character.Mark:
                        poderes.UseMark();
                        break;

                    case Character.Platina:
                        Weapon_2 pipo = poderes.UsePlatina();
                        if(pipo != null)
                        {
                            DropWeapon(true);
                            EquipWeapon(pipo);
                        }
                        break;
                }
            }
            
        }


        if (Input.GetButtonDown(StrGrab))
        {
            if (Grabbing == false)
            {
                last_weapon = null;
                Weapon_2 weapon;
                weapon = null;

                Collider2D[] weaponsTop = Physics2D.OverlapCircleAll(TopTransform.position, GrabRange, MaskWeapon);
                Collider2D[] weaponsBot = Physics2D.OverlapCircleAll(BottomTransform.position, GrabRange, MaskWeapon);

                int i;

                if (weaponsBot.Length != 0)
                {
                    i = Random.Range(0, weaponsBot.Length);
                    weapon = weaponsBot[i].GetComponent<Weapon_2>();
                }
                if (weaponsTop.Length != 0)
                {
                    i = Random.Range(0, weaponsTop.Length);
                    weapon = weaponsTop[i].GetComponent<Weapon_2>();
                }

                if (weapon != null)
                {
                    EquipWeapon(weapon);
                }

            }
            else
            {
                DropWeapon(true);
            }
        }
    }


    public void EquipWeapon(Weapon_2 weapon)
    {
        Vector3 Scale = weapon.transform.localScale;
        if (control.m_FacingRight)
        {
            if (Mathf.Sign(weapon.transform.localScale.x) < 0)
            { Scale.x *= -1; }
        }
        else
        {
            if (Mathf.Sign(weapon.transform.localScale.x) > 0)
            { Scale.x *= -1; }
        }

        weapon.pj = this;
        weapon.onStand = false;
        weapon.transform.localScale = Scale;
        weapon.control = control;
        control.weapon = weapon;
        Grabbing = true;
        weapon.IsGrabbed = true;
        weapon.weaponslot = PosSlot;
        weapon.objectTag = gameObject.tag;
        last_weapon = weapon;
    }

    public void DropWeapon(bool withDrop)
    {
        if (last_weapon != null)
        {
            last_weapon.control = null;
            control.weapon = null;
            Grabbing = false;
            last_weapon.IsGrabbed = false;
            if (withDrop)
            {
                last_weapon.Throw(ForceThrowRight * multiplier, ForceThrowUp);
            }
        }    
    }

    public void TakeDamage()
    {
        Debug.Log("AAA");
        //   Instantiate(EfectoHit, transform.position, Quaternion.identity);
        cam.TryShake(0.1f, 0.1f);
        Lives--;
        DropWeapon(false);
        //DEATH
        
        GameObject @object = Instantiate(dummy, transform.position, Quaternion.Euler(0,0,tilt * multiplier));
        @object.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        @object.GetComponent<Rigidbody2D>().AddForce(rb.velocity);
        if (Lives == 0)
        {
           
        }
        Destroy(gameObject);

        if (Lives > 0)
        {
            @object.GetComponent<autodestroy>().doIt = true;
            //still
        }
        else
        {
            @object.GetComponent<autodestroy>().doIt = false;
            //Lose
        }
    }

    public void TakeDamage(bool yes, int Push)
    {
        Debug.Log("BBB");
        //   Instantiate(EfectoHit, transform.position, Quaternion.identity);
        if (cam != null)
        {
            cam.TryShake(0.1f, 0.1f);
        }
        
        Lives--;
        DropWeapon(false);
        //DEATH
        if (first) {
            first = false;
            GameObject retorno = Instantiate(dummy, transform.position, Quaternion.Euler(0, 0, tilt * multiplier));
            dummy.GetComponent<Rigidbody2D>().AddForce(new Vector2((Push * 10) * multiplier, Push * 5));
            if (Lives > 0)
            {
                retorno.GetComponent<autodestroy>().doIt = true;
                //still
            }
            else
            {
                retorno.GetComponent<autodestroy>().doIt = false;
                //Lose
            }
        }        
        Destroy(gameObject);
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(TopTransform.position, GrabRange);
        Gizmos.DrawWireSphere(BottomTransform.position, GrabRange);
    }
}

