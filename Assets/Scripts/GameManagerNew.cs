using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerNew : MonoBehaviour
{
    public static GameManagerNew instance; // Singleton pattern to access the GameManager from other scripts
    public int points = 0;
    public int comboPoints = 0;
    public double score = 0;
    public int roundedScore = 0;
    public int combo = 0;
    [SerializeField] private int totalNotes = 64;
    private double maxPoints = 0;
    private double maxComboScore = 0;
    private double pointsMultiplier = 0;
    private double comboPointsMultiplier = 0;
    public double hitAccuracy = 1.0;
    public double roundedHitAccuracy = 1.00;
    private float hitlineYPosition = -2.9f;
    private int notesMissed = 0;
    private int notesHit = 0;
    private int highscore;
    public bool isGameStarted = false;
    private bool isGameOver = false;
    private GameObject audioManager;
    public GameObject restartButton;
    public GameObject startButton;
    public GameObject tutorialPanel;

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
        if (PlayerPrefs.GetInt("RhythmGameTutorial", 0)==1)
        {
            tutorialFinished();
        }
        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
    }

    void Update()
    {
        if (isGameStarted && !isGameOver)
        {
            if (GameObject.FindGameObjectsWithTag("Note").Length == 0)
            {
                EndGame();
            }
        }
        calculateScore();
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
        int maxPointsOnNote = 5;     // Maximum points for a perfect hit
        int minPointsOnNote = 1;     // Minimum points for a hit within the hit range

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
                    int added = Mathf.RoundToInt(accuracy * (maxPointsOnNote - minPointsOnNote) + minPointsOnNote);
                    // Note is hit, so add a point and destroy the note
                    IncreasePoints(added);
                    combo++;
                    notesHit++;
                    Destroy(note);
                }
            }
        }
    }

    public void ResetCombo()
    {
        combo = 0;
    }
    public void IncreasePoints(int added)
    {
        if (!isGameOver)
        {
            this.points += added;
            // Additional score-related logic or event handling can be implemented here
            increaseComboPoints(added);
        }
        
    }

    public void increaseComboPoints(int added)
    {
        comboPoints = comboPoints + added * combo;
    }

    public void calculateScore()
    {
        score = points * pointsMultiplier + comboPoints * comboPointsMultiplier;
        roundedScore = ((int)score);
        if (this.roundedScore > highscore)
        {
            PlayerPrefs.SetInt("HighscoreRhythmGame", this.roundedScore);
            highscore = this.roundedScore;
        }
        if((notesHit + notesMissed) != 0)
        {
            hitAccuracy = (double)points / ((notesHit + notesMissed)*5);
        }
        else
        {
            hitAccuracy = 1;
        }
        roundedHitAccuracy = ((int)(hitAccuracy * 100));
    }

    public void StartGame()
    {
        isGameStarted = true;
        audioManager.GetComponent<AudioManager>().PlayMainSong();
        // Find all notes currently in the column
        GameObject[] notes = GameObject.FindGameObjectsWithTag("Note");

        // Iterate through the notes and check if they are in the hit range
        foreach (GameObject note in notes)
        {
            note.GetComponent<NoteController>().gameStart();
        }
    }

    public void EndGame()
    {
        restartButton.SetActive(true);
    }

    public void noteMissed()
    {
        notesMissed++;
    }

    public void setTotalNotes(int newTotal)
    {
        totalNotes = newTotal;
        maxPoints = totalNotes * 5;
        maxComboScore = 5 * ((double)(totalNotes + 1) * ((double)totalNotes / 2));
        print(maxComboScore);
        pointsMultiplier = (400 / maxPoints);
        comboPointsMultiplier = (600 / maxComboScore);
        print(comboPointsMultiplier);
    }

    public void tutorialFinished()
    {
        tutorialPanel.SetActive(false);
        startButton.SetActive(true);

    }

   public void skipTutorial()
    {
        int bVal = 0;
        if (PlayerPrefs.GetInt("RhythmGameTutorial", bVal)==0)
        {
            bVal = 1;
        }
        PlayerPrefs.SetInt("RhythmGameTutorial", bVal);
    }
}
