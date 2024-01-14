using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileBehavior : MonoBehaviour
{
    public TextMeshProUGUI label;
    public int myAnswer = 0;
    public Question question;
    public Button myButton;
    public Color trueColor = Color.green;
    public Color falseColor = Color.red;

    // Method to set the text on the button
    public void SetAnswer(int answer)
    {
        myAnswer = answer;
        // Assuming you have a TextMeshProUGUI component on the button, you can set its text
        if (myButton != null)
        {
            TextMeshProUGUI buttonText = myButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = "" + myAnswer;
            }
        }
    }

    public void TryAnswer()
    {
        bool result = question.TryAnswer(myAnswer);
        // Change button color based on the result
        if (result)
        {
            myButton.image.color = trueColor;
        }
        else
        {
            myButton.image.color = falseColor;
        }
    }
}
