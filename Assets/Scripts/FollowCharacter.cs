using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public Transform blankpoint;
    public float limit_between = 2f;
    public float limit_Max = 20f;
    public float limit_MaxY = 20f;

    public float cutoff = 0;
    public float cutoffY = 0;

    public float modifierZoom = 1;
    public float ExtraModifierZoom = 1.5f;

    public float z;

    private Vector3 last_position;
    private float last;
    private float distance;

    private Vector3 final;

    private Transform Target;
    private Transform Target_2;

    private GameObject x1;
    private GameObject x2;

    private bool allDead = false;
    private bool shaking = false;

    public float magnitude;

    Vector3 nextposition; 

    private Camera Cam;
    // Use this for initialization
    void Start()
    {
        Cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {        
        last = Cam.orthographicSize;
        last_position = Cam.transform.position;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        x1 = GameObject.FindGameObjectWithTag("Player 1");
        x2 = GameObject.FindGameObjectWithTag("Player 2");

        if (x1 == null)
        {
            if (x2 == null)
            {
                allDead = true;
                Target = blankpoint;
                Target_2 = blankpoint;               
            }
            else
            {
                allDead = false;
                Target = x2.transform;
            }
        }
        else
        {
            allDead = false;
            Target = x1.transform;
        }
        if (x2 == null)
        {
            if (x1 == null)
            {
                allDead = true;
                Target = blankpoint;
                Target_2 = blankpoint;
            }
            else
            {
                allDead = false;
                Target_2 = x1.transform;
            }
        }
        else
        {
            allDead = false;
            Target_2 = x2.transform;
        }

        float extra = Time.deltaTime * modifierZoom;
        float Moreextra = extra * ExtraModifierZoom;

        final = new Vector3((Target.position.x + Target_2.position.x) * 0.5f, (Target.position.y + Target_2.position.y) * 0.5f, z);
        Vector3 send = final;
        send.x = Mathf.Lerp(last_position.x, final.x, extra);
        send.y = Mathf.Lerp(last_position.y, final.y, extra);
        Cam.transform.position = send;
        //Cam.transform.position = Mathf.Lerp(last, last_position, Moreextra);


        Vector2 Xa = new Vector2(Target.position.x, 1);
        Vector2 Xb = new Vector2(Target_2.position.x, 1);
        distance = Vector2.Distance(Xa, Xb);

        Vector2 Xa2 = new Vector2(1, Target.position.y);
        Vector2 Xb2 = new Vector2(1,Target_2.position.y);
        float distanceY = Vector2.Distance(Xa2, Xb2);        

        float off = cutoff;
        float limit = limit_Max;

        if (!allDead)
        {
            if (distanceY > distance)
            {
                distance = distanceY;
                off = cutoffY;
                limit = limit_MaxY;
            }

            if (distance < limit_between)
            {
                //Cam.orthographicSize = limit_between;
                Cam.orthographicSize = Mathf.Lerp(last, limit_between + off, extra);
            }
            else
            {
                if (distance > limit)
                {
                    Cam.orthographicSize = Mathf.Lerp(last, limit + off, extra);
                    //Cam.orthographicSize = limit_Max;
                }
                else
                {
                    //Cam.orthographicSize = Mathf.Lerp(last, distance + CutOff, extra);
                    Cam.orthographicSize = Mathf.Lerp(last, distance + off, extra);
                }
            }
        }
        else
        {
            distance = Mathf.Lerp(last, distance + off, Moreextra);
            Cam.orthographicSize = Mathf.Lerp(last, limit + off, Moreextra);
            allDead = true;
        }

        if (shaking)
        {
            Vector3 original = transform.localPosition;            
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(original.x - x, original.y - y, original.z);
        }
    }

    IEnumerator Shake(float duration, float mag)
    {
        magnitude = mag;
        shaking = true;
        yield return new WaitForSeconds(duration);
        shaking = false;
    }

    public void TryShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
}
