using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI researchText;
    [SerializeField] GameObject escapeMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject ui;
    bool escapeMenuOpen = false;
    float gameSpeed = 0;

    private void Start()
    {
        gameSpeed = Time.timeScale;
    }
    public void UpdateMoneyText(int num) 
    {
        moneyText.text = num.ToString();
    }
    public void UpdateResearchText(int num)
    {
        researchText.text = num.ToString();
    }
    public void Resume()
    {
        escapeMenu.SetActive(false);
        escapeMenuOpen = false;
        ui.SetActive(true);
        Time.timeScale = gameSpeed;
    }
    public void Quit() 
    {
        Application.Quit();
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !escapeMenuOpen)
        {
            escapeMenu.SetActive(true);
            escapeMenuOpen = true;
            ui.SetActive(false);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (settingsMenu.activeInHierarchy)
            {
                return;
            }
            Resume(); 
        }
    }
    public void Open(GameObject gameObject) 
    {
        gameObject.SetActive(true);
    }
    public void Close(GameObject gameObject) 
    {
        gameObject.SetActive(false);
    }
}
