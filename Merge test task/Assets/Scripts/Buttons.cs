using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public Text soundText;
    private void Start()
    {
        if (soundText)
            if (AudioListener.volume == 0f)
            {
                soundText.text = "Включить звук";
            }
            else
            {
                soundText.text = "Выключить звук";
            }
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SoundOnOff()
    {
        if(AudioListener.volume == 0f)
        {
            soundText.text = "Выключить звук";
            AudioListener.volume = 1f;
        }
        else
        {
            soundText.text = "Включить звук";
            AudioListener.volume = 0f;
        }        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
