using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionButtons : MonoBehaviour
{
    public enum Action { BULLET, CHARACTER, CHARACTERANDBULLET };
    private Action currentAction;

    public Action getCurrentAction()
    {
        return currentAction;
    }

    public void Bullet()
    {
        currentAction = Action.BULLET;
        Debug.Log("A: "+getCurrentAction());
    }

    public void Character()
    {
        currentAction = Action.CHARACTER;
        Debug.Log("A: " + getCurrentAction());
    }

    public void CharacterAndBullet()
    {
        currentAction = Action.CHARACTERANDBULLET;
        Debug.Log("A: " + getCurrentAction());
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
