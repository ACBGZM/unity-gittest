using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private void Awake() {
        Time.timeScale = 1;
    }
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    public void UIAppear() {
        GameObject.Find("Canvas/MainMenu/UI").SetActive(true);
    }
}
