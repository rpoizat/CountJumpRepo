using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusJump : MonoBehaviour
{
    [SerializeField] private float newJumpSpeed;
    [SerializeField] private PlayerControleurScript player;

    private void OnTriggerEnter(Collider other)
    {
        player.setJumpSpeed(newJumpSpeed);
        gameObject.SetActive(false);
    }
}
