using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;

public class QuestionGenerator : MonoBehaviour
{
    public Question questionPrefab; // Reference to the question prefab to spawn
    public float initialSpawnInterval = 5f; // Initial interval between question spawns
    public float minimumSpawnInterval = 1f; // Minimum spawn interval
    public float spawnIntervalDecrease = 0.1f;
    public float currentSpawnInterval; // Current interval between question spawns

    public bool isSpawning = false;
    public HUD hud;
    private VolumeExpoLerper volumeExpoLerperInstance;
    private void Start()
    {
        volumeExpoLerperInstance = FindObjectOfType<VolumeExpoLerper>();
        volumeExpoLerperInstance.ChangeFromTo(-20f, 0f, 3.0f);
        ResetGenerator();
        StartGenerator();
        StartCoroutine(SpawnQuestions());
    }

    IEnumerator SpawnQuestions()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(currentSpawnInterval);
            if (isSpawning)
            {
                Question newQuestion = Instantiate(questionPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                newQuestion.questionGenerator = this;
                newQuestion.fallSpeed = initialSpawnInterval + 1 - currentSpawnInterval;
                currentSpawnInterval = Mathf.Max(minimumSpawnInterval, currentSpawnInterval - spawnIntervalDecrease);
            }
        }
    }

    public void ResetGenerator()
    {
        currentSpawnInterval = initialSpawnInterval;
    }
    public void StartGenerator()
    {
        isSpawning = true;
    }
    public void StopGenerator()
    {
        isSpawning = false;
    }
    public void WrongAnswer()
    {
        StopGenerator();
        volumeExpoLerperInstance.ChangeFromTo(10f, -20f, 1.0f, () =>
        {
            hud.ShowGameOver();
            volumeExpoLerperInstance.ChangeFromTo(-20f, 0f, 0.5f);
            //delete all game objects with the tag "Question"
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("Question");

            foreach (GameObject obj in objectsWithTag)
            {
                Destroy(obj);
            }
        });
    }
    public void CorrectAnswer()
    {

        volumeExpoLerperInstance.ChangeFromTo(3f, 0f, 0.5f);
    }
}
