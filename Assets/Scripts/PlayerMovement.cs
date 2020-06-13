using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;

    // Start is called before the first frame update
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change != Vector3.zero) {
            MoveCharacter();
        }
    }

    void MoveCharacter() {
        // multiplying by Time.deltaTime makes this a very small amount each frame
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }
}
