using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    [SerializeField] private float speed;

    public void Initialize(float speed)
    {

        this.speed = speed;
    }


    void Update()
    {
        // Move the note downward at a constant speed.
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        
        // Check for player input and scoring logic.
        // Implement note destruction when it reaches the hitline or goes off-screen.
    }
}
