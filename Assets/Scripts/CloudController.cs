using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CloudController : MonoBehaviour
{
    public GameObject cloud0;
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject cloud4;
    public GameObject cloud5;
    public GameObject cloud6;
    public GameObject cloud7;
    public GameObject cloud8;
    public static int maxX = 200;
    public static int maxZ = 200;
    public int maxC = 30;
    public float minY = 40.0f;
    public float speed = 1.0f;

    private List<GameObject> cloudList;
    private float cloudD;

    // Start is called before the first frame update
    void Start()
    {
        cloudD = Random.Range(0, 360);

        cloudList = new List<GameObject>();
        cloudList.Add(cloud0);
        cloudList.Add(cloud1);
        cloudList.Add(cloud2);
        cloudList.Add(cloud3);
        cloudList.Add(cloud4);
        cloudList.Add(cloud5);
        cloudList.Add(cloud6);
        cloudList.Add(cloud7);
        cloudList.Add(cloud8);

        for (int i = 0; i < maxX; i++)
        {
            initiateCloud();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void initiateCloud()
    {
        var cloudX = Random.Range(-maxX, maxX);
        var cloudZ = Random.Range(-maxZ, maxZ);
        var cloudY = minY + Random.Range(0, 10);
        var cloud = Instantiate(cloudList[Random.Range(0, cloudList.Count - 1)], new Vector3(cloudX, cloudY, cloudZ), Quaternion.Euler(0, cloudD, 0), transform);
        var cloudS = Random.Range(0, 2);
        cloud.transform.localScale += new Vector3(cloudS, cloudS, cloudS);
        cloud.GetComponent<Cloud>().Init(Random.Range(speed * 0.2f, speed * 0.5f));
    }
}
