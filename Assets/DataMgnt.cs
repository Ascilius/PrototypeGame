using JetBrains.Annotations;
using System;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataMgmt : MonoBehaviour {

  private static bool saving = false; // mitigating save spamming

  // TODO: loads game state at given path, returns success through a boolean
  public static bool Load() {
    return true;
  }

  // saves current game state into Saves/save, returns success through a boolean
  public static bool Save() {
    // saving snake state
    GameObject snake = GameObject.Find("Snake");
    SnakeController snakeController = snake.GetComponent<SnakeController>();
    string snakeJSON = JsonUtility.ToJson(snakeController);

    Debug.Log("snakeJSON: " + snakeJSON);

    // saving food location
    GameObject food = GameObject.Find("Food");
    FoodBehavior foodBehavior = food.GetComponent<FoodBehavior>();
    string foodJSON = foodBehavior.getSaveData();

    Debug.Log("foodJSON: " + foodJSON);

    // writing to file
    string savePath = System.IO.Directory.GetCurrentDirectory() + "\\Saves\\save0";
    Debug.Log("Saving to \"" + savePath + "\"...");
    FileStream saveFile = new FileStream(savePath, FileMode.Create);
    BinaryWriter writer = new BinaryWriter(saveFile);
    writer.Write(snakeJSON); // NOTE: need to create "Save/" directory for this to work
    writer.Write(foodJSON);

    writer.Close();
    saveFile.Close();

    return true;
  }

  void Update() {
    // Debug.Log("foo");
    if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.X) && !saving) {
      Time.timeScale = 0f;
      saving = true;
      Save();
      Time.timeScale = 1f;
    }

    else if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.X)) {
      saving = false;
    }
  }

}
