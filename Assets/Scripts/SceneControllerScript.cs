using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore; 

public class SceneControllerScript : MonoBehaviour
{
    [SerializeField]
    public GameObject leftWall;
    public GameObject rightWall;

    private Vector2 screenBounds;
    public GameObject player;
    
    private float playerWidth;
    public GameObject[] platforms;

    private Vector2 platformSpawn;

    private float counter = 2;

    private Vector2 cameraArea;
    private Vector2 playerPosition;
    private float verticalOffset;

    public Text scoreTxt;

    public int points;

    // Start is called before the first frame update
    void Start()
    {
        playerWidth = player.GetComponent<SpriteRenderer>().bounds.size.x / 2;

        verticalOffset = -2.5f;

        screenBounds = Camera.main.WorldToScreenPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        leftWall.transform.position = new Vector2 (leftWall.transform.position.x, player.transform.position.y);
        rightWall.transform.position = new Vector2 (rightWall.transform.position.x, player.transform.position.y);

        platformSpawn = new Vector2(cameraArea.x, verticalOffset);

        counter -= Time.deltaTime;

        cameraArea = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), 0, Camera.main.transform.position.z));

        if (counter <= 0)
        {
            playerPosition = new Vector2(0f, verticalOffset += 2);
            Instantiate(platforms[Random.Range(0, platforms.Length)], platformSpawn, platforms[Random.Range(0, platforms.Length)].transform.rotation);
            counter = 2;
            
        }

        scoreTxt.text = points.ToString();

        
    }

    private void LateUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        
        playerPosition.x = Mathf.Clamp(playerPosition.x, screenBounds.x * -1 - playerWidth, screenBounds.x + playerWidth);
        player.transform.position = playerPosition;
    }

    
}
