using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    public delegate void CurrentTileUpdate(Tile tile);
    public event CurrentTileUpdate onCurrentTileUpdate;

    [SerializeField] private float movementSpeed = 5f; // Speed of the player
    private float lockedYPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void SetPosition(Vector3 newPosition)
    {
        lockedYPosition = transform.position.y;
        transform.position = new Vector3(newPosition.x, lockedYPosition, newPosition.z);
    }


    public void SetPath(Stack<Tile> path)
    {
        StopAllCoroutines();
        StartCoroutine(MoveAlongPath(path));
    }

    
    private IEnumerator MoveAlongPath(Stack<Tile> path)
    {
        float playerHeight = transform.position.y;

        if (animator != null)
        {
            Debug.Log("starting run animation...");
            animator.SetBool("IsRunning", true); // Trigger run animation
        }

        while (path.Count > 0)
        {
            Tile currentTile = path.Pop();

            Vector3 startPosition = transform.position;
            Vector3 endPosition = currentTile.transform.position;

            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float journeyTime = journeyLength / movementSpeed;
            float lerpVal = 0;

            transform.LookAt(currentTile.transform.position);
            while (lerpVal < 1)
            {
                lerpVal += Time.deltaTime / journeyTime;
                SetPosition(Vector3.Lerp(startPosition, endPosition, lerpVal));
                onCurrentTileUpdate?.Invoke(currentTile);
                yield return null;
            }

        }

        if (animator != null)
        {
            Debug.Log("ending run animation...");
            animator.SetBool("IsRunning", false); // Trigger run animation
        }

    }
}
