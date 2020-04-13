using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveeScript : MonoBehaviour
{
    [SerializeField] GameManagerScript gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Joueur")
        {
            gameManager.EndLevel();
        }
    }
}
