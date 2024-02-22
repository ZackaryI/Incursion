using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    float timeStep;
    [SerializeField] int index = -1;

    // Start is called before the first frame update
    void Start()
    {
        timeStep = Time.timeScale;

        Time.timeScale = 0;
    }
    public void Stop() 
    {
        Time.timeScale = 0f;
    }
    public void Continue() 
    {
        Time.timeScale = timeStep;
    }
    public void Open(GameObject obj) 
    {
        obj.SetActive(true);
    }
    public void Close(GameObject obj) 
    {
        obj?.SetActive(false);
    }
}
