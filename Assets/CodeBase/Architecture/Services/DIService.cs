using Zenject;

namespace CodeBase.Architecture.Services
{
  public class DIService
  {
    private static DIService _instance;
    public static DIService Instance => _instance ?? (_instance = new DIService());

    public DiContainer Container = ProjectContext.Instance.Container;
    public DiContainer LocalDi;

    public void WarmUp()
    {
       LocalDi = new DiContainer(Container);
    }

    public void CleanUp()
    {
       LocalDi = new DiContainer();
    }
   }
}