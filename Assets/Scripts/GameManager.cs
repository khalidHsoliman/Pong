using System.Collections; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager gm = null;

    // UI Elements
    public Text GameOver; 
    public Text PlayerScore;
    public Text AIScore;

    public Button RestartButton;
    public GameObject Panel;
    //

    private int playerScore = 0;
    private int aiScore = 0;
    
    private bool gameIsOver = false; 

    private void Start()
    {
        if (gm == null)
            gm = this;
        else
            Destroy(gameObject); 


    }

    private void Update()
    {
        if(gameIsOver)
        {
            if (int.Parse(PlayerScore.text) > int.Parse(AIScore.text))
                StartCoroutine("Win");

            else
                StartCoroutine("Lose"); 
        }
    }

    public void IncreaseScore(GameObject obj)
    {
        if (obj.CompareTag("LeftGoal"))
        {
            playerScore++; 
            PlayerScore.text = playerScore.ToString();
        }

        else if(obj.CompareTag("RightGoal"))
        {
            aiScore++;
            AIScore.text = aiScore.ToString();
        }

        if (playerScore == 11 || aiScore == 11)
            gameIsOver = true; 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString()); 
    }

    
    IEnumerator Win()
    {
        Time.timeScale = 0.1f; 
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0.0f;
        yield return new WaitForSeconds(1.0f);

        GameOver.text = "YOU WON!";
        Panel.SetActive(true);

        while(Panel.GetComponent<Image>().color.a < 255)
        {
            Panel.GetComponent<Image>().color += new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.05f);
        }

        RestartButton.interactable = true; 
    }

    IEnumerator Lose()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 0.0f;
        yield return new WaitForSeconds(1.0f);

        GameOver.text = "YOU LOST!";
        Panel.SetActive(true);

        while (Panel.GetComponent<Image>().color.a < 255)
        {
            Panel.GetComponent<Image>().color += new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.05f);
        }

        RestartButton.interactable = true;
    }
}
