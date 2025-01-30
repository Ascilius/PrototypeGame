using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

  private GameObject head;
  private List<GameObject> snake;
  private uint length;
  [SerializeField] private Sprite snakeBodySprite;
  [SerializeField] private Vector2 loc;

  // movement
  [SerializeField] private float movementSpeed = 1f;
  private Vector2 nullDirection = new Vector2(0f, 0f);
  private Vector2 movementDirection;

  // timing
  private float startTime;
  [SerializeField] private float intervalTime = 0.5f;

  // food
  private GameObject food;

  GameObject newPart(string name, Vector2 pos) {
    print("Debug: New part at " + pos.ToString());

    GameObject newPart = new GameObject(name);
    newPart.AddComponent<SpriteRenderer>();
    newPart.AddComponent<Rigidbody2D>();
    newPart.transform.parent = this.transform; // setting newPart as part of the snake

    // adding sprite
    SpriteRenderer sr = newPart.GetComponent<SpriteRenderer>();
    sr.sprite = snakeBodySprite;

    // disabling gravity / setting position
    Rigidbody2D rb = newPart.GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f;
    rb.transform.position = pos;

    // tracking new snake part
    snake.Add(newPart);
    length++;

    // tracking food game object
    food = GameObject.Find("Food");

    return newPart;
  }

  void destroyTail() {
    GameObject.Destroy(snake[0]); // destorying fifo
    snake.Remove(snake[0]); // removing tracking from internal list
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    snake = new List<GameObject>();
    length = 0;
    loc = new Vector2(0f, 0f);

    head = newPart("Head", loc);

    startTime = Time.time;
  }

  // Update is called once per frame
  void Update() {
    // determining input direction
    float hor = Input.GetAxis("Horizontal");
    float ver = Input.GetAxis("Vertical");

    Vector2 updatedDirection;
    if (Mathf.Abs(hor) > Mathf.Abs(ver))
      updatedDirection = new Vector2(hor, 0f);
    else
      updatedDirection = new Vector2(0f, ver);

    // rejecting no direction change
    if (!updatedDirection.Equals(nullDirection))
    {
      movementDirection = updatedDirection;
      print("Debug: Direction updated: " + movementDirection.ToString());
    }
  }

  private void FixedUpdate() {
    float totalTime = Time.time - startTime;
    if ((totalTime > intervalTime) && (!movementDirection.Equals(nullDirection))) {
      loc += movementDirection.normalized * movementSpeed;
      startTime = Time.time; // reset timer

      // move snake
      head.name = "Body";
      head = newPart("Head", loc);

      // eating
      Vector2 foodLoc = food.transform.position;
      if (loc.Equals(foodLoc))
        food.tag = "Eaten"; // marking food as eaten
      else
        destroyTail();
    }
  }
}
