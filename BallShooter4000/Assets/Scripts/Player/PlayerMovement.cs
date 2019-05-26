using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.
    Transform player;
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;
    PlayerHealth playerHealth;          // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    int floorMask;
    bool reachTargetOne, reachTargetTwo, reachTargetThree, reachTargetFour;                                    // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        reachTargetOne = false;
        reachTargetTwo = true;
        reachTargetThree = true;
        reachTargetFour = true;
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
        
        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (playerHealth.currentHealth > 0 )
        {
          
            if (nav.isOnNavMesh && reachTargetOne == false)
            {
                nav.SetDestination(new Vector3 (28, 0, 0));
                if (Vector3.Distance(new Vector3(28, 0, 0), player.position) < 3)
                {
                    reachTargetOne = true;
                    reachTargetTwo = false;
                }
            }
            if (nav.isOnNavMesh && reachTargetTwo == false)
            {
                nav.SetDestination(new Vector3(0, 0, 24));
                if ( Vector3.Distance(new Vector3(0, 0, 24),player.position) < 3)
                {
                    reachTargetTwo = true;
                    reachTargetThree = false;
                }
            }
            if (nav.isOnNavMesh && reachTargetThree == false)
            {
                nav.SetDestination(new Vector3(-28, 0, 0));
                if (Vector3.Distance(new Vector3(-28, 0, 0), player.position) < 3)
                {
                    reachTargetThree = true;
                    reachTargetFour = false;
                }
            }
            if (nav.isOnNavMesh && reachTargetFour == false)
            {
                nav.SetDestination(new Vector3(0, 0, -28));
                if (Vector3.Distance(new Vector3(0, 0, -28), player.position) < 3)
                {
                    reachTargetFour = true;
                    reachTargetOne = false;
                }
            }

        }
        else
        {
            nav.enabled = false;
        }
    }


    ////void FixedUpdate()
    ////{
    ////    // Store the input axes.
    ////    float h = Input.GetAxisRaw("Horizontal");
    ////    float v = Input.GetAxisRaw("Vertical");

    ////    // Move the player around the scene.
    ////    Move(h, v);

    ////    // Turn the player to face the mouse cursor.
    ////    Turning();

    ////    // Animate the player.
    ////    Animating(h, v);
    ////}

    ////void Move(float h, float v)
    ////{
    ////    // Set the movement vector based on the axis input.
    ////    movement.Set(h, 0f, v);

    ////    // Normalise the movement vector and make it proportional to the speed per second.
    ////    movement = movement.normalized * speed * Time.deltaTime;

    ////    // Move the player to it's current position plus the movement.
    ////    playerRigidbody.MovePosition(transform.position + movement);
    ////}

    ////void Turning()
    ////{
    ////    // Create a ray from the mouse cursor on screen in the direction of the camera.
    ////    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    ////    // Create a RaycastHit variable to store information about what was hit by the ray.
    ////    RaycastHit floorHit;

    ////    // Perform the raycast and if it hits something on the floor layer...
    ////    if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
    ////    {
    ////        // Create a vector from the player to the point on the floor the raycast from the mouse hit.
    ////        Vector3 playerToMouse = floorHit.point - transform.position;

    ////        // Ensure the vector is entirely along the floor plane.
    ////        playerToMouse.y = 0f;

    ////        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
    ////        Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

    ////        // Set the player's rotation to this new rotation.
    ////        playerRigidbody.MoveRotation(newRotation);
    ////    }
    ////}

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        //anim.SetBool("IsWalking", walking);
    }
}
