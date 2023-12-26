using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class GameEnvController : GoalTrigger
{
    public int buttonsOnEpisode = 2;
    public int boxesOnEpisode = 8;

    private AgentPusher agent;
    public GridedDistributor buttonsDistributor;
    public GridedDistributor boxDistributor;
    public GridedDistributor agentsDistributor;
    public Door door;
   // public GoalTrigger goalTriggered;

    void FixedUpdate()
    {
    }

    void Start()
    {
        ResetScene();
    }

    void ResetScene()
    {
        var buttons = buttonsDistributor.Respawn(buttonsOnEpisode);
        boxDistributor.Respawn(boxesOnEpisode);
        var activators = new DoorActivator[buttons.Length];
        for (var i = 0; i < buttons.Length; i++)
        { activators[i] = buttons[i].GetComponent<Button>();
            activators[i].onActivate.RemoveAllListeners();
            activators[i].onDeactivate.RemoveAllListeners();
            activators[i].onBoxActivate.RemoveAllListeners();
            activators[i].onBoxDeactivate.RemoveAllListeners();
            activators[i].onActivate.AddListener(OnActivate);
            activators[i].onDeactivate.AddListener(OnDeactivate);
            activators[i].onBoxActivate.AddListener(OnBoxActivate);
            activators[i].onBoxDeactivate.AddListener(OnBoxDeactivate);
        }
        door.ResetActivators(activators);
        agent = agentsDistributor.Respawn(1)[0].GetComponent<AgentPusher>();
    }
    public void OnActivate()
    {
        if (!door.isDoorOpened) {
            agent.AddReward(0.7f);
        }
        
    }
    public void OnDeactivate()
    {
      //  agent.AddReward(-0.1f);

    }
    public void OnBoxActivate()
    {
        //agent.AddReward(0.3f);
    }
    public void OnBoxDeactivate()
    {
        //agent.AddReward(-0.25f);     
    }
   
    public void finishedd()
    {
  
        agent.AddReward(0.3f);
        agent.EndEpisode();
        ResetScene();
    }

    public void OnGoalTriggered()
    {
        agent.AddReward(0.2f);
      //  agent.EndEpisode();
      //  ResetScene();
    }

}
