using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f; // Speed of the player
    private Stack<Tile> _path;

    private float lockedYPosition;

    public void SetPosition(Vector3 newPosition)
    {
        lockedYPosition = transform.position.y;
        transform.position = new Vector3(newPosition.x, lockedYPosition, newPosition.z);
    }

    public void SetPath(Stack<Tile> path)
    {
        _path = path;
        StopAllCoroutines();
        StartCoroutine(MoveAlongPath(path));
    }

    
    private IEnumerator MoveAlongPath(Stack<Tile> path)
    {
        float playerHeight = transform.position.y;
        while (path.Count > 0)
        {
            Tile nextTile = path.Pop();
            Vector3 startPosition = transform.position;
            Vector3 endPosition = nextTile.transform.position;
            float journeyLength = Vector3.Distance(startPosition, endPosition);
            float journeyTime = journeyLength / movementSpeed;
            float lerpVal = 0;

            while (lerpVal < 1)
            {
                lerpVal += Time.deltaTime / journeyTime;
                SetPosition(Vector3.Lerp(startPosition, endPosition, lerpVal));
                yield return null;
            }
        }
    }
}
