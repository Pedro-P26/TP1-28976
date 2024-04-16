using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics.Contracts;

public class SnakeController : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;

    public float movespeed = 5;
    public float steerspeed = 100;
    public int Gap;
    public int score;

    public AudioSource SnakeEat;

    //public Text ScorePoints;
    public Text scoreText;

    public GameObject bodyprefab;

    public GameObject gotext;

    public GameObject restart;

    
    List<GameObject> bodylist = new List<GameObject>();

    List<Vector3> PositionHistory = new List<Vector3>();



    private void Start()
    {
        

        //growsnake();
       gotext.SetActive(false);

        

        restart.SetActive(false);

        

    }

    

    // Update is called once per frame
    void Update()
    {
        //Movimento da cobra consuante a direção e a velocidade
        transform.position += transform.forward * movespeed * Time.deltaTime;
        //Movimentar a cobra para a esquerda ou direita
        float steerdirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerdirection * steerspeed *  Time.deltaTime);
        

        PositionHistory.Insert(0, transform.position);

        int index = 0;

        //Posição da cabeça da cobra com as partes do corpo

        foreach(var body  in bodylist)
        {
            Vector3 point = PositionHistory[Mathf.Clamp(index * Gap, 0, PositionHistory.Count - 1)];


            Vector3 movedirection = point - body.transform.position;
            body.transform.position += movedirection * movespeed * Time.deltaTime;

            body.transform.LookAt(point);

            index++;
        }

        
        //ScorePoints.text = score.ToString();
        scoreText.text = score.ToString();

    }


    //Cria uma nova instancia do corpo da cobra e adiciona á lista
    void growsnake()
    {
        GameObject body = Instantiate(bodyprefab);
        //body.tag = "snakebody";
        bodylist.Add(body);
    }


    //Verifica se bate contra estes gameobjects e efetua coisas

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "apple")
        {
            
            growsnake();
            Destroy(other.gameObject);
            score++;
            movespeed = movespeed + 0.1f;

           

        }

        if(other.gameObject.tag == "walll" )
        {
            gotext.SetActive(true);
            Time.timeScale = 0;
            restart.SetActive(true);
        }

        
    }

    //Reinicia o jogo
    public void restartbutton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

}
