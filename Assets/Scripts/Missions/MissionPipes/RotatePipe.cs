using UnityEngine;

public class RotatePipe : MonoBehaviour
{
    public int rotationIndex = 0;
    public PipeGame pipeGame;

    public void Rotate()
    {
        if (transform.parent != null)
        {
            string parentName = transform.parent.name;

            if (parentName.StartsWith("Pipe"))
            {
                RotatePipe parentPipeScript = transform.parent.GetComponent<RotatePipe>();

                if (parentPipeScript != null)
                {
                    parentPipeScript.rotationIndex = (parentPipeScript.rotationIndex + 1) % 4;
                    transform.parent.Rotate(0, 0, 90);

                    if (pipeGame != null)
                    {
                        string[] nameParts = transform.parent.name.Split('_'); // Ex: "Pipe_2_3"
                        if (nameParts.Length == 3 && int.TryParse(nameParts[1], out int i) && int.TryParse(nameParts[2], out int j))
                        {
                            pipeGame.UpdatePlateau(i, j, rotationIndex);
                            pipeGame.CheckSolution();
                        }
                    }
                }
                return;
            }
        }
        rotationIndex = (rotationIndex + 1) % 4;
        transform.Rotate(0, 0, 90);
        if (pipeGame != null)
        {
            string[] nameParts = transform.name.Split('_'); // Ex: "Pipe_2_3"
            if (nameParts.Length == 3 && int.TryParse(nameParts[1], out int i) && int.TryParse(nameParts[2], out int j))
            {
                pipeGame.UpdatePlateau(i, j, rotationIndex);
                pipeGame.CheckSolution();
            }
        }
    }
}