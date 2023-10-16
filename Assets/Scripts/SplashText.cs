using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplashText : MonoBehaviour
{
    public TextMeshProUGUI perfect;
    public TextMeshProUGUI great;
    public TextMeshProUGUI nice;
    public TextMeshProUGUI good;
    public TextMeshProUGUI fine;
    public TextMeshProUGUI miss;
    private int ID = 0;

    private TextMeshProUGUI[] textObjects;

    private void Start()
    {
        // Store the TextMeshPro objects in an array for easy management
        textObjects = new TextMeshProUGUI[] { miss, fine, good, nice, great, perfect };

        // Deactivate all text objects at the start
        DeactivateAllText();
    }

    // Function to deactivate all text objects
    void DeactivateAllText()
    {
        foreach (var textObject in textObjects)
        {
            textObject.gameObject.SetActive(false);
        }
    }

    // Function to activate a specific text object for a duration (in seconds)
    public void ActivateText(int textToActivate, float duration)
    {
        ID++;
        // Ensure the input is within a valid range
        if (textToActivate >= 0 && textToActivate < textObjects.Length)
        {
            // Deactivate any currently active text
            DeactivateAllText();

            // Activate the specified text
            textObjects[textToActivate].gameObject.SetActive(true);

            // Start a coroutine to deactivate the text after the specified duration
            StartCoroutine(DeactivateTextAfterDuration(textToActivate, duration, ID));
        }
    }

    // Coroutine to deactivate a specific text object after a specified duration
    IEnumerator DeactivateTextAfterDuration(int textToDeactivate, float duration, int startID)
    {
        yield return new WaitForSeconds(duration);
        if(startID == ID)
        {
            // Deactivate the specified text after the duration
            textObjects[textToDeactivate].gameObject.SetActive(false);
        }

    }
}
