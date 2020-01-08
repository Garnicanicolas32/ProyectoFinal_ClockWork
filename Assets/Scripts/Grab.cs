using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    /*
    public Transform PosSlot;
    public float DistanceRayCast  = 0.5f;
    public float GrabRange;
    private bool Grabbing = false;

    public float ForceThrowUp = 0.2f;
    public float ForceThrowRight = 0.7f;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (Grabbing == false)
            {
                Vector3 PosArriba = PosSlot.position;
                PosArriba.y = PosSlot.position.y + DistanceRayCast;
                Vector3 PosAbajo = PosSlot.position;
                PosAbajo.y = PosSlot.position.y - DistanceRayCast;

                RaycastHit2D hitInfoArriba = Physics2D.Raycast(PosArriba, PosSlot.right, GrabRange);
                RaycastHit2D hitInfoMedio = Physics2D.Raycast(PosSlot.position, PosSlot.right, GrabRange);
                RaycastHit2D hitInfoAbajo = Physics2D.Raycast(PosAbajo, PosSlot.right, GrabRange);
                if (hitInfoMedio)
                {
                    Debug.Log(hitInfoMedio.transform.name);
                    Weapon weapon = hitInfoMedio.transform.GetComponent<Weapon>();
                    if (weapon != null)
                    {
                        Grabbing = true;
                        weapon.IsGrabbed = true;
                        weapon.weaponslot = PosSlot;
                        Debug.Log("Naisu");
                    }
                }
                else
                {
                    if (hitInfoArriba)
                    {
                        Weapon weapon = hitInfoArriba.transform.GetComponent<Weapon>();
                        if (weapon != null)
                        {
                            Grabbing = true;
                            weapon.IsGrabbed = true;
                            weapon.weaponslot = PosSlot;
                            Debug.Log("Naisu");
                        }
                    }
                    else if (hitInfoAbajo)
                    {
                        Weapon weapon = hitInfoAbajo.transform.GetComponent<Weapon>();

                        if (weapon != null)
                        {
                            Grabbing = true;
                            weapon.IsGrabbed = true;
                            weapon.weaponslot = PosSlot;
                            Debug.Log("Naisu");
                        }
                    }
                }

            }
            else
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(PosSlot.position, PosSlot.right);
                Weapon weapon = hitInfo.transform.GetComponent<Weapon>();
                if (weapon != null)
                {
                    Grabbing = false;
                    weapon.IsGrabbed = false;
                    weapon.Throw();
                }

            }
        }
    }
  
     * 
     
     
     ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
     
     
                /*
                Vector3 PosIzquierda = TopTransform.position;
                PosIzquierda.x -= DistanceRayCast;

                Vector3 PosDerecha = PosSlot.position;
                PosDerecha.x += DistanceRayCast;

                

                RaycastHit2D hitInfoIzq = Physics2D.Raycast(PosIzquierda, TopTransform.up, Mathf.Infinity, MaskWeapon);
                RaycastHit2D hitInfoMedio = Physics2D.Raycast(TopTransform.position, TopTransform.up, Mathf.Infinity, MaskWeapon);
                RaycastHit2D hitInfoDer = Physics2D.Raycast(PosDerecha, TopTransform.up, Mathf.Infinity,MaskWeapon);

                if (hitInfoMedio.collider != null)
                {
                    Debug.Log("Collider ok");
                    //Weapon weapon = hitInfoMedio.transform.GetComponent<Weapon>();
                    Weapon weapon = hitInfoMedio.transform.gameObject.GetComponent<Weapon>();

                    if (weapon != null)
                    {
                        Debug.Log("WEapon yeah");
                        EquipWeapon(weapon);
                    }
                }
                else
                {
                    if (hitInfoIzq)
                    {
                        Weapon weapon = hitInfoIzq.transform.GetComponent<Weapon>();
                        if (weapon != null)
                        {
                            Debug.Log("PIPO");
                            EquipWeapon(weapon);
                        }
                    }
                    else if (hitInfoDer)
                    {
                        Weapon weapon = hitInfoDer.transform.GetComponent<Weapon>();
                        if (weapon != null)
                        {
                            Debug.Log("PIPO");
                            EquipWeapon(weapon);
                        }
                    }
                }
                */   
     
     
     
     
     
     
     
     
     
     
     
}
