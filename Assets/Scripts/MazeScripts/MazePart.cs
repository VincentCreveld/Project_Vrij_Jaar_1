using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePart : MonoBehaviour, IHighlightable {

    MazeController mazeController;

    public enum MazePartType { coverplate, endblock, startblock};
    public MazePartType mazePartType;

	void Start () {
        mazeController = transform.parent.GetComponent<MazeController>();
    }

    public void Highlight()
    {
        Debug.Log("highlighting");
        if (mazeController.completed == false)
        {
            if (mazeController.inMaze)
            {
                if (Input.GetButton("Fire2"))
                    mazeController.ExitMaze();
            }
                switch ((int)mazePartType)
            {
                case 0:
                    if (mazeController.inMaze)
                    {
                        mazeController.ExitMaze();
                    }
                    break;
                case 1:
                    if (mazeController.inMaze)
                    {
                        if (Input.GetButton("Fire1"))
                            mazeController.FinishMaze();
                    }
                    break;
                case 2:
                    if(Input.GetButton("Fire1"))
                        mazeController.StartMaze();
                    break;
                default:
                    break;
            }
        }
    }

    public void DeHighlight()
    {
    }

    public Material ReturnMatGO()
    {
        return gameObject.GetComponent<MeshRenderer>().material;
    }
}
