using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public AudioSource audio;

    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainMenu");

    }

    public void BackToMenu()
    {
        StartCoroutine(WaitForSceneLoad());
    }

    public void playSound()
    {
        audio.Play();
    }
}

