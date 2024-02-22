using UnityEngine;

public class UiAudio : MonoBehaviour
{
    AudioSource audio;
   [SerializeField] AudioClip sound;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void playAudio() 
    {
        audio.PlayOneShot(sound);
    }
}
