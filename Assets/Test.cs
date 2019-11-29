using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public GameObject Cube;
    public GameObject Sphere;
    public bool isTop;
    public float SphereY;

    // Start is called before the first frame update
    void Start()
    {
        isTop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTop)
            {
                SphereY = Sphere.transform.transform.position.y;
                Sphere.transform.DOMoveY(1, 0.5f);
//                Sphere.transform.DOMoveY(1, 0.5f).SetRelative();
                Cube.transform.DORotate(new Vector3(90, 0, 0), 0.5f);
            }
            else
            {
//                Sphere.transform.DOMoveY(-1, 0.5f).SetRelative();
                Sphere.transform.DOMoveY(SphereY, 0.5f);
                Cube.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
            }

            isTop = !isTop;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (isTop)
        {
            Sphere.transform.Translate(Vector3.right * h * Time.deltaTime);
            Sphere.transform.Translate(Vector3.up * v * Time.deltaTime);
        }
        else
        {
            Sphere.transform.Translate(Vector3.right * h * Time.deltaTime);
        }
    }
}