using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SnakeController : MonoBehaviour {

  // snake
  private GameObject head;
  private List<GameObject> snake;
  [SerializeField] public uint length;
  [SerializeField] public Sprite snakeBodySprite;

  // movement
  [SerializeField] public float movementSpeed = 1f;
  private Vector2 nullDirection = new Vector2(0f, 0f); 
  private Vector2 movementDirection;

  // timing
  private float startTime;
  [SerializeField] public float intervalTime = 0.5f;

  // food
  private GameObject food;

  GameObject newPart(string name, Vector2 pos) {
    // print("Debug: New part at " + pos.ToString());

    GameObject newPart = new GameObject(name);
    newPart.AddComponent<SpriteRenderer>();
    newPart.transform.parent = this.transform; // setting newPart as part of the snake

    // adding sprite
    SpriteRenderer sr = newPart.GetComponent<SpriteRenderer>();
    sr.sprite = snakeBodySprite;

    // setting position
    newPart.transform.position = pos;

    // tracking new snake part
    snake.Add(newPart);
    length++;

    return newPart;
  }

  void destroyTail() {
    GameObject.Destroy(snake[0]); // destorying fifo
    snake.Remove(snake[0]); // removing tracking from internal list
    length--;
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    snake = new List<GameObject>();
    length = 0;

    head = newPart("Head", new Vector2(0f, 0f));

    // tracking food game object
    food = GameObject.Find("Food");

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
    
    updatedDirection.Normalize();

    // rejecting no direction change and reverse direction
    Vector2 reverseDirection = new Vector2(movementDirection.x * -1, movementDirection.y * -1);
    // print("Debug: movementDirection: " + movementDirection);
    // print("Debug: reverseDirection: " + reverseDirection);
    if (!(updatedDirection.Equals(nullDirection) || updatedDirection.Equals(reverseDirection))) {
      movementDirection = updatedDirection;
      // print("Debug: Direction updated: " + movementDirection.ToString());
    }
  }

  private void FixedUpdate() {
    float totalTime = Time.time - startTime;
    if ((totalTime > intervalTime) && (!movementDirection.Equals(nullDirection))) {
      Vector3 loc = head.transform.position + (Vector3) (movementDirection * movementSpeed);
      startTime = Time.time; // reset timer

      // move snake
      head.name = "Body";
      head = newPart("Head", loc);

      // eating
      Vector2 foodLoc = food.transform.position;
      if (head.transform.position.Equals(foodLoc))
        food.tag = "Eaten"; // marking food as eaten
      else
        destroyTail();
    }
  }

  public List<GameObject> getSnake()
  {
    return snake;
  }

}
