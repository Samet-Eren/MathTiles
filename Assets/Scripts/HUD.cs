using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{


    private VolumeExpoLerper volumeExpoLerperInstance;
    public CanvasGroup gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        volumeExpoLerperInstance = FindObjectOfType<VolumeExpoLerper>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void ShowGameOver()
    {
        gameOverPanel.interactable = true;
        gameOverPanel.blocksRaycasts = true;
        gameOverPanel.alpha = 1f;
    }
    public void RestartGame()
    {
        volumeExpoLerperInstance.ChangeFromTo(0f, -20f, 1.0f, () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
}
