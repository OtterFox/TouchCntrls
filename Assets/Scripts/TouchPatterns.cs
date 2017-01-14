using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchPatterns : MonoBehaviour {

    int tapCount;
    float tapTimer;

    bool isTap;
    bool isDoubleTap;
    bool isSwipeR;
    bool isSwipeL;
    bool isSwipeUp;
    bool isSwipeDown;
    bool isTouchDisabled;

	void Start ()
    {
        
    }
		
	void Update ()
    {
        Taps();
        Swipe();
        PatternTest();

        //print(" " + Input.touchCount);

    }

    void Taps()
    {
       if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapCount++;
        }
       if(tapCount > 0)
        {
            tapTimer += Time.deltaTime;   //start timer to count tap or double tap     
        }
       if (tapCount == 2)
        {
            tapTimer = 0.0f;
            tapCount = 0;
            isDoubleTap = true;
            DoubleTap();
        }
       if(tapTimer > .2f)     //timer to call if single tap
        {
            tapTimer = 0f;
            tapCount = 0;
            isTap = true;
            SingleTap();
            
        }
    }

    void SingleTap()
    {
        if (!isTouchDisabled)
        {
            print("tap ");
        }
    }

    void DoubleTap()
    {
        if (!isTouchDisabled)
        {
            print("double tap ");
        }
    }

    void Swipe()
    {
        if (Input.touches.Length > .3f && !isTouchDisabled )
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).deltaPosition.x > 0)
            {
                isSwipeR = true;
                SwipeRight();
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).deltaPosition.x < 0)
            {
                isSwipeL = true;
                SwipeLeft();
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).deltaPosition.y > 0)
            {
                isSwipeUp = true;
                SwipeUp();
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).deltaPosition.y < 0)
            {
                isSwipeDown = true;
                SwipeDown();
                
            }
        }

    }

    //for testing
    public void SwipeRight()
    {
        isSwipeR = false;
        print("Right swipe ");
    }

    public void SwipeLeft()
    {
        
        print("Left swipe " );
    }

    public void SwipeUp()
    {
        isSwipeUp = false;
        print("Up swipe ");
    }

    public void SwipeDown()
    {
        isSwipeDown = false;
        print("Down swipe ");
    }

    void PatternTest()
    {
        //Was there a tap then swipe? Test the pattern if so
        if(isDoubleTap && isSwipeL)
        {
            print("tap + left swipe worked");
            isDoubleTap = false;
            isSwipeL = false;
            isTouchDisabled = true;
            StartCoroutine("TouchPause");
        }
    }

    IEnumerator TouchPause()
    {
       
        yield return new WaitForSeconds(2f);    // 2f needs to be its own float var to decrese when objects instantiate speed increases
       
        isTouchDisabled = false;
    }
}
