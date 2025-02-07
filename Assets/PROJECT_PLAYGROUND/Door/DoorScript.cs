using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DoorStateChangeEvent : UnityEvent<bool> { }


public class DoorScript : MonoBehaviour

{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject doorLeftPrefab;
    public GameObject doorRightPrefab;
    public float doorWidth = 1;
    public float doorOpenOffset = 1;
    public DoorStateChangeEvent doorStateChangeEvent;
    public float openingSpeed = 0.005f; 

    private GameObject doorLeft;
    private GameObject doorRight;

    public bool isOpenOnStart;
    private bool _isOpen;
    public bool isOpen
    {
        get
        {
            return _isOpen;
        }
        set
        {
            _isOpen = value;
            doorStateChangeEvent.Invoke(_isOpen);
        }
    }

    private Vector3 leftClosedTransform;
    private Vector3 rightClosedTransform;
    private Vector3 leftOpenTransform;
    private Vector3 rightOpenTransform;

    private long i = 0;

    // Start is called before the first frame update
    void Start()
    {
        _isOpen = isOpenOnStart;

        leftClosedTransform = new Vector3(-doorWidth, 0, 0);
        rightClosedTransform = new Vector3(0, 0, 0);

        leftOpenTransform = leftClosedTransform - new Vector3(doorOpenOffset, 0, 0);
        rightOpenTransform = rightClosedTransform + new Vector3(doorOpenOffset, 0, 0);
        
        doorLeft = Instantiate(doorLeftPrefab);
        doorLeft.transform.SetParent(this.gameObject.transform);
        doorLeft.transform.localPosition = _isOpen ? leftOpenTransform : leftClosedTransform;
        
        doorRight = Instantiate(doorRightPrefab);
        doorRight.transform.SetParent(this.gameObject.transform);
        doorRight.transform.localPosition = _isOpen ? rightOpenTransform : rightClosedTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOpen)
        {
            // GO TOWARDS OPENING STATE
            Debug.Log("open");
            if (doorLeft.transform.localPosition.x > leftOpenTransform.x)
            {
                doorLeft.transform.localPosition -= new Vector3(openingSpeed, 0, 0);
            }
            else
            {
                doorLeft.transform.localPosition = leftOpenTransform;
            }

            if (doorRight.transform.localPosition.x < rightOpenTransform.x)
            {
                doorRight.transform.localPosition += new Vector3(openingSpeed, 0, 0);
            }
            else
            {
                doorRight.transform.localPosition = rightOpenTransform;
            }
        } else
        {
            // GO TOWARDS CLOSING STATE
            Debug.Log("closed");
            if (doorLeft.transform.localPosition.x < leftClosedTransform.x)
            {
                doorLeft.transform.localPosition += new Vector3(openingSpeed, 0, 0);
            }
            else
            {
                doorLeft.transform.localPosition = leftClosedTransform;
            }

            if (doorRight.transform.localPosition.x > rightClosedTransform.x)
            {
                doorRight.transform.localPosition -= new Vector3(openingSpeed, 0, 0);
            }
            else
            {
                doorRight.transform.localPosition = rightClosedTransform;
            }
        }

        // DEBUG
/*        if (i % 3000 == 0)
        {
            isOpen = true;
        } else if (i % 3000 == 1500)
        {
            isOpen = false;
        }*/

        i++;
    }
}
