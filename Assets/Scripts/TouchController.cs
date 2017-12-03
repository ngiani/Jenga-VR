using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Mode {POINT, GRAB, IDLE}
enum Hand {LEFT, RIGHT}

public class TouchController : MonoBehaviour {

    Tower tower;
    Vector3 originalPosition;
    Vector3 originalBlockPosition;
    Mode currentMode;

    [SerializeField]Hand hand;

	// Use this for initialization
	void Start () {

        GetComponent<Animator>().Play("Natural");
        tower = GameObject.FindObjectOfType<Tower>();


        originalPosition = transform.position;
        currentMode = Mode.IDLE;

    }
	
	// Update is called once per frame
	void Update () {

        if (hand == Hand.RIGHT)
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

            //Point with finger while pressing hand trigger to enter point mode

            if ((!OVRInput.Get(OVRInput.NearTouch.SecondaryIndexTrigger, OVRInput.Controller.Active) || !OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger, OVRInput.Controller.Active))
                && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Active))
            {
                //Show pointing hand
                GetComponent<Animator>().Play("Point");

                //Point a block to select current block
                RaycastHit hitInfo;

                Block selected = null;

                if (Physics.Raycast(transform.position, transform.forward, out hitInfo))
                {
                    selected = hitInfo.transform.GetComponent<Block>();
                }

                if (tower.selected != null && tower.selected != selected)
                {
                    tower.selected.HandleUnSelection();
                }

                if (selected != null && !selected != tower.selected)
                {
                    selected.HandleSelection();
                }

                if (tower.selected != selected)
                    tower.selected = selected;


                //Rotate around y axis with thumbstick
                Vector2 secondaryThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick, OVRInput.Controller.Active);

                if (!secondaryThumbstick.Equals(new Vector2(0.0f, 0.0f)))
                {
                    float rotation = Mathf.Atan2(secondaryThumbstick.y, secondaryThumbstick.x);
                    if (tower.selected != null)
                    {
                        tower.selected.transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * rotation, 0);
                    }
                }

                currentMode = Mode.POINT;
            }

            //Press both trigger to enter grab mode 
            else if ((OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger, OVRInput.Controller.Active) || OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger, OVRInput.Controller.Active))
                && OVRInput.Get(OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Active))
            {
                //Show grabbing hand
                GetComponent<Animator>().Play("GrabStickUp");


                //Move selected block while grabbing it with hand
                if (currentMode != Mode.GRAB && tower.selected != null)
                {
                    originalPosition = transform.position;
                    originalBlockPosition = tower.selected.transform.position;
                }

                if (tower.selected != null)
                {
                    tower.selected.transform.position = transform.position - (originalPosition - originalBlockPosition);
                }


                //Rotate around y axis with thumbstick
                Vector2 secondaryThumbstick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick, OVRInput.Controller.Active);
                if (!secondaryThumbstick.Equals(new Vector2(0.0f, 0.0f)))
                {
                    float rotation = Mathf.Atan2(secondaryThumbstick.y, secondaryThumbstick.x);
                    if (tower.selected != null)
                    {
                        tower.selected.transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * rotation, 0);
                    }
                }
                currentMode = Mode.GRAB;

            }


            //Nothing pressed, just show open hand
            else
            {
                GetComponent<Animator>().Play("Natural");

                currentMode = Mode.IDLE;


            }
        }

        else if (hand == Hand.LEFT)
        {
            transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
        }

        
            

        OVRInput.Update(); 

	}
}
