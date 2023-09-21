using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuOptions : MonoBehaviour
{
    [SerializeField] Scene selectedScene;

    public void Button_exit()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void Load_Scene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Load_Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Load_MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
