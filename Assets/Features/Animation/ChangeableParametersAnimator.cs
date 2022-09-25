namespace Features.Animation
{
  public class ChangeableParametersAnimator : SimpleAnimator
  {
    private AnimatorParametersChanger parametersChanger;

    public override void Initialize()
    {
      base.Initialize();
      parametersChanger = new AnimatorParametersChanger(animator, animator.parameters);
    }

    public void ChangeParameter(int hashValue, float endValue, float duration)
    {
      parametersChanger.ChangeParameter(hashValue, endValue, duration);
    }
  }
}