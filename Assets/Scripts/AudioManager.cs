using UnityEngine;
using System.Collections;
public class AudioManager : MonoBehaviour
{
    public AudioSource audioSourceMainSong;
    public AudioClip song1;
    public AudioClip song2;
    public AudioClip song3;
    public AudioClip mainSong; // Reference to your main song audio clip
    public AudioSource audioSourceDrumstick;
    public AudioClip drumstickSound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Function to play the main song
    public void PlayMainSong(int songNR)
    {
        switch (songNR)
        {
            case 1:
                mainSong = song1;
                break;
            case 2:
                mainSong = song2;
                break;
            case 3:
                mainSong = song3;
                break;
            default:
                mainSong = song1;
                break;
        }
        if (audioSourceMainSong != null && mainSong != null)
        {
            audioSourceMainSong.clip = mainSong;
            audioSourceMainSong.PlayDelayed(3);
        }
        else
        {
            Debug.LogError("AudioSource or mainSong AudioClip is not set.");
        }
    }

    public void playDrumstick(float delay, float spawnInterval)
    {
        if (audioSourceDrumstick != null && drumstickSound != null)
        {
            StartCoroutine(waitForDrumsticks(delay, spawnInterval));
        }
        else
        {
            Debug.LogError("AudioSource or mainSong AudioClip is not set.");
        }
    }
    IEnumerator waitForDrumsticks(float delay, float spawnInterval)
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(delay-4*spawnInterval);
        audioSourceDrumstick.clip = drumstickSound;
        audioSourceDrumstick.Play();
        yield return new WaitForSeconds(spawnInterval);
        audioSourceDrumstick.Play();
        yield return new WaitForSeconds(spawnInterval);
        audioSourceDrumstick.Play();
    }
}