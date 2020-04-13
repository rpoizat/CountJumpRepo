using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControleurScript : MonoBehaviour
{
    [SerializeField] private int nbSaut;

    [SerializeField] private int nbRewind;

    [SerializeField] private int limiteSaut;

    [SerializeField]
    private Transform depart;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJump;
    [SerializeField]
    private RewindList RewindComponent;
    private Stack<GameObject> rewindList;

    
    public bool triggerRewind;
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
        //si on dépasse des limites du terrain
        if(transform.position.x < -45f)
        {
            transform.position = new Vector3(45f, transform.position.y, transform.position.z);
        }
        else
        {
            if(transform.position.x > 45)
            {
                transform.position = new Vector3(-45f, transform.position.y, transform.position.z);
            }
        }

        //avancer vers la droite
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerTransform.Translate(playerTransform.right * playerSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            playerTransform.Translate(-playerTransform.right * playerSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && canJump && nbSaut != limiteSaut)
        {
            nbSaut = nbSaut + 1;
            playerRigidbody.AddForce(playerTransform.up * playerJump, ForceMode.Acceleration);
            canJump = false;
            triggerRewind = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rewindList = RewindComponent.getRewindList();

            if (rewindList.Count > 0 )
            {
                nbSaut = nbSaut - 1;

                nbRewind = nbRewind + 1;

                if (rewindList.Count == 1)
                {

                    rewindList.Pop();
                    Debug.Log("On tp");
                    Transform plateform = depart;
                    ReplacePlayer(new Vector3(plateform.position.x, plateform.position.y + plateform.localScale.y / 2 + 1f, plateform.position.z));
                }

                else
                {
                    rewindList.Pop();
                    Debug.Log("On tp");
                    Transform plateform = rewindList.Peek().transform;
                    ReplacePlayer(new Vector3(plateform.position.x, plateform.position.y + plateform.localScale.y / 2 +1f, plateform.position.z));
                }
                
            }
            
        }
    }
}
