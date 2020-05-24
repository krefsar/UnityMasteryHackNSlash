using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    private List<Controller> controllers;

    private void Awake()
    {
        controllers = FindObjectsOfType<Controller>().ToList();

        int index = 1;
        foreach (var controller in controllers)
        {
            controller.SetIndex(index);
            index++;
        }
    }

    private void Update()
    {
        foreach (var controller in controllers)
        {
            if (controller.IsAssigned == false && controller.AnyButtonDown())
            {
                AssignController(controller);
            }

        }
    }

    private void AssignController(Controller controller)
    {
        controller.IsAssigned = true;
        Debug.Log("Assigned Controller " + controller.gameObject.name);

        FindObjectOfType<PlayerManager>().AddPlayerToGame(controller);
    }
}
