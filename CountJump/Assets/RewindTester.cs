using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindTester : MonoBehaviour
{

    [SerializeField]
    private RewindList RewindComponent;
    private Stack<GameObject> rewindList;

    public void Start()
    {
        rewindList = RewindComponent.getRewindList();
    }


    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.transform.parent.gameObject.GetComponent<PlayerControleurScript>().triggerRewind)
        {
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerControleurScript>().triggerRewind = false;
            rewindList.Push(gameObject);
        }
        

    }
}
