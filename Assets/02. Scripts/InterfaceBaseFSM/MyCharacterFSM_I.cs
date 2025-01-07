using System.Collections;
using UnityEngine;

public class MyCharacterFsm_I : MonoBehaviour
{
    private StateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        stateMachine.Run();
    }
    // Update is called once per frame
    void Update()
    {
        stateMachine.UpdateState();
    }
}
