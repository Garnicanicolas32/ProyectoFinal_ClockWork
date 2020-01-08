using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Spawn : MonoBehaviour
{
    public Animator transitionanim;

    public float time = 5;
    public float size = 1;

    public TextMeshProUGUI texto;
    public TextMeshProUGUI timer;
    public GameObject panel;
   /* public class ListHealtImgs
    {
        public List<healtImgs> ListList;
    }    */

   // public class healtImgs
    //{
     //   public List<Image> LstImage;
   // }

    public List<GameObject> players;
    private List<List<Image>> healthImgs;
    public List<Image> TwelveImages;

    public List<Transform> pos;
    private List<GameObject> Find;
    private List<bool> doing;
    private List<int> lives;
    private List<bool> stillAlive;
    private bool pause = false;
    public int MaxTime;
    private float Ftime;
    public float timefornext = 10;

    // Use this for initialization
    void Start()
    {
        Ftime = MaxTime;
        pause = false;
        texto.enabled = false;
        panel.SetActive(false);

        healthImgs = new List<List<Image>>();
        Find = new List<GameObject>();
        doing = new List<bool>();
        lives = new List<int>();
        stillAlive = new List<bool>();
        

        int i = 1;
        foreach (GameObject a in players)
        {
            doing.Add(false);
            stillAlive.Add(true);
            string str = "Player " + i;
            Find.Add(GameObject.FindGameObjectWithTag(str));
            lives.Add(Find[i - 1].GetComponent<Movement>().Lives);
            i++;
        }

        int contX = 0;
        int contY = 1;
        int contZ = 2;
        for (int x = 0; x < 4; x++)
        {
            List<Image> temp = new List<Image>(); //0 1 2 //3 4 5 // 6 7 8 // 9 10 11//
            temp.Add(TwelveImages[contX]);
            temp.Add(TwelveImages[contY]);
            temp.Add(TwelveImages[contZ]);
            contX += 3;
            contY += 3;
            contZ += 3;
            healthImgs.Add(temp);
        }
        foreach (List<Image> a in healthImgs)
        {
            a[0].gameObject.SetActive(false);
            a[1].gameObject.SetActive(false);
            a[2].gameObject.SetActive(false);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        int temporal = Mathf.RoundToInt(Ftime);
        timer.text = temporal.ToString();
        Ftime -= Time.deltaTime;

        if (Input.GetButtonDown("Pause"))
        {
            if (pause)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
        int cont = 1;
        int i = 0;
        foreach (GameObject a in players)
        {
            string str = "Player " + cont;
            Find[i] = GameObject.FindGameObjectWithTag(str);
            if (Find[i] != null)
            {
                doing[i] = false;
            }

            if (Find[i] == null && !doing[i])
            {
                lives[i]--;
                if (lives[i] > 0)
                {
                    StartCoroutine(spawn(players[i], i));
                    doing[i] = true;
                }
            }
            cont++;
            i++;
        }

        int contX = 0;
        int ManyAlive = 0;
        foreach (GameObject a in players)
        {
            switch (lives[contX])
            {
                case 0:
                    healthImgs[contX][0].gameObject.SetActive(false);
                    healthImgs[contX][1].gameObject.SetActive(false);
                    healthImgs[contX][2].gameObject.SetActive(false);
                    break;

                case 1:
                    healthImgs[contX][0].gameObject.SetActive(true);
                    healthImgs[contX][1].gameObject.SetActive(false);
                    healthImgs[contX][2].gameObject.SetActive(false);
                    ManyAlive++;
                    break;

                case 2:
                    healthImgs[contX][0].gameObject.SetActive(true);
                    healthImgs[contX][1].gameObject.SetActive(true);
                    healthImgs[contX][2].gameObject.SetActive(false);
                    ManyAlive++;
                    break;

                case 3:
                    healthImgs[contX][0].gameObject.SetActive(true);
                    healthImgs[contX][1].gameObject.SetActive(true);
                    healthImgs[contX][2].gameObject.SetActive(true);
                    ManyAlive++;
                    break;

                default:
                    healthImgs[contX][0].gameObject.SetActive(false);
                    healthImgs[contX][1].gameObject.SetActive(false);
                    healthImgs[contX][2].gameObject.SetActive(false);                    
                    break;
            }
            contX++;
        }

        if (ManyAlive < 2)
        {
            panel.SetActive(true);
            texto.enabled = true;
            timer.enabled = false;
            StartCoroutine(nextscene());
        }
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
        pause = false;
    }
    public void pauseGame()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);
        pause = true;
    }


    IEnumerator spawn(GameObject @object, int i)
    {
        yield return new WaitForSeconds(time);

        int x = Random.Range(0, pos.Count);
        GameObject obj = Instantiate(players[i], pos[x].position, Quaternion.identity);
        obj.GetComponent<Movement>().Lives = lives[i];
        Vector3 scale = obj.transform.localScale;
        scale.x = size;
        scale.y = size;
        obj.transform.localScale = scale;

        doing[i] = false;
    }

    IEnumerator nextscene()
    {
        //yield return new WaitForSeconds(timefornext);
        yield return new WaitForSeconds(4);

        if (transitionanim != null)
        {
            transitionanim.SetTrigger("end");
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
}
