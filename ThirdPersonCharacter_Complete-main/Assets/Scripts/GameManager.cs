using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }

    public string room_code;

    private TMP_Text room_text;

    private float timer = 0;
    private bool activate_timer = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (activate_timer)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                room_text = GameObject.Find("Room code text").GetComponent<TMP_Text>();
                if (room_text != null)
                    room_text.text = "Room: " + room_code;
                timer = 0;
                if (room_text != null && room_text.text != "Room text")
                {
                    activate_timer = false;
                }
            }
        }
    }

    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
        if (scene_name == "SampleScene")
        {
            activate_timer = true;
        }
    }
}