using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    public int keys = 0;
    public float speed = 5.0f;
    public Text keyAmount;
    public Text youwin;
    public GameObject door;
    public Light2D playerlight;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(playerlight.pointLightOuterRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(LightOuterGradually());
        }

        if(keys == 3){
            Destroy(door);
        }
    }

    public void OnClick()
    {
        StartCoroutine(LightOuterGradually());
    }

    IEnumerator LightOuterGradually(){
        int loopcount = 20;
        float waitsecond = 0.05f;
        float offsetScale = 5.0f / loopcount;

        var cachedWait = new WaitForSeconds(waitsecond);  

        // playerlight.pointLightOuterRadius = 2.0f;
        for (int loop = 0; loop < loopcount; loop++)
        {
            // update radius
            playerlight.pointLightOuterRadius += offsetScale;
            yield return cachedWait;
        }

        yield return new WaitForSeconds(3);

        for (int loop = 0; loop < loopcount; loop++)
        {
            // update radius
            playerlight.pointLightOuterRadius -= offsetScale;
            yield return cachedWait;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Keys"){
            keys++;
            keyAmount.text = "Keys: " + keys;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "GoldStar"){
            youwin.text = "YOU WIN!!!";
        }
        if(collision.gameObject.tag == "Enemies"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        //How to bounce of walls
        if(collision.gameObject.tag == "Walls"){
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
            }
            if(Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            if(Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }
            if(Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, speed * Time.deltaTime, 0);
            }
        }
    }
}
