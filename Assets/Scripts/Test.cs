using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Text test;
    public SkinnedMeshRenderer CLouth;

    void Update() {
        test.text = "" + CLouth.bounds.extents;
    }
}
