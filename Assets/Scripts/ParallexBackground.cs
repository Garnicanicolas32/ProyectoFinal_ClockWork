using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackground : MonoBehaviour {

    public Transform[] Fondos;
    public float[] ParallexEscala;
    public float Smoothing = 1f;

    public Transform Cam;
    private Vector3 PosPrevia;

    private void Awake()
    {
        Cam = Camera.main.transform;
    }

    // Use this for initialization
    void Start () {
        PosPrevia = Cam.position;

        ParallexEscala = new float[Fondos.Length];
        for (int i = 0; i < Fondos.Length; i++)
        {
            ParallexEscala[i] = Fondos[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        for (int i = 0; i < Fondos.Length; i++)
        {
            float Parallax = (PosPrevia.x - Cam.position.x) * ParallexEscala[i];

            float BackgroundTargetPosX = Fondos[i].position.x + Parallax;

            Vector3 backgroundTargetPos = new Vector3(BackgroundTargetPosX, Fondos[i].position.y, Fondos[i].position.z);

            Fondos[i].position = Vector3.Lerp(Fondos[i].position, backgroundTargetPos, Smoothing * Time.deltaTime);
        }

        PosPrevia = Cam.position;
	}
}
