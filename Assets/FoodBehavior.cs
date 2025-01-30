using Unity.VisualScripting;
using UnityEngine;

public class FoodBehavior : MonoBehaviour {

  [SerializeField] private Vector2 loc;
    
  void newPos() {
    loc = new Vector2((int)(Random.value * 9) - 4, (int)(Random.value * 5) - 2);
    this.transform.position = loc;
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    this.AddComponent<Rigidbody2D>();
    Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
    rb.gravityScale = 0f;
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
