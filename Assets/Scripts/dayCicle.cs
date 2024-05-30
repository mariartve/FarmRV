using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayCicle : MonoBehaviour
{
    // Start is called before the first frame update

    public int rotation = 2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime, 0, 0);
    }
}
