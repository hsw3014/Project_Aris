using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showMenu : MonoBehaviour
{
    public GameObject PauseUI;
    private AudioSource UI_Audio;
    private bool isPaused = false;

    public AudioClip menuSound;

    // Start is called before the first frame update
    void Start()
    {
        UI_Audio = GetComponent<AudioSource>();
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                UI_Audio.volume = 0.2f;
            }
            else
            {
                UI_Audio.volume = 1.0f;
            }
        }

        if(isPaused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseUI.SetActive(true);
            Time.timeScale = 1f;
        }
    }
}
