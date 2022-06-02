using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;

public class ResetLevel : MonoBehaviour
{

    [SerializeField] private Player.PlayerControl gamer;
    private bool dying;

    void Start() {
        dying = false;    
    }

    void Update() {
        dying = gamer.die;

        if(dying == true){
            Invoke("Reload", 3f);
        }
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        dying = false;
    }

}
