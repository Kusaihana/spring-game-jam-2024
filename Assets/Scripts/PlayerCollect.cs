
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    private List<ITouchable> _currentTouchedObjects = new();
    private ITouchable _currentlyDisplayedObject;
    public List<CollectionItem> CurrentlyHeldItems { get; set; } = new();
    void Update()
    {
        //collect
        CheckCollect();
            
        //handle display
        CheckAndDisplayClosestObject();
    }

    private void CheckCollect()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _currentlyDisplayedObject is ICollectable collectable)
        {
            _currentlyDisplayedObject.HidePrompt();
            _currentTouchedObjects.Remove(_currentlyDisplayedObject);
            _currentlyDisplayedObject = null;
            
            CollectionItem collectedItem = collectable.Collect();
            CurrentlyHeldItems.Add(collectedItem);
            
            Debug.Log("Collected: " + collectedItem.Name);
        }
    }

    private void CheckAndDisplayClosestObject()
    {
        if (HasNoObjects())
            return;

        ITouchable closestObject = GetClosestObject();

        if (_currentlyDisplayedObject == closestObject)
            return;
        
        //new object to display
        _currentlyDisplayedObject?.HidePrompt();

        closestObject.ShowPrompt();

        _currentlyDisplayedObject = closestObject;

    }

    private bool HasNoObjects()
    {
        return _currentTouchedObjects.Count == 0;
    }

    private ITouchable GetClosestObject()
    {
        if (_currentTouchedObjects.Count == 1)
        {
            return _currentTouchedObjects[0];
        }

        ITouchable closest = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        
        foreach(var obj in _currentTouchedObjects)
        {
            Vector3 directionToTarget = ((MonoBehaviour)obj).transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closest = obj;
            }
        }
        
        return closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        ITouchable touchable = other.GetComponent<ITouchable>();
        if (touchable != null)
        {
            _currentTouchedObjects.Add(touchable);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        ITouchable touchable = other.GetComponent<ITouchable>();
        if (touchable != null)
        {
            if (_currentlyDisplayedObject == touchable)
            {
                _currentlyDisplayedObject.HidePrompt();
                _currentlyDisplayedObject = null;
            }
            _currentTouchedObjects.Remove(touchable);
        }
    }
}