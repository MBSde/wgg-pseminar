using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerNew : MonoBehaviour
{
    public static GameManagerNew instance; // Singleton pattern to access the GameManager from other scripts
    public int score = 0;
    public int combo = 0;
    public double accuracy = 1;
    private float hitlineYPosition = -2.9f;
    [SerializeField] private int highscore;
    private bool isGameOver = false;

    void Awake()
    {
        // Implement Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        highscore = PlayerPrefs.GetInt("HighscoreRhythmGame", 0);
    }

    void Update()
    {
        // Check for player input
        if (Input.GetButtonDown("First Button"))
        {
            CheckNoteHit(0); // 0 corresponds to the first column
        }
        else if (Input.GetButtonDown("Second Button"))
        {
            CheckNoteHit(1); // 1 corresponds to the second column
        }
        else if (Input.GetButtonDown("Third Button"))
        {
            CheckNoteHit(2); // 1 corresponds to the second column
        }
        else if (Input.GetButtonDown("Fourth Button"))
        {
            CheckNoteHit(3); // 1 corresponds to the second column
        }
    }

    void CheckNoteHit(int column)
    {
        print("checking column Nr. " + column);
        float hitRange = 0.5f; // Adjust this range as needed
        int maxPoints = 5;     // Maximum points for a perfect hit
        int minPoints = 1;     // Minimum points for a hit within the hit range

        // Find all notes currently in the column
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");

        // Iterate through the notes and check if they are in the hit range
        foreach (GameObject note in notes)
        {
            NoteController noteController = note.GetComponent<NoteController>();
            if (noteController != null && noteController.column == column)
            {
                // Check if the note is within the hit range around the hitline
                float noteY = note.transform.position.y;
                if (Mathf.Abs(noteY - hitlineYPosition) <= hitRange)
                {
                    // Calculate the hit accuracy based on proximity to the hitline
                    float accuracy = 1.0f - (Mathf.Abs(noteY - hitlineYPosition) / hitRange);

                    // Calculate the points based on accuracy (between minPoints and maxPoints)
                    int points = Mathf.RoundToInt(accuracy * (maxPoints - minPoints) + minPoints);
                    // Note is hit, so add a point and destroy the note
                    IncreaseScore(points);
                    combo++;
                    
                    Destroy(note);
                }
            }
        }
    }

    public void ResetCombo()
    {
        combo = 0;
    }
    public void IncreaseScore(int points)
    {
        if (!isGameOver)
        {
            score += points;
            // Additional score-related logic or event handling can be implemented here
        }
        if (score > highscore)
        {
            PlayerPrefs.SetInt("HighscoreRhythmGame", score);
            highscore = score;
        }
    }
}
