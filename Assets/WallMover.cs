using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallMover : MonoBehaviour
{

    public float moveSpeed = -0.5f;
    private float currentMoveSpeed = 0;
    public Text uiText;
    public float textTimer = 2f;
    private float textTimeTemp = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentMoveSpeed = moveSpeed;
        //should maybe load a default/random wall 'set' from a list of prefabs here, and do so again after 'cleanup'
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, 0, currentMoveSpeed * Time.deltaTime);
        if (textTimeTemp > 0)
        {
            textTimeTemp -= Time.deltaTime;
        }
        else
        {
            uiText.text = "";
        }
    }

    //When we hit the triggers on kyle's head, hands, feet, or torso
    private void OnTriggerEnter(Collider other)
    {
        if (uiText != null)
        {
            //this was mostly just to test collisions, before implementing wall crumble
            uiText.text = "Ouch!";
            textTimeTemp = textTimer;
            currentMoveSpeed = 0f;
            //trigger wall crumble
            Crumble();
        }
    }

    //Gets all grandchildren of wall and gives them a non-kinematic rigidbody affected by gravity 
    //this will make all the children act independently of the wall and fall to pieces
    private void Crumble()
    {
        foreach (Transform childTransform in this.GetComponentInChildren<Transform>())
        {
            foreach (Transform setPieceTransform in childTransform.GetComponentInChildren<Transform>())
            {
                setPieceTransform.gameObject.AddComponent<Rigidbody>();
                Rigidbody rigidbody = setPieceTransform.GetComponent<Rigidbody>();
                rigidbody.isKinematic = false;
                rigidbody.useGravity = true;
                rigidbody.transform.SetParent(setPieceTransform);
            }
        }
    }
}
