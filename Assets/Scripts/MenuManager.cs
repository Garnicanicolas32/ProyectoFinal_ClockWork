using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //public Scene change;
    public Scene[] escenas;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Quit()
    {
        Debug.Log("aa");
        Application.Quit();
    }
}
