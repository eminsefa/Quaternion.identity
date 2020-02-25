using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndCreate : MonoBehaviour
{
    public GameObject[] asteroids;
    public Camera cam;
   
   
    public GameObject RandomAsteroid()
    {
        int i = Random.Range(0, 6);
        return asteroids[i];
    }
    public Vector2 RandomSpawnPosition()
    {
        float xPos=Random.Range(cam.transform.position.x * 1.5f, cam.transform.position.x * 2f);
        float yPos = Random.Range(0, cam.transform.position.y * 2);
        return new Vector2(xPos, yPos);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        
        Destroy(collision.gameObject);
        GameObject Asteroid = Instantiate(RandomAsteroid(), RandomSpawnPosition(),transform.rotation);
    }

}
