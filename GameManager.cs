using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public TextMeshProUGUI UIPoint;
    public TextMeshProUGUI UIStage;
    public GameObject UIRestartBtn;

    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();    
    }

    public void NextStage()
    {
        // Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE" + (stageIndex+1);
        } else  // Game Clear
        {
            //Player Control Lock
            Time.timeScale = 0;
            // Restart Button UI
            TextMeshProUGUI btnText = UIRestartBtn.GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = "Clear";
            ViewBtn();
        }
            
        // Calculate Point

        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            //All Health UI Off
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
            //Player Die Effect
            player.OnDie();
            //Result UI
            Debug.Log("�׾����ϴ�!");
            //Retry Button UI
            ViewBtn();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            // Player Reposition
            if (health > 1)
            {
                PlayerReposition();
            }
            //Health Down
            HealthDown();
        }    
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ViewBtn()
    {
        UIRestartBtn.SetActive(true);
    }
}