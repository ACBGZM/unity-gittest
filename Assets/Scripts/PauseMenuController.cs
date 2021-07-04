using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;

    public AudioMixer audioMixer;

    public void PauseMenuAppear() {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void PauseMenuDisappear() {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }


    public void SetVolume(float sliderValue) {
        audioMixer.SetFloat("MainVolume", sliderValue);
    }

    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

}
