using UnityEngine;
using System.Collections;
using TMPro;

public class AutoMoveBW : MonoBehaviour
{
    public float speed = 5f; // Kecepatan pergerakan objek
    public GameObject box;
    private int score = 0;

    void Update()
    {
        MoveBackward(); // Panggil fungsi MoveBackward
    }

    void MoveBackward()
    {
        // Pergerakan objek ke belakang menggunakan transform.Translate
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.name == "box")
        {
            speed += 1.5f;
            UpdateBoxAnimatorSpeed();

            // Check if the score is a multiple of 10
            if (score % 10 == 0)
            {
                IncreaseSpeed();
            }
        }
    }

    void UpdateBoxAnimatorSpeed()
    {
        // Adjust the animator speed based on the updated speed value
        float animatorSpeed = Mathf.Clamp(speed / 5.0f, 1.0f, 3.0f); // Adjust the range based on your preference
        box.GetComponent<Animator>().speed = animatorSpeed;
    }

    public void IncreaseSpeed()
    {
        // Peningkatan kecepatan setiap skor bertambah 10
        speed += 1.0f;
        Debug.Log("Speed increased! New speed: " + speed);
        UpdateBoxAnimatorSpeed();
    }

    // Assume you have a method to update the score, for example:
    public void UpdateScore()
    {
        score++;
        Debug.Log("Score increased! New score: " + score);
    }
}
