﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControleurScript : MonoBehaviour
{
    [SerializeField] private int nbSaut;

    [SerializeField] private int nbRewind;

    [SerializeField] private int limiteSaut;
    [SerializeField] private int editJumpLimite;

    [SerializeField]
    private Transform depart;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJump;
    [SerializeField]
    private RewindList RewindComponent;

    [Header("Référence interface")]
    [SerializeField] private Text info;
    private Stack<GameObject> rewindList;

    
    public bool triggerRewind;
    [HideInInspector]
    public bool EditMode = false;
    private Ray scanJump;
    private Vector3 posRayCentre;
    private Vector3 posRayGauche;
    private Vector3 posRayDroit;
    private bool canJump = true;

    public void ReplacePlayer(Vector3 position)
    {
        playerTransform.position = position;
        
    }

    public Rigidbody getRigidBody()
    {
        return playerRigidbody;
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
        if(transform.position.x < -50f)
        {
            transform.position = new Vector3(50f, transform.position.y, transform.position.z);
        }
        else
        {
            if(transform.position.x > 50f)
            {
                transform.position = new Vector3(-50f, transform.position.y, transform.position.z);
            }
        }

        if(transform.position.y < -26f)
        {
            transform.position = new Vector3(transform.position.x, 27f, transform.position.z);
        }
        else
        {
            if(transform.position.y > 27f)
            {
                transform.position = new Vector3(transform.position.x, -26f, transform.position.z);
            }
        }

        //avancer vers la droite
        if (Input.GetKey(KeyCode.RightArrow) && !EditMode)
        {
            playerTransform.Translate(playerTransform.right * playerSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftArrow) && !EditMode)
        {
            playerTransform.Translate(-playerTransform.right * playerSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && canJump && nbSaut < limiteSaut && !EditMode)
        {
            nbSaut = nbSaut + 1;
            playerRigidbody.AddForce(playerTransform.up * playerJump, ForceMode.Acceleration);
            canJump = false;
            triggerRewind = true;
            
        }

        if (Input.GetKeyDown(KeyCode.R) && !EditMode)
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

            else
            {
                Transform plateform = depart;
                ReplacePlayer(new Vector3(plateform.position.x, plateform.position.y + plateform.localScale.y / 2 + 1f, plateform.position.z));
            }
            
        }

        //update de l'interface
        info.text = "Sauts restants : " + (limiteSaut - nbSaut) + "\n Nombre de Rewind : " + nbRewind;
    }

    //modifier les propriétés du saut
    public void setJumpSpeed(float value)
    {
        playerJump += value;
    }

    //définir la limite de saut
    public void SetJumpLimit(int value)
    {
        limiteSaut = value;
    }

    public void AddJumps(int value)
    {
        Debug.Log("AJOUT SAUT");
        limiteSaut += value;
    }

    public void AddSpeed(float value)
    {
        playerSpeed += value;
    }

    public void ResetCompteurs(Transform g)
    {
        nbSaut = 0;
        nbRewind = 0;
        playerJump = 1200;
        playerSpeed = 8;
        RewindComponent.ResetStack();
        depart = g;

    }

    public void ResetCompteursCustom()
    {
        nbSaut = 0;
        nbRewind = 0;
        playerJump = 1200;
        playerSpeed = 8;
        RewindComponent.ResetStack();
    }

    public int getlimiteSaut()
    {
        return limiteSaut;
    }

    public void setNbSaut(int value)
    {
        nbSaut = value;
    }

    public int getNbSaut()
    {
        return nbSaut;
    }

    public void setEditJumpLimite(int value)
    {
        editJumpLimite = value;
        
    }

    public int getEditJumpLimite()
    {
        return editJumpLimite;

    }
}
