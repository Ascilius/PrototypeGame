using JetBrains.Annotations;
using System;
using System.IO;
using Unity.Loading;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataMgmt : MonoBehaviour {

  private static bool saving = false; // mitigating save spamming
  private static bool loading = false;

  private static string savePath = System.IO.Directory.GetCurrentDirectory() + "\\Saves\\save0";

  // loads game state at given path, returns success through a boolean
  public static bool Load() {
    // send string to script, have user define loading behavior
    // or define all loading behavior here

    FileStream saveFile = new FileStream(savePath, FileMode.Open);
    BinaryReader reader = new BinaryReader(saveFile);
    string snakeJSON = reader.ReadString();
    
    print("Debug: snakeJSON: " + snakeJSON);

    GameObject snake = GameObject.Find("Snake");
    JsonUtility.FromJsonOverwrite(snakeJSON, snake.GetComponent<SnakeController>());
    // SnakeController SCNew = snake.GetComponent<SnakeController>();
    
    reader.Close();
    saveFile.Close();

    // copying data
    /*
    SCNew.length = SCOld.length;
    SCNew.snakeBodySprite = SCOld.snakeBodySprite;
    SCNew.movementSpeed = SCOld.movementSpeed;
    SCNew.intervalTime = SCOld.intervalTime;
    */

    return true;
  }

  // saves current game state into Saves/save, returns success through a boolean
  public static bool Save() {
    // GameObject newObj = Type.GetType("GameObject");

    /*
    GameObject[] objects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
    print(objects.Length + " objects");
    foreach (GameObject obj in objects)
      print(obj.ToString());
    */

    // saving snake state
    GameObject snake = GameObject.Find("Snake");
    SnakeController snakeController = snake.GetComponent<SnakeController>();
    string snakeJSON = JsonUtility.ToJson(snakeController);

    Debug.Log("snakeJSON: " + snakeJSON);

    /*
    // saving food location
    GameObject food = GameObject.Find("Food");
    FoodBehavior foodBehavior = food.GetComponent<FoodBehavior>();
    string foodJSON = foodBehavior.getSaveData();

    Debug.Log("foodJSON: " + foodJSON);
    */

    // writing to file
    Debug.Log("Saving to \"" + savePath + "\"...");
    FileStream saveFile = new FileStream(savePath, FileMode.Create);
    BinaryWriter writer = new BinaryWriter(saveFile);
    writer.Write(snakeJSON); // NOTE: need to create "Save/" directory for this to work
    // writer.Write(foodJSON);

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

    else if (saving && (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.X))) {
      saving = false;
    }

    if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.V) && !loading) {
      Time.timeScale = 0f;
      loading = true;
      Load();
      Time.timeScale = 1f;
    }

    else if (loading && (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.V))) {
      loading = false;
    }
  }

}
