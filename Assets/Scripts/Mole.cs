using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class Mole : MonoBehaviour
{
    #region Variables

    public static event Action MoleClicked;
    
    [SerializeField] private bool isMovingUp = false;
    [SerializeField] private float yPosition = 0.5f; 
    [SerializeField] private float speed = 3f;

    public bool IsMovingUp { set => isMovingUp = value; get => isMovingUp; }
    private Vector3 startingPosition;
    private float movedUpTimer = 2f;
    private Collider myCollider;

    #endregion




    private void Start()
    {
        startingPosition = transform.position;
        myCollider = GetComponent<Collider>();

    }
    // Update is called once per frame
    private void Update()
    {
        // check boolean
        // moveUp
        if (isMovingUp)
        {
            // start moving up
            if (yPosition > transform.position.y)
             {
                 MoveUp();
             }

            // stop moving up
            if (yPosition < transform.position.y) 
             {
                transform.position = new Vector3(startingPosition.x, yPosition, startingPosition.z);
                // decrease timer
                movedUpTimer -= Time.deltaTime;
             }
        }

        // else moveDown
        else
        {
            // if the mole is Up (it's position is more than the inital position)
            if (transform.position.y > startingPosition.y)
            {
                MoveDown();
                //transform.position = new Vector3(0, 0.5f, 0);
            }
            else
            {
                transform.position = new Vector3(startingPosition.x, startingPosition.y, startingPosition.z);
            }
        }

        
        // after 2 seconds make mole move down 
        if (movedUpTimer <= 0)
        {
            isMovingUp = false;
            movedUpTimer = 2f;
        }
    }

    // will continue moving up Infinitely -> so we need to check the position of y to stop the moving when the desired position reached  
    private void MoveUp()
    {
        // activate collider
        myCollider.enabled = true;
        //  Time.deltaTime -> so it's independent when changing the position
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    // Move Down 
    private void MoveDown()
    {
        // deactivate collider
        myCollider.enabled = false;
        // -Vector3.up -> so it moves down 
        transform.Translate(-Vector3.up * speed * Time.deltaTime);
    }


    private void OnMouseDown()
    {
        isMovingUp = false;

        // publish that the event happened 
        MoleClicked?.Invoke();
        //  MoveDown();
    }


}
