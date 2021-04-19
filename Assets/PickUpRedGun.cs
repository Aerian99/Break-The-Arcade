using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUpRedGun : MonoBehaviour
{
    Interpolator _interpolator = new Interpolator(1f, Interpolator.Type.SMOOTH);
    Vector3 _position;
    public float interpolationValue;
    // Start is called before the first frame update
    void Start()
    {
        _interpolator.ToMax();
    }

    // Update is called once per frame
    void Update()
    {
        _interpolator.Update(Time.deltaTime);

        if (_interpolator.IsMaxPrecise)
            _interpolator.ToMin();
        else if (_interpolator.IsMinPrecise)
            _interpolator.ToMax();
        _position.y = 0.5f * 1 * _interpolator.Value + interpolationValue;
        _position.x = this.transform.position.x;
        this.transform.position = _position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().redUnlocked = true;
            collision.gameObject.GetComponent<playerBehaviour>().bulletsShotgun = 0;
            collision.gameObject.GetComponent<playerBehaviour>().reservedAmmoShotgun = 0;
            Destroy(gameObject);
        }
    }
}
