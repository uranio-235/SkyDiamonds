using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Helper;

public class GemaController : MonoBehaviour
{
    // Update is called once per frame
    private void Update() =>
        transform.Rotate(new Vector3(N.Random(10,25),N.Random(25,40),N.Random(40,60)) * Time.deltaTime);       
}
