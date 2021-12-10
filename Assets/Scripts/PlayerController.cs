using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Transform bulletPos;
    public GameObject[] bullets;

    private SerialPort arduinoStream;
    public string port;   //arduino端口号
    private Thread readThread; // 宣告執行緒
    public string readMessage;
    bool isNewMessage;

    private float h;
    private int bulletNum = 0;

    void Start()
    {
        if (port != "")
        {
            arduinoStream = new SerialPort(port, 9600); //指定連接埠、鮑率並實例化SerialPort
            arduinoStream.ReadTimeout = 10;
            try
            {
                arduinoStream.Open(); //開啟SerialPort連線
                readThread = new Thread(new ThreadStart(ArduinoRead)); //實例化執行緒與指派呼叫函式
                readThread.Start(); //開啟執行緒
                Debug.Log("SerialPort 连接成功");
            }
            catch
            {
                Debug.Log("SerialPort 连接失败");
            }
        }
    }

    void Update()
    {
        //float h = Input.GetAxis("Horizontal");
        //Debug.Log(h);
        //if(readMessage != null) {
        //    string b = "2";
        //    int a = int.Parse(b);
        //    Debug.Log(a == -1);
        //}
        if(readMessage != null && int.Parse(readMessage) != 2) {
            h = int.Parse(readMessage);
        }

        transform.Translate(Vector3.right * h * Time.deltaTime * speed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5, 5), 1, -28);  //contain

        if(readMessage != null){
            if (int.Parse(readMessage) == 2)  //Input.GetMouseButtonDown(0)
            {

                Instantiate(bullets[bulletNum], bulletPos.position, Quaternion.identity);
            }
        }
   

        if (Input.GetKeyDown(KeyCode.Q))
        {
            bulletNum++;
            if (bulletNum>1)
            {
                bulletNum = 0;
            }
        }

        //port massages
        if (isNewMessage)
        {
            Debug.Log(readMessage);
        }
        else
        {
            readMessage = null;
        }
        isNewMessage = false;
    }

    private void ArduinoRead()
    {
        while (arduinoStream.IsOpen)
        {
            try
            {
                readMessage = arduinoStream.ReadLine(); // 讀取SerialPort資料並裝入readMessage

                isNewMessage = true;
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }
    public void ArduinoWrite(string message)
    {
        Debug.Log(message);
        try
        {
            arduinoStream.Write(message);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
    void OnApplicationQuit()
    {
        if (arduinoStream != null)
        {
            if (arduinoStream.IsOpen)
            {
                arduinoStream.Close();
            }
        }
    }
}
