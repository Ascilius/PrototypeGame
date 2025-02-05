using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodBehavior : MonoBehaviour {

  private SnakeController snakeObject;

  // make sure new food location does not collide with snake
  void newPos() {
    bool pass = false;
    while (!pass) {
      this.transform.position = new Vector2((int)(Random.value * 9) - 4, (int)(Random.value * 5) - 2);
      
      List<GameObject> snake = snakeObject.getSnake();
      pass = true;
      foreach (GameObject body in snake)
      {
        if (body.transform.position.Equals(this.transform.position)) {
          pass = false;
          break;
        }
      }
    }
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    this.AddComponent<Rigidbody2D>();
    Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f;
    snakeObject = GameObject.Find("Snake").GetComponent<SnakeController>();
    newPos();
  }

  // Update is called once per frame
  void Update() {
    if (this.tag.Equals("Eaten")) {
      newPos();
      this.tag = "Not Eaten"; // marking food as not eaten
    }
  }
  
}
