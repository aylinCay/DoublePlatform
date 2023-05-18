using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 _firstInput;

    private Vector3 _secondInput;

    private Vector3 result;

    private bool _isVertical;

    public Vector3 ReadInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _firstInput = Input.mousePosition;
        }

        if (Input.GetButton("Fire1"))
        {
            _secondInput = Input.mousePosition;

        }

        if (Input.GetButtonUp("Fire1"))
        {
            result = _firstInput - _secondInput;
        }

         _isVertical = Mathf.Abs(result.y) > Mathf.Abs(result.x);
        if (_isVertical)
        {
            result = Vector3.zero;
            return Vector3.up;
        }

        return (result.x > 0 ? -1 : 1) * Vector3.right;

    }
}
