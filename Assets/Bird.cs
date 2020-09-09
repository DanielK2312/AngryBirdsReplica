using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private bool _birdWasLaunched = false;
    private float _timeSittingAround;

    // SerializeField allows modification of a variable within the game engine
    [SerializeField] private float _launchPower = 500;

    private void Awake()
    {
        // whatever position bird is in at start is set to initial position
        // helpful if bird dies and we want to reset inital position
        _initialPosition = transform.position; 
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(1, transform.position);


        // if the bird has been launched and the velocity of the bird is moving very slowly
        if (_birdWasLaunched &&
            GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1f)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > 10 || 
            transform.position.y < -10 ||
            transform.position.x > 10 ||
            transform.position.x <-10 ||
            _timeSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1; // We want the bird to drop after released
        _birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        // from camera plain, birds position will move based on where it is dragged to
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // sets the position to whereever the x and y are moved by the mouse leaving out the z
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}