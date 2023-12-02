using UnityEngine;
using System.Collections;
using TMPro;

public class AutoMove : MonoBehaviour
{
    public float speed = 5f; // Kecepatan pergerakan objek
    public GameObject box;

    void Update()
    {
        MoveForward();
    }

    void MoveForward()
    {
        // Pergerakan objek ke depan menggunakan transform.Translate
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider obj)
    {
        if (obj.name == "box")
        {
            // ...

            // Increase speed based on the score
            speed += 1.5f; // Adjust this value based on your preference

            // ...

            UpdateBoxAnimatorSpeed(); // Call the function to update the box animator speed
        }
    }

    void UpdateBoxAnimatorSpeed()
    {
        // Adjust the animator speed based on the updated speed value
        float animatorSpeed = Mathf.Clamp(speed / 5.0f, 1.0f, 3.0f); // Adjust the range based on your preference
        box.GetComponent<Animator>().speed = animatorSpeed;
    }
}