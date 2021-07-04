using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterHouse : MonoBehaviour
{

    public GameObject nextLevelDialog;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            this.gameObject.SetActive(false);
            nextLevelDialog.SetActive(true);
            Invoke(name = "GoToNextLevel", 1.5f);
        }
    }

    void GoToNextLevel() {
        int totalSceneNum = SceneManager.sceneCountInBuildSettings;
        int thisSceneNum = SceneManager.GetActiveScene().buildIndex;
        if (thisSceneNum + 1 < totalSceneNum) {
            SceneManager.LoadScene(thisSceneNum + 1);
        }
        else {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
