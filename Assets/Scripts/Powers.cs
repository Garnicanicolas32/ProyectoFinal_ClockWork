using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour
{


    public int id_character;
    /// ////////////
    /// 
    public Weapon_2[] platinas_weapons;
    public Transform weaponPosition;
    public float WeaponSize = 1;

    public Transform GranadePosition;
    public Vector3 x1;
    public Vector3 x2;
    public Vector3 x3;
    public GameObject granade;
    public float time;
    public float multiplier;
    public float xtime;

    public float MarkTime = 7;
    public float PlatinaTime = 14;

    public void FixedUpdate()
    {
        if (xtime > 0)
        {
            xtime -= Time.deltaTime;
        }
    }

    public void UseMark()
    {
        if (xtime <= 0)
        {
            xtime = MarkTime;
            //  if (ImpactEffect != null)
                 // {
                 //    Instantiate(ImpactEffect, GranadePosition, Quaternion.identity);
                 // }

            x1 = GranadePosition.position;
            x2 = GranadePosition.position;
            x3 = GranadePosition.position;

            x1.y -= 1;
            x3.y += 1;
            GameObject @object = Instantiate(granade, x1, Quaternion.identity);
            GameObject @object2 = Instantiate(granade, x2, Quaternion.identity);
            GameObject @object3 = Instantiate(granade, x3, Quaternion.identity);


            @object.GetComponent<Granade>().timer = time;
            @object.GetComponent<Granade>().multiplier = multiplier;
            @object.GetComponent<Granade>().Throw();
            @object.GetComponent<Granade>().which = 1;

            @object2.GetComponent<Granade>().timer = time;
            @object2.GetComponent<Granade>().multiplier = multiplier;
            @object2.GetComponent<Granade>().Throw();
            @object2.GetComponent<Granade>().which = 2;

            @object3.GetComponent<Granade>().timer = time;
            @object3.GetComponent<Granade>().multiplier = multiplier;
            @object3.GetComponent<Granade>().Throw();
            @object3.GetComponent<Granade>().which = 3;

            //Gravity.velocity = new Vector2(ForceRight* multiplier, ForceUp);
        }
    }

    public Weapon_2 UsePlatina()
    {
        if (xtime <= 0)
        {
            xtime = PlatinaTime;
            int a = Random.Range(0, platinas_weapons.Length);
            Weapon_2 create = platinas_weapons[a];
            create.onStand = true;
            create.standPos = weaponPosition.position;
            Vector3 scale = create.transform.localScale;
            scale.x = WeaponSize;
            scale.y = WeaponSize;
            create.transform.localScale = scale;
            Weapon_2 @object = Instantiate(create, weaponPosition.position, Quaternion.identity);
            return @object;
            //  if (ImpactEffect != null)
            // {
            //    Instantiate(ImpactEffect, GranadePosition, Quaternion.identity);
            // }
        }
        else { return null; }
    }
}
