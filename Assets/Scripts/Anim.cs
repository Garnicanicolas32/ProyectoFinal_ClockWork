using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour {

    private Animator Animat;

	// Use this for initialization
	void Start () {
        Animat = GetComponent<Animator>();
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Animat.SetBool("Agachandose", true);
        }
        else
        {
            Animat.SetBool("Agachandose", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Animat.SetTrigger("Salto");
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            Animat.SetBool("Moviendose", true);
        }
        else
        {
            Animat.SetBool("Moviendose", false );
        }

    }
}
