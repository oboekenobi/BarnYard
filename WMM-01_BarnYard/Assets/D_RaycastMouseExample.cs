using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_RaycastMouseExample : MonoBehaviour
{
    public GameObject hitObject;
    public LayerMask IgnoreMe;
    private MemoryObject _memObject;
    private MemoryObject _oldMemObject;
    [SerializeField]
    private List<GameObject> catchObject;
    [SerializeField]
    public List<GameObject> objectList;




    //public float scale = 2;
    //bool isHoverOn = false;

    

    private void FixedUpdate()
    {
        //This is your raycast
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        if (!Physics.Raycast(ray, out RaycastHit rayHit))
        {
          /*  if (hitObject == catchObject[0])
            {
                if (_memObject != null)
                {
                    _memObject.SetHoverState(false);
                    _memObject = null;
                }
            }*/
            if (_memObject != null)
            {
                _memObject.SetHoverState(false);
                _memObject = null;
            }
            hitObject = null;

            Debug.Log("Not Hitting anything");
        }
        else //If we aren't hovering, reset the scale of our object and clear it from memory
        {
            if (rayHit.collider.tag.Equals("MemObject"))
            {
                hitObject = rayHit.transform.gameObject;
            }
           
            //This determines which GameObject is being hit and stores it in a list as well as the previous hitobject
          

            catchObject.Add(hitObject);

            if (catchObject.Count > 1)
            {
                catchObject.Remove(catchObject[1]);

            }

            if (hitObject != catchObject[0])
            {
                if (objectList.Count > 0)
                {
                    objectList.Clear();
                }

                objectList.Add(catchObject[0]);
                catchObject.Clear();
            }



            //This Sets the previous hitobjects hoverstate to false 
            if (objectList.Count > 0)
            {
                _oldMemObject = objectList[0].GetComponent<MemoryObject>();
                _oldMemObject.SetHoverState(false);
            }




            Debug.Log("Hitting something");
            //Try to get a component
            try
            {
                _memObject = catchObject[0].GetComponent<MemoryObject>();
            }
            catch
            {
                Debug.Log("There is no component");
            }

            //If we got it, set the hoverstate
            if (_memObject != null)
            {
                _memObject.SetHoverState(true);
            }
            
         
     
         
           
          

          
        }

      
    }

}

