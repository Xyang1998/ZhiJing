using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Choose1 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("introduction");//level1Ϊ����Ҫ�л����ĳ���
    }

    // Update is called once per frame
    void Update()
    {

    }
}
