using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CanonRotation : MonoBehaviour
{
    public Vector3 _maxRotation;
    public Vector3 _minRotation;
    private float offset = -51.6f;
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed = 0f;
    public float MaxSpeed;
    public float MinSpeed;
    public GameObject PotencyBar;
    private float initialScaleX;

    private void Awake()
    {
        initialScaleX = PotencyBar.transform.localScale.x;
    }
    void Update()
    {
        //PISTA: mireu TOTES les variables i feu-les servir

        var mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);//guardem posici� del ratol� a la c�mera
        mousePos.z = 0;
        var direction = mousePos - gameObject.transform.position;//vector entre el click i la bala
        var angle = (Mathf.Atan2(direction.y, direction.x) * 180f / Mathf.PI + offset);       
        transform.rotation = Quaternion.Euler(0,0,angle); //aplicar rotaci� de l'angle al can�  */

        if (Input.GetMouseButton(0))
        {
            ProjectileSpeed += 0.04f;//cada segon s'ha de fer 4 unitats m�s gran
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            var projectile = Instantiate(Bullet, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(mousePos.x + ProjectileSpeed, mousePos.y + ProjectileSpeed);//quina velocitat ha de tenir la bala? 
            ProjectileSpeed = 0f; //reset despr�s del tret
        }
        CalculateBarScale();

    }
    public void CalculateBarScale()
    {
        PotencyBar.transform.localScale = new Vector3(Mathf.Lerp(0, initialScaleX, ProjectileSpeed / MaxSpeed),
            PotencyBar.transform.localScale.y,
            PotencyBar.transform.localScale.z);
    }
}
