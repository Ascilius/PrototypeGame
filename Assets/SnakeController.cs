using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

  GameObject head;
  List<GameObject> snake;
  uint length;
  [SerializeField] Sprite snakeBodySprite;
  [SerializeField] Vector2 loc;

  GameObject newPart(string name, Vector2 pos) {
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
    rb.position = pos;

    // tracking new snake part
    snake.Add(newPart);
    length++;

    return newPart;
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    snake = new List<GameObject>();
    length = 0;
    loc = new Vector2(0f, 0f);

    head = newPart("Head", loc);
  }

  // Update is called once per frame
  void Update() {
    
  }
}
