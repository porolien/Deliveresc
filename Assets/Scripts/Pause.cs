using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (pauseMenuUI != null)
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    OpenMenu();
                }
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
    void OpenMenu()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ChangeScene(string scene)
    {
        if (scene == "quit")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
}
