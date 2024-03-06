using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BallSpawner : MonoBehaviour
{
    public Sprite spritePelota;
    public Vector2 minSpawnPoint;
    public Vector2 maxSpawnPoint;


    public float radioMinimo = 0.5f;
    public float radioMaximo = 1.5f;
    public float fuerza = 1000f;

    public float gravedad = 1f;
    public float rebote = 0.8f;
    public float friccion = 1f;
    public float spawnInterval = 10f;
    public int maxBalls = 10;

    private int ballsSpawned = 0;
    private float timer = 0f;


    void Start()
    {


    }
    void SpawnBall()
    {



        Vector2 spawnPosition = new Vector2(Random.Range(minSpawnPoint.x, maxSpawnPoint.x),
                                            Random.Range(minSpawnPoint.y, maxSpawnPoint.y));

        Color colorAleatorio = new Color(Random.value, Random.value, Random.value);


        GameObject nuevaPelota = new GameObject("Pelota");

        SpriteRenderer spriteRenderer = nuevaPelota.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = spritePelota;



        if (spriteRenderer.sprite.name.Contains("Circle"))
        {
            CircleCollider2D collider = nuevaPelota.AddComponent<CircleCollider2D>();
            collider.isTrigger = false;
            collider.sharedMaterial = new PhysicsMaterial2D
            {
                friction = friccion,
                bounciness = rebote
            };
            collider.usedByEffector = true;
            Debug.Log(spriteRenderer.sprite);
        }
        else if (spriteRenderer.sprite.name.Contains("Square"))
        {
            BoxCollider2D collider = nuevaPelota.AddComponent<BoxCollider2D>();
            collider.isTrigger = false;
            collider.sharedMaterial = new PhysicsMaterial2D
            {
                friction = friccion,
                bounciness = rebote
            };
            collider.usedByEffector = true;
            Debug.Log(spriteRenderer.sprite);
        }

        Rigidbody2D rb = nuevaPelota.AddComponent<Rigidbody2D>();
        rb.gravityScale = gravedad;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        nuevaPelota.GetComponent<Renderer>().material.color = colorAleatorio;

        nuevaPelota.transform.localScale = Vector3.one * Random.Range(radioMinimo, radioMaximo);

        nuevaPelota.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(fuerza * -1, fuerza), Random.Range(fuerza * -1, fuerza)));

        nuevaPelota.transform.position = spawnPosition;




        ballsSpawned++;



    }
    void Update()
    {
        timer += Time.deltaTime;

        if (ballsSpawned < maxBalls && timer >= spawnInterval)
        {
            SpawnBall();
            timer = 0f;
        }
    }
}
