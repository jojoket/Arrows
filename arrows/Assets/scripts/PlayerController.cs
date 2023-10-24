using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    public float speed = 5f;

    //Components
    Rigidbody2D rigidBody2D;

    //Values
    Vector2 inputDir;
    Vector2 MousePos;
    public float Orientation = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Mouse
        MousePos = Mouse.current.position.ReadValue();
        MousePos = Camera.main.ScreenToWorldPoint(MousePos);

        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        inputDir = (MousePos - position);

        //Applying Forces
        rigidBody2D.AddForce(inputDir * speed * 100 * Time.deltaTime);
        Orientation = Vector2.Angle(inputDir, Vector2.up);
        Orientation = inputDir.x > 0 ? (360-Orientation) : Orientation;
        rigidBody2D.MoveRotation(Orientation);

    }

    public void applyInputDir(InputAction.CallbackContext input)
    {
        inputDir = input.ReadValue<Vector2>();
    }
}
