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
    private Vector3 posRay;
    private bool canJump = true;

    public void ReplacePlayer(Vector3 position)
    {
        playerTransform.position = position;
    }

    private void FixedUpdate()
    {
        posRay = new Vector3(playerTransform.position.x, playerTransform.position.y - 0.5f, playerTransform.position.z);

        if (Physics.Raycast(posRay, -playerTransform.up, 0.5f) && canJump == false)
        {
            canJump = true;
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
