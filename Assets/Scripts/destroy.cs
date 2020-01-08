using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {

    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject x = GetComponentInParent<Transform>().parent.gameObject;
        Destroy(x);
    }
}
