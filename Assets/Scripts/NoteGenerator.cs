using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGenerator : MonoBehaviour
{

    public GameObject notePrefab;
    public Transform[] columns;
    public float noteSpeed = 4.0f;
    public float spawnDelay = 3.0f;
    public int ticksPerBeat = 1;
    public float beatsPerMinute = 120.0f;
    private float secondsPerBeat = 0.5f;
    private float spawnYPosition = 6.5f;
    private float hitlineYPosition = -3.5f;

    // Other variables and methods...

    void Start()
    {
        // Calculate the time in seconds per beat based on BPM and ticks per beat.
        secondsPerBeat = 60.0f / (beatsPerMinute * ticksPerBeat);

        // Start a coroutine to spawn notes.
        SpawnNotesAtStart();
        
    }

    // Other methods...
    void SpawnNotesAtStart()
    {
        float firstNoteHeight = (float)(noteSpeed * spawnDelay - 0.15);

        // Calculate the time interval between note spawns.
        float spawnInterval = secondsPerBeat / ticksPerBeat;
 
        // Loop to spawn notes.
        for (int i = 0; i < 100; i++) // Spawn 100 notes (adjust the number as needed).
        {
            // Calculate the spawn time for this note.
            float height = firstNoteHeight + i * spawnInterval * noteSpeed;

            // Instantiate a note and set its position.
            GameObject note = Instantiate(notePrefab);
            int columnNumber = Random.Range(0, columns.Length);
            Transform selectedColumn = columns[columnNumber];
            float xPosition = selectedColumn.position.x;

            // Calculate the y position to arrive at the hitline on the beat.
            float yPosition = hitlineYPosition + height;

            note.transform.position = new Vector3(xPosition, yPosition, 0);

            // Set the note's behavior (NoteController) and destroy it when it's no longer needed.
            NoteController noteController = note.GetComponent<NoteController>();
            noteController.Initialize(noteSpeed, columnNumber);
            
        }
    }

}
