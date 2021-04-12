using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startpos;

    public GameObject cam;
    public float parallaxEffect;


    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect)); // Distancia que o player se moveu em relacao à camera.
        float dist = (cam.transform.position.x * parallaxEffect); // Distancia que o player se moveu no 'world space'.

        // Mover a camera
        transform.position = new Vector3(startpos + dist, 0, transform.position.z);

        if(temp > startpos + length)
        {
            startpos += length;
        } 
        else if(temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
