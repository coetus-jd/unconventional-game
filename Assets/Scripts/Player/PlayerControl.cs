using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using UnityEngine;

namespace Player
{

public class PlayerControl : MonoBehaviour
{

    [SerializeField] private Animator anim;
    private Thread receiveThread;
	private UdpClient client;
	private int port;
	[SerializeField] private GameObject Player;
	[SerializeField] private float jumpForce;
    private Rigidbody2D rb;
    private bool jump;
    private bool slide;
    private bool jEnabled;
    public bool die;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        port = 7777;
        jump = false;
        jEnabled = true;
        die = false;

        InitUDP();    
    }

    private void InitUDP()
    {
        print ("UDP Initialized");

		receiveThread = new Thread (new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start ();

    }
    private void ReceiveData()
	{
		client = new UdpClient (port);
		while (true)
		{
			try
			{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port);
				byte[] data = client.Receive(ref anyIP);

                string text = Encoding.UTF8.GetString(data);
                string up = "UP";
                string down = "DOWN";
                
                if(up == text)
                {
                    jump = true;
                }
                else if(down == text)
                {
                    slide = true;
                }


			} catch(Exception e)
			{
				print (e.ToString());
			}
		}
	}

    void Update()
    {
        movement();
    }

    void movement()
    {
        if(jump == true && jEnabled == true && slide == false)
        {
            rb.AddForce(
			Vector3.up * jumpForce,
			ForceMode2D.Impulse
			);
            anim.SetBool("Jumping", true);

        }
        else if(slide == true && jump == false)
        {
            anim.SetBool("Sliding", true);
        }
        else
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Sliding", false);
        }
        jump = false;
        slide = false;
        
    }

    void jumpEnable(){
        jEnabled = true;
    }
    void jumpDisable(){
        jEnabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        die = true;
        Destroy(this.gameObject);
    }
}
}

