using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenechange : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;

                if (Vector2.Distance(fingerDownPosition, fingerUpPosition) > minDistanceForSwipe)
                {
                    Vector2 swipeDirection = fingerUpPosition - fingerDownPosition;

                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        // Swipe left or right
                        if (swipeDirection.x < 0)
                        {
                            // Swipe left, load next scene
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        }
                        else
                        {
                            // Swipe right, load previous scene
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                        }
                    }
                }
            }
        }
    }
}

