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
        if(RewindComponent != null)
        {
            rewindList = RewindComponent.getRewindList();
        }
    }

    public void SetRewindComponent(RewindList r)
    {
        RewindComponent = r;
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
