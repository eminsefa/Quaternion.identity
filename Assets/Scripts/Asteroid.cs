using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Vector3 randomInUnit;
    public float rotateSpeed=5f;
    float moveSpeed;

    Rigidbody2D rb;

    IEnumerator Start()
    {
        while (!GameEngine.instance.timerStarted)
        {
            yield return null;
        }
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(0.5f, 2.0f);
        rb.velocity = new Vector2(-1, Random.Range(-0.5f, 0.5f)) * moveSpeed;

        randomInUnit = new Vector3(0, 0, Random.insideUnitSphere.z);
    }
    void Update()
    {
        transform.Rotate(randomInUnit * rotateSpeed);
       
    }
    
}
