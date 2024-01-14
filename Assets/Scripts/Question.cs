using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Question : MonoBehaviour
{
    public TextMeshProUGUI questionLabel;
    public int trueAnswer = 0;
    public TileBehavior tile1;
    public TileBehavior tile2;
    public TileBehavior tile3;
    public float fallSpeed = 1f;
    public QuestionGenerator questionGenerator;
    private bool answered = false;
    void Start()
    {
        generateQuestion();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y - fallSpeed * Time.deltaTime,
            transform.position.z
        );
        if (transform.position.y < -7f)
        {
            if (!answered)
            {
                questionGenerator.WrongAnswer();
            }
            Destroy(gameObject);
        }
    }

    public void generateQuestion()
    {
        int n1 = Random.Range(2, 10);
        int n2 = Random.Range(2, 10);
        int operationIndex = Random.Range(0, 3);
        string[] operationSymbols = new string[] { "+", "-", "x" };
        string operationSymbol = operationSymbols[operationIndex];

        switch (operationIndex)
        {
            case 0:
                //add
                trueAnswer = n1 + n2;
                break;
            case 1:
                //subtract
                trueAnswer = n1 - n2;
                break;
            case 2:
                //multiply
                trueAnswer = n1 * n2;
                break;
        }
        questionLabel.text = n1.ToString() + operationSymbol + n2.ToString();

        //set the answers
        TileBehavior[] tiles = { tile1, tile2, tile3 };
        ShuffleArray(tiles);
        //correct answer
        tiles[0].SetAnswer(trueAnswer);
        //wrong answers
        tiles[1].SetAnswer(trueAnswer + Random.Range(1, 5));
        tiles[2].SetAnswer(trueAnswer - Random.Range(1, 5));

    }

    void ShuffleArray(TileBehavior[] array)
    {
        for (int t = 0; t < array.Length; t++)
        {
            TileBehavior tmp = array[t];
            int r = Random.Range(t, array.Length);
            array[t] = array[r];
            array[r] = tmp;
        }
    }
    public bool TryAnswer(int answer)
    {
        answered = true;
        tile1.myButton.interactable = false;
        tile2.myButton.interactable = false;
        tile3.myButton.interactable = false;

        bool isCorrectAnswer = answer == trueAnswer;

        if (isCorrectAnswer)
        {
            questionGenerator.CorrectAnswer();
        }
        else
        {
            questionGenerator.WrongAnswer();
        }
        return isCorrectAnswer;

    }
}
