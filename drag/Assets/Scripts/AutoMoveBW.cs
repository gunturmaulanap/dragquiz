using UnityEngine;
using System.Collections;
using TMPro;

public class AutoMoveBW : MonoBehaviour
{
    public float speed = 5f; // Kecepatan pergerakan objek
    public GameObject box;

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
        }
    }

    void UpdateBoxAnimatorSpeed()
    {
        // Adjust the animator speed based on the updated speed value
        float animatorSpeed = Mathf.Clamp(speed / 5.0f, 1.0f, 3.0f); // Adjust the range based on your preference
        box.GetComponent<Animator>().speed = animatorSpeed;
    }
}
