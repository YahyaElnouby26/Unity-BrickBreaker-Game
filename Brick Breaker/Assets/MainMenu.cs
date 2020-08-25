using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource audio;

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitForSceneLoad(string name)
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(name);

    }

    public void StartGame()
    {
        StartCoroutine(WaitForSceneLoad("MainGame"));
    }

    public void HowPlay()
    {
        StartCoroutine(WaitForSceneLoad("HowToPlay"));
    }
    
    public void playSound()
    {
        audio.Play();
    }
}
