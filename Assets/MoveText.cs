using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveText : MonoBehaviour
{

    Interpolator _interpolator = new Interpolator(1f, Interpolator.Type.SMOOTH);
    Vector2 _position;

    private void Start()
    {
        _interpolator.ToMax();
    }
    void Update()
    {
        _interpolator.Update(Time.deltaTime);

        if (_interpolator.IsMaxPrecise)
            _interpolator.ToMin();
        else if (_interpolator.IsMinPrecise)
            _interpolator.ToMax();

        _position.x = this.transform.position.x;
        _position.y = 5 * 1 * _interpolator.Value + 170f;
        this.transform.position = _position;
    }
}
