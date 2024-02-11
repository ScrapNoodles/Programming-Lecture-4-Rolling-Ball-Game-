using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;

    // SoundFX variables
    [SerializeField] private AudioSource _pickupSFX;
    [SerializeField] private AudioSource _winSFX;
    [SerializeField] private AudioSource _loseSFX;

    // Variables for timer
    // Source: https://www.youtube.com/watch?v=o0j7PdU88a4
    float currentTime = 0f;
    float startingTime = 10f;

    [SerializeField] private TextMeshProUGUI countdownText;

    // Variable to keep track of collected "PickUp" objects.
    private int count;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;

    // UI object to display winning text.
    public GameObject winTextObject;

    

    // Start is called before the first frame update.
    void Start()
    {

        currentTime = startingTime;

        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

        // Initialize count to zero.
        count = 0;

        // Update the count display.
        SetCountText();

        // Initially set the win text to be inactive.
        winTextObject.SetActive(false);
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    // lose condition
    private bool _losecondition = false;

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime; ;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            _losecondition = true;
            countdownText.text = "YOU LOST";

        }
    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

            // Increment the count of "PickUp" objects collected.
            count = count + 1;

            // Update the count display.
            SetCountText();

            // Plays sound if player picks up a cube
            _pickupSFX.Play();
        }
    }

    
    // Function to update the displayed count of "PickUp" objects collected.
    void SetCountText()
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (count >= 11 && _losecondition == false)
        {
            // Display the win text.
            winTextObject.SetActive(true);

            // win SFX
            _winSFX.Play();
        }
        else if (_losecondition)
        {
            countText.text = "Count: " + "-1023012930123910239012931092049120491023190293019230192049109510510912391238918239182398198239182938192839145092035902385928359239852384829374";
            _loseSFX.Play();
        }
    }
}
