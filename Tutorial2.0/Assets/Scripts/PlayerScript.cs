using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
     private Rigidbody2D rd2d;

      public float speed;

      public Text score;
      public Text livesText;
    private int scoreValue;
    private int livesValue;

      public GameObject WinTextObject;
      public GameObject LoseTextObject;

      public AudioClip musicClipOne;
      public AudioClip musicClipTwo;
      public AudioSource musicSource;
      public Animator AnimController;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
         scoreValue = 0;

         rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;

        SetCountText();
        WinTextObject.SetActive(false);

        SetCountText();
        LoseTextObject.SetActive(false);

         musicSource.clip = musicClipOne;
         musicSource.Play();
         AnimController = GetComponent<Animator>();

    }
    void Update ()
    {
        SetCountText();
    }
    void SetCountText()
    {
    score.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 8)
        {
            WinTextObject.SetActive(true);
            Destroy(gameObject);
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();

        }

        livesText.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        AnimController.SetFloat("move x",hozMovement) ;
        AnimController.SetFloat("move y",vertMovement) ;
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                transform.position = new Vector3(43f, 0f, 0f);
            }
        }
      if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            livesText.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); 
                //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }

}
