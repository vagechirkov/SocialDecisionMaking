using Unity.MLAgents;
using UnityEngine;

namespace SocialDecisionAgent.Runtime.Group
{
    public class MLAgentGroupController : AgentGroupBase
    {
        
        SimpleMultiAgentGroup _mlAgentGroup;
        
        void Awake()
        {
            InitializeAgentGroup();

            _mlAgentGroup = new SimpleMultiAgentGroup();
            
            foreach (var agent in Agents)
            {
                if (agent is Agent mlAgent)
                {
                    _mlAgentGroup.RegisterAgent(mlAgent);
                }
            }
        }
        
        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !IsTrialRunning)
            {
                IsTrialRunning = true;
                GenerateTrial();
            }

            if (IsTrialRunning)
            {
                resetTimer += 1;
                if (resetTimer >= MaxEnvironmentSteps)
                {
                    _mlAgentGroup.GroupEpisodeInterrupted();
                    IsTrialRunning = false;
                }
            }
        }


    }
}