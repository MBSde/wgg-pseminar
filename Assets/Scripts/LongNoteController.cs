using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoteController : MonoBehaviour
{
    private float speed;
    public int column;
    private float length;
    public bool resetCombo = false;
    private GameManagerNew gameManager;
    private bool gameStarted = false;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject connection;
    [SerializeField] private GameObject tail;


    public void Initialize(float speed, int column, float length)
    {

        this.speed = speed;
        this.column = column;
        this.length = length;


    }
    private void Start()
    {
        connection.transform.localScale = new Vector3(1, length, 1);
        connection.transform.localPosition = new Vector3(0, length / 2, 0);
        tail.transform.localPosition = new Vector3(0, length, 0);
    }


    void Update()
    {
        setLengths();
        if (gameStarted)
        {
            // Move the note downward at a constant speed.
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            // Check for player input and scoring logic.
            // Implement note destruction when it reaches the hitline or goes off-screen.
            
        }

    }

    public void gameStart()
    {
        gameStarted = true;
    }

    public float getLength()
    {
        return this.length;
    }

    public void setToHitline()
    {
        speed = 0;
    }

    void setLengths()
    {
        
    }
}
