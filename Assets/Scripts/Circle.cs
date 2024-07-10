// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Circle : MonoBehaviour
{
    Vector3 _startingPosition;
    [SerializeField] float magnitude = 10f;
    [SerializeField] private float period = 2f;
    [SerializeField] [Range(0, 2 * Mathf.PI)] private float phase = 0f;
    [SerializeField] Vector2 centerPosition;
    
    Vector3 _movementVector;
    float _movementFactor;
    private float _angle = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _startingPosition = transform.position;

        float xCat = transform.position.x - centerPosition.x;
        float yCat = transform.position.y - centerPosition.y;

        if (xCat != 0)
        {
            _angle = Mathf.Atan(yCat / xCat);
            phase = phase + _angle;
        }
        else
        {
            _angle = Mathf.PI / 2;
            phase = phase + _angle;
        }

        _movementVector = new Vector3(magnitude * Mathf.Cos(_angle), magnitude * Mathf.Sin(_angle), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (period == 0f) { return; }
        
        float cycles = Time.time / period; // Continually growing over time
        
        const float tau = Mathf.PI * 2; // Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau + phase); // Going from -1 to 1

        _movementFactor = (rawSinWave + 1f) / 2f; // Recalculated to go from 0 to 1, so it's cleaner
        
        Vector3 offset = _movementVector * _movementFactor;
        transform.position = _startingPosition + offset;
    }
}