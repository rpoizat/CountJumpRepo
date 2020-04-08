using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControleurScript : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJump;

    private Ray scanJump;
    private Vector3 posRayCentre;
    private Vector3 posRayGauche;
    private Vector3 posRayDroit;
    private bool canJump = true;

    public void ReplacePlayer(Vector3 position)
    {
        playerTransform.position = position;
    }

    private void FixedUpdate()
    {
        posRayCentre = new Vector3(playerTransform.position.x, playerTransform.position.y - 0.5f, playerTransform.position.z);
        posRayGauche = new Vector3(playerTransform.position.x - playerTransform.localScale.x / 2, playerTransform.position.y - 0.5f, playerTransform.position.z);
        posRayDroit = new Vector3(playerTransform.position.x + playerTransform.localScale.x / 2, playerTransform.position.y - 0.5f, playerTransform.position.z);

        if (!Physics.Raycast(posRayCentre, -playerTransform.up, 0.5f) && canJump == true)
        {
            canJump = false;
        }
        else
        {
            if (!Physics.Raycast(posRayGauche, -playerTransform.up, 0.5f) && canJump == true)
            {
                canJump = false;
            }
            else
            {
                if (!Physics.Raycast(posRayDroit, -playerTransform.up, 0.5f) && canJump == true)
                {
                    canJump = false;
                }
            }
        }

        if (Physics.Raycast(posRayCentre, -playerTransform.up, 0.5f) && canJump == false)
        {
            canJump = true;
        }
        else
        {
            if (Physics.Raycast(posRayGauche, -playerTransform.up, 0.5f) && canJump == false)
            {
                canJump = true;
            }
            else
            {
                if (Physics.Raycast(posRayDroit, -playerTransform.up, 0.5f) && canJump == false)
                {
                    canJump = true;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //avancer vers la droite
        if(Input.GetKey(KeyCode.RightArrow))
        {
            playerTransform.Translate(playerTransform.right * playerSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            playerTransform.Translate(-playerTransform.right * playerSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.UpArrow) && canJump)
        {
            playerRigidbody.AddForce(playerTransform.up * playerJump, ForceMode.Acceleration);
            canJump = false;
        }
    }
}
