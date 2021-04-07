using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;

    public float speed = 5f;

    [Space(10)]

    public float screenPartRatio = 3f;

    private void Awake() {
        gameManager = GameManager.Instance;
        inputManager = InputManager.Instance;
    }

    private void OnEnable() {
        inputManager.OnTouch += HandleInput;
    }

    private void OnDisable() {
        //inputManager.OnStopTouch -= Move;
    }

    public void HandleInput(Vector2 touchPosition) {
        if(gameManager.state != GameManager.GameState.RUN) {
            return;
        }

        if(0 < touchPosition.x && touchPosition.x < Screen.width/screenPartRatio) {
            Move(Vector3.left);
        }
        else if((screenPartRatio-1) / screenPartRatio * Screen.width < touchPosition.x && touchPosition.x < Screen.width) {
            Move(Vector3.right);
        }
    }

    public void Move(Vector3 direction) {        
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction*100, speed * Time.deltaTime);
    }
}
