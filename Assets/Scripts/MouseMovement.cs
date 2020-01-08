using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseMovement : MonoBehaviour {

    public float MovimientoHorizontal;
    public float MovimientoVertical;
    public float VelocidadMovimiento;
    public Vector3 last;
    private bool first = true;
    private bool can = true;
    //public SpriteRenderer SevenUp;
    public Image imagen;
    //private Sprite Inv;
    //private Sprite Vis;
    // Use this for initialization
    void Start () {
        //SevenUp = gameObject.GetComponent<SpriteRenderer>();
        imagen = gameObject.GetComponent<Image>();
      //  Inv = SevenUp.sprite;
       // Color Yes = new Color(0, 0, 0, 0);
       // Vis = SevenUp.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition == last)
        {
            if (first)
            {
                if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
                {
                    first = false;
                }
            }
            else
            {
                Cursor.visible = false;
                imagen.color = new Color(255, 255, 255, 255);
                //SevenUp.color = new Color(255, 255, 255, 255);
                can = true;
            }            
        }

        if (can)
        {
            MovimientoHorizontal = Input.GetAxisRaw("Horizontal") * VelocidadMovimiento;
            MovimientoVertical = Input.GetAxisRaw("Vertical") * -VelocidadMovimiento;
            Vector3 actual = transform.position;
            actual.x += MovimientoHorizontal;
            actual.y += MovimientoVertical;
            transform.position = actual;
        }

        if (Input.mousePosition != last)
        {
            can = false;
            Cursor.visible = true;
            first = true;
            //SevenUp.color = new Color(0, 0, 0, 0);
            imagen.color = new Color(255, 255, 255, 0);
        }
        last = Input.mousePosition;
    }
}
