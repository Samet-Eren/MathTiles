using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private VolumeExpoLerper volumeExpoLerperInstance;
    void Start()
    {
        volumeExpoLerperInstance = FindObjectOfType<VolumeExpoLerper>();
        volumeExpoLerperInstance.ChangeFromTo(-20f, 0f, 1.5f);
    }

    void Update()
    {

    }

    public void StartGame()
    {
        volumeExpoLerperInstance.ChangeFromTo(10f, -20f, 2.0f, () =>
        {
            SceneManager.LoadScene("Game");
        });
    }
}
