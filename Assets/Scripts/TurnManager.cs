﻿/*
  Tutorial For Turn Manager from https://www.youtube.com/watch?v=cX_KrK8RQ2o
  Modified by Edward Reyes
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<GridMovement>> units = new Dictionary<string, List<GridMovement>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<GridMovement> turnTeam = new Queue<GridMovement>();
    public Text teamtext;
    static public bool gameCont = true;
    static public string winner;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0 && gameCont == true)
        {
            InitTeamTurnQueue();
        }
        UIChange();
    }

    static void InitTeamTurnQueue()
    {
        List<GridMovement> teamList = units[turnKey.Peek()];

        foreach(GridMovement unit in teamList)
        {
            //Debug.Log(unit.name);
            turnTeam.Enqueue(unit);
        }
        StartTurn();
    }

    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            Debug.Log(turnTeam.Peek().tag);
            turnTeam.Peek().BeginTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public void UIChange()
    {
        if (gameCont == true)
        {
            if (turnTeam.Peek().tag == "NPC")
            {
                teamtext.text = "Current team: Red team";
            }
            else
            {
                teamtext.text = "Current team: Blue team";
            }
        }
        else
        {
            teamtext.text = "Game Over! " + winner + " Wins!";
        }
    }

    public static void EndTurn()
    {
        GridMovement unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(GridMovement unit)
    {
        List<GridMovement> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<GridMovement>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }
        //Debug.Log(unit.name);
        list.Add(unit);
    }

    public static void RemoveUnit(GridMovement unit)
    {
        //if(turnKey.Contains(unit.tag))
        //{
        //Debug.Log(units);
        //Debug.Log(turnKey);
        //Debug.Log(turnTeam);
        //List<GridMovement> teamList = units[turnKey.Peek()];
        //foreach (GridMovement target in teamList)
        //{
        //    Debug.Log(target.gameObject.tag);
        //}
        Debug.Log(unit);
        Debug.Log(units[unit.tag]);
        units[unit.tag].Remove(unit);
        if (units[unit.tag].Count == 0)
        {
            //units[unit.tag]);
            Debug.Log("Game Over");
            if (unit.tag == "NPC")
            {
                winner = "Blue";
            }
            else
            {
                winner = "Red";
            }
            gameCont = false;
            
        }
        /*string init = turnKey.Dequeue();
        string team = init;
        while (turnKey.Dequeue() != init)
        {
            turnKey.Enqueue(team);
        }*/

        
        
        //InitTeamTurnQueue();
        
        //units.Remove(unit.tag);
            //turnKey = new Queue<string>(turnKey.Where(p => p != v));
        //}
    }
}
