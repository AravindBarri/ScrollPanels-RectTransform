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

        if (Input.GetMouseButton(0))
        {
            if(rectTransform.anchoredPosition.x >= -1001 && rectTransform.anchoredPosition.x <= 1001)
            {
                Move(moveSpeed, Input.GetAxisRaw("Mouse X"));
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
                            Move(moveSpeed, +1);
                        }
                        else
                        {   
                            Debug.Log("Left Swipe");
                            Move(moveSpeed, -1);
                        }
                    }
                }
            }
        }
#endif
        if (rectTransform.anchoredPosition.x <= -1000)
        {
            rectTransform.anchoredPosition = new Vector2(-1000,0);
        }
        if(rectTransform.anchoredPosition.x >= 1000)
        {
            rectTransform.anchoredPosition = new Vector2(1000, 0);
        }
    }
    public void Move(float offset,float sign)
    {

        rectTransform.anchoredPosition += sign * Vector2.right * offset;
    }
}
