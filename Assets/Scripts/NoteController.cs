using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private float speed;
    public int column;
    public bool resetCombo = false;
    private GameManagerNew gameManager;

    public void Initialize(float speed, int column)
    {

        this.speed = speed;
        this.column = column;
    }


    void Update()
    {
        // Move the note downward at a constant speed.
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        
        // Check for player input and scoring logic.
        // Implement note destruction when it reaches the hitline or goes off-screen.
        if(transform.position.y <= -3.4f &&!resetCombo )
        {
            gameManager = GameManagerNew.instance;
            gameManager.ResetCombo();
            resetCombo = true;
        }
        if(transform.position.y <= -5.5f)
        {
            Destroy(this);
        }
    }
}
