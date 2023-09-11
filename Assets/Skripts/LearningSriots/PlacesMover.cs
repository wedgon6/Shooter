using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacesMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _palace;

    private Transform[] _places;
    private int _currentPlacesIndex;

    private void Start()
    {
        _places = new Transform[_palace.childCount];

        for (int index = 0; index < _palace.childCount; index++)
            _places[index] = _palace.GetChild(index).transform;
    }

    private void Update()
    {
        Transform currentPalace = _places[_currentPlacesIndex];
        transform.position = Vector3.MoveTowards(transform.position, currentPalace.position, _speed * Time.deltaTime);


        if (transform.position == currentPalace.position) 
            GetNextsPalaceIndex();
    }

    private Vector3 GetNextsPalaceIndex()
    {
        _currentPlacesIndex++;

        if (_currentPlacesIndex == _places.Length)
            _currentPlacesIndex = 0;

        Vector3 derection = _places[_currentPlacesIndex].transform.position;
        transform.forward = derection - transform.position;
        
        return derection;
    }
}