using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentalDeath : MonoBehaviour {

    private Movement @object;
    private Weapon_2 weapon;

    void OnTriggerEnter2D(Collider2D other)
    {
        @object = other.GetComponent<Movement>();
        weapon = other.GetComponent<Weapon_2>();

        if (@object)
        {
            @object.TakeDamage();
        }
        else if(weapon)
        {
            Destroy(other.gameObject);
        }
    }
}
