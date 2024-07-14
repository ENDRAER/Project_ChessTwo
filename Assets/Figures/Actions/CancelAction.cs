public class CancelAction : Action
{
    public override void Interacting()
    {
        m_figureScript.selectedAction.DisableAction();
    }
}
