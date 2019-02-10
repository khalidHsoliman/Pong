using System.Collections; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// This is the gamemanager used to control win/lose states and manage UI and keep track with the score
/// </summary>
public class GameManager : MonoBehaviour {

    // implementing Singleton pattern 
    // making only one static instance for the gamemanager
    public static GameManager gm = null;

    public GameObject Ball;
    public GameObject Player;
    public GameObject AI;
    public GameObject[] Borders; 

    // UI Elements
    public Text GameOver; 
    public Text PlayerScore;
    public Text AIScore;

    public Button RestartButton;
    public GameObject Panel;
    //

    public bool gameIsOver = false;

    private AudioSource audioSource; 

    private int playerScore = 0;
    private int aiScore = 0;

    private bool toggleColor = false;

    private float timeToToggle = 5.0f;
    private float timeTillShut = 0.0f;
    private float addForce = 20.0f;
    private float oldBallSpeed   = 0.0f;
    private float oldPlayerSpeed = 0.0f;
    private float oldAISpeed     = 0.0f;


    private void Start()
    {
        // Singleton
        if (gm == null)
            gm = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Speedup pickup timer
        if (toggleColor)
        {
            timeTillShut += Time.deltaTime;

            if (timeTillShut >= timeToToggle)
            {
                toggleColor = false;
                timeTillShut = 0.0f;

                ResetObj();
            }
        }

        // Win/Lose states
        if(gameIsOver)
        {
            if (int.Parse(PlayerScore.text) > int.Parse(AIScore.text))
                StartCoroutine("Win");

            else
                StartCoroutine("Lose"); 
        }
    }

    /// <summary>
    /// increases the score when scoring a goal
    /// </summary>
    /// <param name="obj"></param>
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

    /// <summary>
    /// Restart the scene ... refernced in the restart button
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    /// <summary>
    /// Called when the ball collides with the speedup pickup
    /// </summary>
    public void Speedup()
    {
        // this check is to handle the situition when two speedup pickups picked together
        if (toggleColor)
        {
            timeTillShut = 0.0f;

            ResetSpeed();
        }

        else
            toggleColor = true;

        // saves the old speed 
        oldBallSpeed   = Ball.GetComponent<BallBehaviour>().speed;
        oldPlayerSpeed = Player.GetComponent<PlayerController>().speed;
        oldAISpeed     = AI.GetComponent<AIPlayer>().speed;

        // increase the speed and change the ball color 
        Ball.GetComponent<BallBehaviour>().speed += 20;
        Ball.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0); 
        Ball.GetComponent<TrailRenderer>().startColor = new Color(1, 0, 0);
        Player.GetComponent<PlayerController>().speed += 20;
        AI.GetComponent<AIPlayer>().speed += 20;

        // speedup the bg music
        audioSource.pitch += 0.2f;
        
        // this coroutine is used to toggle the borders color 
        StartCoroutine("Speedy"); 
    }

    /// <summary>
    /// Called when the ball collides with the Magnet pickup
    /// </summary>
    public void ChangeDir()
    {
        // checks if the ball in the upper or lower half of the screen 
        if (Ball.transform.position.y > 0)
            Ball.GetComponent<Rigidbody2D>().velocity += new Vector2(0, addForce); 
        else
            Ball.GetComponent<Rigidbody2D>().velocity += new Vector2(0, -addForce);
    }

    /// <summary>
    /// Reset Objects states after the pickup time is finished 
    /// </summary>
    private void ResetObj()
    {
        ResetSpeed();

        Ball.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        Ball.GetComponent<TrailRenderer>().startColor = new Color(1, 1, 1);

        audioSource.pitch -= 0.2f;
    }

    /// <summary>
    /// Reset speed using old values
    /// </summary>
    private void ResetSpeed()
    {
        Ball.GetComponent<BallBehaviour>().speed      = oldBallSpeed;
        Player.GetComponent<PlayerController>().speed = oldPlayerSpeed;
        AI.GetComponent<AIPlayer>().speed             = oldAISpeed;
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

    IEnumerator Speedy()
    {
        while (toggleColor)
        {

            foreach (GameObject border in Borders)
            {
                border.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
            }

            yield return new WaitForSeconds(0.25f);

            foreach (GameObject border in Borders)
            {
                border.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
            }

            yield return new WaitForSeconds(0.25f);

        }
    }
}
