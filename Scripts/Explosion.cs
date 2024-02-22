using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AudioClip audioClip;
    [SerializeField] float Duration = 10;

    void Start()
    {
        if (audioClip != null) 
        {
            gameObject.GetComponent<AudioSource>().clip = audioClip;
            gameObject.GetComponent<AudioSource>().Play();
        }

        Destroy(gameObject, Duration);
    }
}
