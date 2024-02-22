using TMPro;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] scoreText;
    private void Start()
    {

        for (int i = 0; i < scoreText.Length; i++)
        {
            string name = "Level" + (1 + i);
            try 
            {
                scoreText[i].text = "Score: " + PlayerPrefs.GetInt(name);
            }
            catch 
            {
                Debug.Log("score does not exist");
                scoreText[i].text = "Score: 0";
            }
        }
    }
    public void ResetScore()
    {
        for (int i = 0; i < scoreText.Length; i++)
        {
            string name = "Level" + (1 + i);
            try
            {
                PlayerPrefs.SetInt(name, 0);
                scoreText[i].text = "Score: " + PlayerPrefs.GetInt(name);
            }
            catch
            {
                Debug.Log("score does not exist");
                scoreText[i].text = "Score: 0";
            }
        }
    }
}


