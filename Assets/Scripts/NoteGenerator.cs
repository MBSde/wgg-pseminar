using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class NoteData
{
    public int Column;
    public float Time;
    public bool TimeIsInTicks;
    public float Length;
}
[System.Serializable]
public class SongData
{
    public float BPM;
    public int TicksPerBeat;
    public string SoundFileName;
    public float NoteSpeed;
    public float SpawnDelay;
    public NoteData[] Notes;
}

public class NoteGenerator : MonoBehaviour
{

    public GameObject notePrefab;
    private GameObject audioManager;
    private GameManagerNew gameManager;
    public Transform[] columns;
    private float noteSpeed = 4.0f;
    private float spawnDelay = 3.0f;
    private int ticksPerBeat = 4;
    private float beatsPerMinute = 120.0f;
    private float secondsPerBeat = 0.125f;
    private float hitlineYPosition = -2.9f;
    private string soundFileName;
    public TextAsset odeLeicht;


    // Other variables and methods...

    void Start()
    {
        gameManager = GameManagerNew.instance;
        // Calculate the time in seconds per beat based on BPM and ticks per beat.
        secondsPerBeat = 60.0f / (beatsPerMinute * ticksPerBeat);
        SongData songData = JsonUtility.FromJson<SongData>(odeLeicht.text);

        // Output the song data to the console (for testing)
        beatsPerMinute = songData.BPM;
        ticksPerBeat = songData.TicksPerBeat;
        soundFileName = songData.SoundFileName;
        noteSpeed = songData.NoteSpeed;
        spawnDelay = songData.SpawnDelay;


        // Create an array of notes
        NoteData[] notes = songData.Notes;


        gameManager.setTotalNotes(notes.Length);
        // Start a coroutine to spawn notes.

        audioManager = GameObject.FindGameObjectWithTag("AudioManager");
        float firstNoteHeight = (float)(noteSpeed * spawnDelay + 0.25);

        // Calculate the time interval between note spawns.
        float ticksPerSecond = 1/((beatsPerMinute*ticksPerBeat) / 60);

        foreach (NoteData note in notes)
        {
            GameObject noteObj = Instantiate(notePrefab);
            Transform selectedColumn = columns[note.Column];
            float height = 0;
            if (note.TimeIsInTicks)
            {
                height = firstNoteHeight + note.Time  * noteSpeed * ticksPerSecond;
            }
            else
            {
                height = firstNoteHeight + note.Time * noteSpeed;
            }

            float xPosition = selectedColumn.position.x;
            // Calculate the y position to arrive at the hitline on the beat.
            float yPosition = hitlineYPosition + height;

            noteObj.transform.position = new Vector3(xPosition, yPosition, 0);
            NoteController noteController = noteObj.GetComponent<NoteController>();
            noteController.Initialize(noteSpeed, note.Column);
        }
    }



    public void playBeatAtStart()
    {
        
        for(int i = 1; i < 4; i++)
        {
            audioManager.GetComponent<AudioManager>().playDrumstick(spawnDelay, (beatsPerMinute/60));
                
        }
    }
}


