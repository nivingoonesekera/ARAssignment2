using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallMover : MonoBehaviour
{

    public float moveSpeed = -0.5f;
    public Text uiText;
    public float textTimer = 2f;
    private float textTimeTemp = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
        if (textTimeTemp > 0)
        {
            textTimeTemp -= Time.deltaTime;
        }
        else
        {
            uiText.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (uiText != null)
        {
            uiText.text = "Ouch!";
            textTimeTemp = textTimer;
            Crumble();
        }
    }

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
