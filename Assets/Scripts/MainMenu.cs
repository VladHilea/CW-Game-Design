using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        if(audioSource !=null)
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            LoadTutorial();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            LoadGame();
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial Scene");
        Debug.Log("assacnas");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
}

