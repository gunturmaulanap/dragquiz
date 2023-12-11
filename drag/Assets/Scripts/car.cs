using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Car : MonoBehaviour
{
    public GameObject box, gameover, congratulation;
    public TMP_Text questionDisplay, scoreDisplay;
    public float speed, rotationAngle;
    public AutoMoveBW autoMoveScript;

    public string[] questions, correctAnswers;
    string[] currentAnswers;
    int questionIndex = -1;
    int score = 0;

    private const float MaxSpeed = 5.0f;
    private const float MinAnimatorSpeed = 1.0f;
    private const float MaxAnimatorSpeed = 3.0f;

    void Start()
    {
        autoMoveScript = GameObject.FindObjectOfType<AutoMoveBW>();
        StartCoroutine(NextQuestion());
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            MoveCar(-speed, -rotationAngle);
        else if (Input.GetKey(KeyCode.RightArrow))
            MoveCar(speed, rotationAngle);
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            StopCar();
    }

    void MoveCar(float velocity, float rotation)
    {
        GetComponent<Rigidbody>().velocity = new Vector3(velocity, 0, 0);
        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    void StopCar()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.name == "box")
        {
            if (obj.transform.GetChild(0).GetComponent<TMP_Text>().text == currentAnswers[0])
            {
                UpdateScore(10);
                GetComponent<AudioSource>().Play();
                UpdateSpeed();

            }
            else
            {
                gameover.SetActive(true);
                Time.timeScale = 0;
            }
            DisableBoxColliders(obj.transform.parent);
            obj.gameObject.SetActive(false);
            StartCoroutine(NextQuestion());
        }
    }

    void UpdateScore(int points)
    {
        score += points;
        scoreDisplay.text = score.ToString();
    }

    void UpdateSpeed()
    {
        speed += 1f;
        UpdateBoxAnimatorSpeed();
    }

    void UpdateBoxAnimatorSpeed()
    {
        float animatorSpeed = Mathf.Clamp(speed / MaxSpeed, MinAnimatorSpeed, MaxAnimatorSpeed);
        box.GetComponent<Animator>().speed = animatorSpeed;
    }

    void DisableBoxColliders(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator NextQuestion()
    {
        yield return new WaitForSeconds(1.5f);

        questionIndex++;

        if (questionIndex < questions.Length)
        {
            DisplayQuestion();
        }
        else
        {
            Time.timeScale = 0;
            congratulation.SetActive(true);
        }
    }

    void DisplayQuestion()
    {
        questionDisplay.transform.parent.gameObject.SetActive(true);
        questionDisplay.text = questions[questionIndex];

        box.GetComponent<Animator>().enabled = true;
        box.GetComponent<Animator>().Play(0);

        currentAnswers = correctAnswers[questionIndex].Split('|');

        InitializeBoxes();
    }

    void InitializeBoxes()
    {
        for (int i = 0; i < box.transform.childCount; i++)
        {
            box.transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = "";
            box.transform.GetChild(i).gameObject.SetActive(true);
            box.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
        }

        AssignAnswersToBoxes();
    }

    void AssignAnswersToBoxes()
    {
        for (int i = 0; i < box.transform.childCount; i++)
        {
            int index;
            do
            {
                index = (int)Random.Range(0, box.transform.childCount);
            } while (box.transform.GetChild(index).GetChild(0).GetComponent<TMP_Text>().text != "");
            box.transform.GetChild(index).GetChild(0).GetComponent<TMP_Text>().text = currentAnswers[i];
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
