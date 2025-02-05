using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataMgmt : MonoBehaviour {

  // TODO: loads game state at given path, returns success through a boolean
  public static bool Load() {
    return true;
  }

  // saves current game state into Saves/save, returns success through a boolean
  public static bool Save() {
    SnakeController snake = GameObject.Find("Snake").GetComponent<SnakeController>();
    string snakeJSON = JsonUtility.ToJson(snake);

    Debug.Log("snakeJSON: " + snakeJSON);

    FoodBehavior food = GameObject.Find("Food").GetComponent<FoodBehavior>();
    string foodJSON = JsonUtility.ToJson(food);

    Debug.Log("foodJSON: " + foodJSON);

    return true;
  }

  void Update() {
    // Debug.Log("foo");
    if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.X))
      Save();
  }

}
