using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    private float leftBoundary;
    private float rightBoundary;
    private bool movingRight = true;


    public GameObject EnemyBullet;
    public GameObject FirePosition;

    [SerializeField]
    private BarStat healthBar;

    private NewBehaviourScript myHealth;

    private Animator animator;

    private void Awake()
    {
        myHealth = GetComponent<NewBehaviourScript>();
        healthBar.bar = GameObject.FindGameObjectWithTag("EnemyHealth").GetComponent<BarScript>();
        healthBar.Initialize();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        healthBar.MaxVal = myHealth.maxHealth;

        // Calculate the screen boundaries based on the camera's view
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * screenRatio;

        // Set the left and right boundaries
        leftBoundary = -cameraWidth;
        rightBoundary = cameraWidth;

        InvokeRepeating("FireBullet", 0f, 2f);
    }

    void Update()
    {
        healthBar.CurrentVal = myHealth.healt;


        if(myHealth.healt <= 0)
        {
            HandelDeath();
            return;
        }


        // Determine the direction based on the current position
        if (movingRight)
        {
            // Move right
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightBoundary)
            {
                movingRight = false;
            }
        }
        else
        {
            // Move left
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftBoundary)
            {
                movingRight = true;
            }
        }
    }

    void FireBullet()
    {
        if (EnemyBullet != null && FirePosition != null)
        {
            GameObject bullet = Instantiate(EnemyBullet);
            bullet.transform.position = FirePosition.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            myHealth.healt -= 10;
            Debug.Log("Enemy health after hit: " + myHealth.healt);
            Destroy(collision.gameObject);
            Debug.Log("Player bullet hit the enemy!");

        }
    }

    private void HandelDeath()
    {
        if(animator != null)
        {
            animator.SetBool("enemydie", true);
        }
        Destroy(gameObject, 1f);
        
    }

}
