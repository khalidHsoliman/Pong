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

    public bool gameIsOver = false;

    private int playerScore = 0;
    private int aiScore = 0;
    

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

        if (playerScore == 5 || aiScore == 5)
            gameIsOver = true; 
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    
    IEnumerator Win()
    {
        yield return new WaitForSeconds(0.5f);

        GameOver.text = "YOU WON!";
        Panel.SetActive(true);
        RestartButton.interactable = true;

        while (Panel.GetComponent<Image>().color.a < 254)
        {
            Panel.GetComponent<Image>().color += new Color32(0, 0, 0, 10);
            yield return new WaitForSeconds(0.5f);
        }

    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(0.5f);

        GameOver.text = "YOU LOST!";
        Panel.SetActive(true);
        RestartButton.interactable = true;

        while (Panel.GetComponent<Image>().color.a < 254)
        {
            Panel.GetComponent<Image>().color += new Color32(0, 0, 0, 10);
            yield return new WaitForSeconds(0.5f);
        }

    }
}
