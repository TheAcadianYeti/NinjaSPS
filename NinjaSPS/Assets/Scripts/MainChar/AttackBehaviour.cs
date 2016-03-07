using UnityEngine;
using System.Collections;

public class AttackBehaviour : StateMachineBehaviour {

	GameObject uh = null;
	Ninja mainChar = null;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (uh == null)
		{
			Debug.Log("Yo");
			//Just go ahead and get the damned game object....grr
			Debug.Log(GameObject.FindGameObjectsWithTag("Ninja").Length);
			uh = GameObject.FindGameObjectWithTag("Ninja");
			//uh = GameObject.Find("Ninja");
			mainChar = uh.GetComponent<Ninja>();
		}
	}
	
	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{


		mainChar.doneAttacking();
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
