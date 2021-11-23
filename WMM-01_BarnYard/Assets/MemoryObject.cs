using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryObject : MonoBehaviour
{ 
    //This component handles everything for the object
    [SerializeField]
    public Vector3 scale;
    private bool isHoveredOn = false;
    private bool canScale = true;
/*    public Transform startPosition, endPosition;*/
    private Transform _this;
    private bool canLerp = true;
    private bool hasLerped = false;
    public float lerpDuration = 0.5f;
    [SerializeField]
    public Vector3 start;


    //These two bool patterns help to prevent our method from being called twice
    void Update()
    {
        if (isHoveredOn)
        {
            if (canScale)
            {
                ScaleObject(isHoveredOn);
                canScale = false;
            }
        }
        else if (!isHoveredOn)
        {
            if (!canScale)
            {
                ScaleObject(isHoveredOn);
                canScale = true;
            }
        }
    }

    //This is what the raycast calls
    public void SetHoverState(bool _hoverState)
    {
        isHoveredOn = _hoverState;
    }

    //This scales our object
    private void ScaleObject(bool _hoverState)
    {
        if (_hoverState) //If we aren't hovering, start our hover state
        {
            //Call the lerp down here
            /*
                        startPosition = Vector3.one;
                        endPoisiton = Vector3.scale;*/
            /* Vector3 startPos = transform.localScale;*/
            /* float elapsedTime = 0;*/
            /* Vector3 objectScale = transform.localScale * scale;*/
            /* ToggleScalePosition(objectScale, startPos);*/
            /*  transform.localScale = Vector3.Lerp(transform.localScale * scale, transform.localScale * scale, elapsedTime / duration);
              elapsedTime += Time.deltaTime;*/
          
         
            StartCoroutine(LerpPosition(scale, lerpDuration));
            
        }
        else if (!_hoverState)
        {
            StartCoroutine(LerpPositionTwo(start, lerpDuration));
            /*transform.localScale = Vector3.one; //(1,1,1)*/
        }
    }

   /* public void ToggleScalePosition(Vector3 endPosition, Vector3 startPosition)
    {
        if (canLerp)
        {
            if (!hasLerped)
            {
                hasLerped = true;
                StartCoroutine(LerpPosition(endPosition, lerpDuration));
            }
            else
            {
                hasLerped = false;
                StartCoroutine(LerpPosition(startPosition, lerpDuration));
            }
        }
    }
*/

    IEnumerator LerpPosition(Vector3 _endPos, float duration)
    {
        canLerp = false;

        float elapsedTime = 0;
        var startPos = transform.localScale;
        var targetPos = Vector3.zero;

        while (elapsedTime <= duration)
        {
            targetPos = Vector3.Lerp(startPos, _endPos, elapsedTime / duration);
            yield return null;
            elapsedTime += Time.deltaTime;
            transform.localScale = targetPos;
        }
        canLerp = true;
        yield break;
    }
    IEnumerator LerpPositionTwo(Vector3 _endPos, float duration)
    {
        canLerp = false;

        float elapsedTime = 0;
        var startPos = transform.localScale;
        var targetPos = Vector3.zero;

        while (elapsedTime <= duration)
        {
            targetPos = Vector3.Lerp(startPos, _endPos, elapsedTime / duration);
            yield return null;
            elapsedTime += Time.deltaTime;
            transform.localScale = targetPos;
        }
        canLerp = true;
        yield break;
    }
}
 