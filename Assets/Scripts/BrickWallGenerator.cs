using UnityEngine;

public class BrickWallGenerator : MonoBehaviour
{
    [SerializeField] GameObject brickPrefab; // Reference to the brick prefab
    [SerializeField] int rows = 5; // Number of rows of bricks
    [SerializeField] int columns = 10; // Number of columns of bricks
    [SerializeField] float spacing = 0.4f; // Spacing between bricks
    [SerializeField] float brickRatio = 3; // Width to height ratio of each brick
    [SerializeField] float unbreakableChance = 0.1f;
    private float brickWidth; // Width of each brick
    private float brickHeight; // Height of each brick
    private Vector3 startPosition; // Starting position of the wall
    private int maxStrength = Colors.strengthColors.Length; // Maximum strength for bricks


    void Start()
    {
        AdjustBrickSize();
        AdjustStartPosition();
        GenerateWall();
    }

    void AdjustBrickSize()
    {
        float screenWidth = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x * 2;

        float itemWidth = screenWidth / columns;

        brickWidth = itemWidth - spacing;

        brickHeight = brickWidth / brickRatio;
    }

    void AdjustStartPosition()
    {
        // Get the upper-left corner of the screen in world coordinates
        Vector3 upperLeftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));

        // Offset by half the brick's width and height to align bricks properly
        upperLeftCorner.x += brickWidth / 2 + spacing;
        upperLeftCorner.y -= brickHeight / 2 + spacing;

        startPosition = upperLeftCorner;
    }

    void GenerateWall()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // Calculate brick position
                float xPosition = startPosition.x + column * (brickWidth + spacing);
                float yPosition = startPosition.y - row * (brickHeight + spacing);

                Vector3 position = new Vector3(xPosition, yPosition, 0);

                brickPrefab.transform.localScale = new Vector3(brickWidth, brickHeight, 1);

                // Instantiate the brick
                GameObject brick = Instantiate(brickPrefab, position, Quaternion.identity);

                // Configure brick properties
                Brick brickScript = brick.GetComponent<Brick>();
                brickScript.strengthColors = Colors.strengthColors;
                if (brickScript != null)
                {
                    // Assign strength and unbreakable status
                    if (Random.value < unbreakableChance)
                    {
                        brickScript.unbreakable = true;
                    }
                    else
                    {
                        brickScript.strength = Random.Range(1, maxStrength + 1);
                    }
                }

                brick.transform.parent = transform;
            }
        }
    }
}
