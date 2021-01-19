using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpArmas : MonoBehaviour
{
    public List<Sprite> armas;
    int randomArma;
    Interpolator _interpolator = new Interpolator(1f, Interpolator.Type.SMOOTH);
    Vector3 _position;
    // Start is called before the first frame update
    void Start()
    {
        _interpolator.ToMax();
        randomArma = Random.Range(0, armas.Count);
        if(randomArma == 4)
            GetComponent<SpriteRenderer>().sprite = armas[6];
        else if(randomArma == 5)
            GetComponent<SpriteRenderer>().sprite = armas[7];
        else
            GetComponent<SpriteRenderer>().sprite = armas[randomArma];
    }

    // Update is called once per frame
    void Update()
    {
        _interpolator.Update(Time.deltaTime);

        if (_interpolator.IsMaxPrecise)
            _interpolator.ToMin();
        else if (_interpolator.IsMinPrecise)
            _interpolator.ToMax();
        _position.y = 1 * 1 * _interpolator.Value + 7.5f;
        _position.x = this.transform.position.x;
        this.transform.position = _position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            switch (randomArma)
            {
                //Basic Weapon Green
                case 0:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<PurpleShoot>().greenPowerUp = true;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<PurpleShoot>().bluePowerUp = false;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = armas[randomArma];
                    break;
                //Basic Weapon Blue
                case 1:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<PurpleShoot>().bluePowerUp = true;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<PurpleShoot>().greenPowerUp = false;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sprite = armas[randomArma];
                    break;
                //Laser Weapon Green
                case 2:
                    break;
                //Laser Weapon Blue
                case 3:
                    break;
                //Shotgun Weapon Green
                case 4:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<RedShoot>().powerUpGreen = true;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<RedShoot>().powerUpBlue = false;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().sprite = armas[randomArma];
                    Debug.Log("Escopeta ve");
                    break;
                //Shotgun Weapon Blue
                case 5:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<RedShoot>().powerUpBlue = true;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<RedShoot>().powerUpGreen = false;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3).GetComponent<SpriteRenderer>().sprite = armas[randomArma];
                    Debug.Log("Escopeta ro");
                    break;
                default:
                    break;
            }
            Destroy(gameObject);

        }
    }
}
