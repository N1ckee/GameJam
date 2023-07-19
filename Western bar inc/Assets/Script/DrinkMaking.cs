using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class DrinkMaking : MonoBehaviour
{
    public string Drink = "";
    public float Cash = 0.5f;
    public float Quality = 5;
    public MakingPosition[] makingPositionList;

    public int CurrentTask = 0;
    private bool IsGoingToPosition = false;

    bool IsSaying = false;

    public IEnumerator StartMaking(NpcController Controller)
    {
        if (CurrentTask < makingPositionList.Length)
        {
            if (Controller.WayPoints.Count <= 1 && !IsGoingToPosition)
            {
                int TheLocation = 0;
                if (makingPositionList[CurrentTask].Pos.Length > 1)
                {
                    TheLocation = Random.Range(0, makingPositionList[CurrentTask].Pos.Length);
                }

                if (!IsSaying)
                {
                    Controller.SaySomething(makingPositionList[CurrentTask].Saying);
                    IsSaying = true;
                }

                Controller.FindNewpath(Controller.PreviousPosition, makingPositionList[CurrentTask].Pos[TheLocation]);
                IsGoingToPosition = true;
            }

            if (Controller.WayPoints.Count <= 1 && IsGoingToPosition)
            {
                yield return new WaitForSeconds(makingPositionList[CurrentTask].TimeToMake);

                CurrentTask++;
                IsGoingToPosition = false;
                IsSaying = false;
            }
            yield return null;
        }
    }
}

[System.Serializable]
public class MakingPosition
{
    [SerializeField]
    private string actionName;

    [SerializeField]
    private Vector2Int[] pos;

    [SerializeField]
    private float timeToMake;

    [SerializeField]
    private string saying;

    public string ActionName
    {
        get { return actionName; }
        set { actionName = value; }
    }

    public Vector2Int[] Pos
    {
        get { return pos; }
        set { pos = value; }
    }

    public float TimeToMake
    {
        get { return timeToMake; }
        set { timeToMake = value; }
    }

    public string Saying
    {
        get { return saying; }
        set { saying = value; }
    }
}