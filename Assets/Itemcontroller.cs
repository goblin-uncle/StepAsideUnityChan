using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemcontroller : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject cornPrefab;
    //Unityちゃんのオブジェクト
    private GameObject unitychan;

    // Start is called before the first frame update
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.z < this.unitychan.transform.position.z-10)
        {
            Destroy(gameObject);
        }
    }
}
