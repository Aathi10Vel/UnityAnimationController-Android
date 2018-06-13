using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {
    public Animator animBrute;
    public CharacterController bruteControl;
    public Vector3 bruteMovement = Vector3.zero;

    public float verticalMove;
    public float horizontalMove;
    public float spriteSpeed;

    public float playerSpeed=5.0f;
    public float playerGravity = 20.0f;
    public float playerVelocity = 0.0f;
    public float playerJumpSpeed = 1.0f;
    public bool actionPerform;

    public float getSpeed;
    public float getDirection;

    public bool spriteToggle;
    public Button spriteButton;
    public Button jumpButton;
    public Sprite charRun, charWalk;

    // Use this for initialization
    void Start () {
        spriteToggle = false;
        actionPerform = false;
        animBrute = GetComponent<Animator>();
        bruteControl = GetComponent<CharacterController>();
        spriteButton.image.overrideSprite = charRun;
	}
	
	// Update is called once per frame
	void Update () {

        playerMovement();
        playerAnimationController();
	}

    public void playerAnimationController() {
        getSpeed = animBrute.GetFloat("Speed");
        getDirection = animBrute.GetFloat("Direction");

        verticalMove = CrossPlatformInputManager.GetAxis("Vertical");
        horizontalMove = CrossPlatformInputManager.GetAxis("Horizontal");

        spriteButton.onClick.AddListener(spriteRunning);

        if (spriteToggle)
        {
            if (getSpeed != 0 || getDirection != 0)
            {
                spriteSpeed = 0.2f;
                playerSpeed = 10.0f;
            }
            else {
                spriteSpeed = 0.0f;
                playerSpeed = 5.0f;
            }
        }

        if(getSpeed ==0 && getDirection == 0)
        {
            spriteSpeed = 0.0f;
            actionPerform = false;
        }
    }

    public void playerMovement() {
        bruteMovement = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0, CrossPlatformInputManager.GetAxis("Vertical"));
        bruteMovement = transform.TransformDirection(bruteMovement);
        playerVelocity -= playerGravity * Time.deltaTime;
        bruteMovement.y = playerVelocity;
        bruteMovement *= playerSpeed;
        bruteControl.Move(bruteMovement * Time.deltaTime);
        actionPerform = true;

        jumpButton.onClick.AddListener(bruteJump);
    }

    public void LateUpdate()
    {
        animBrute.SetFloat("Speed", verticalMove);
        animBrute.SetFloat("Direction", horizontalMove);
        animBrute.SetFloat("Sprite",spriteSpeed);
        animBrute.SetFloat("JumpSpeed", playerVelocity);
    }

    public void spriteRunning() {
        if (!spriteToggle)
        {
            spriteToggle = true;
            spriteButton.image.overrideSprite = charWalk;
        }
        else
        {
            spriteToggle = false;
            spriteButton.image.overrideSprite = charRun;
        }
    }

    public void bruteJump()
    {
        if (bruteControl.isGrounded)
        {
            playerVelocity = playerJumpSpeed;
        }
    }
}
