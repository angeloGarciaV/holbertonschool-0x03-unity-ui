using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 500f;
    public float boost = 1000f;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public GameObject winLoseUI;
    public Text winLoseText;

    private bool isMoving;
    private float originalSpeed;
    private int score = 0;
    
    void Start()
    {
        originalSpeed = speed;
    }
    void Update()
    {        
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            isMoving = true;
            rb.AddForce(0, 0, speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isMoving = true;
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            isMoving = true;
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isMoving = true;
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) && isMoving == true)
        {
            speed = boost;
        }else
        {
            speed = originalSpeed;
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
        isMoving = false;

        if (health == 0)
        {
            winLoseUI.GetComponent<Image>().color = Color.red; 
            winLoseText.text = "Game Over!";
            winLoseUI.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            score++;
            SetScoreText();
        }
        
        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
        }
        
        if (other.CompareTag("Goal"))
        {
            winLoseUI.GetComponent<Image>().color = Color.green; 
            winLoseText.color = Color.black;
            winLoseText.text = "You Win!";
            winLoseUI.SetActive(true);
        }
    }
    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }
    
    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
