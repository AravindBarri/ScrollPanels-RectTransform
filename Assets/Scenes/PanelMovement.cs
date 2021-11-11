using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMovement : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] float moveSpeed = 1f;
    private Vector3 fp;   
    private Vector3 lp;  
    private float dragDistance;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        //print(rectTransform.anchoredPosition);
        if (Input.GetMouseButton(0))
        {
            if(rectTransform.anchoredPosition.x >= -501 && rectTransform.anchoredPosition.x <= 501)
            {
                if (rectTransform.anchoredPosition.y >= -501 && rectTransform.anchoredPosition.y <= 501)
                {
                    MoveX(moveSpeed, Input.GetAxisRaw("Mouse X"));
                    MoveY(moveSpeed, Input.GetAxisRaw("Mouse Y"));
                }
                    
            }

        }
#endif
#if UNITY_ANDROID
        if (Input.touchCount == 1) 
        {
            Touch touch = Input.GetTouch(0); 
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) 
            {
                lp = touch.position; 

                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   
                        if ((lp.x > fp.x))  
                        {   
                            Debug.Log("Right Swipe");
                            MoveX(moveSpeed, +1);
                        }
                        else
                        {   
                            Debug.Log("Left Swipe");
                            MoveX(moveSpeed, -1);
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                            MoveY(moveSpeed, +1);
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            MoveY(moveSpeed, +1);
                        }
                    }
                }
            }
        }
#endif
        if (rectTransform.anchoredPosition.y <= -500 )
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -500);
        }
        if (rectTransform.anchoredPosition.y >= 500)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 500);
        }
        if(rectTransform.anchoredPosition.x <= -500)
        {
            rectTransform.anchoredPosition = new Vector2(-500,rectTransform.anchoredPosition.y);
        }
        if ( rectTransform.anchoredPosition.x >= 500)
        {
            rectTransform.anchoredPosition = new Vector2(500, rectTransform.anchoredPosition.y);
        }
    }
    public void MoveX(float offset,float sign)
    {

        rectTransform.anchoredPosition += sign * Vector2.right * offset;
    }
    public void MoveY(float offset, float sign)
    {

        rectTransform.anchoredPosition += sign * Vector2.up * offset;
    }
}
