using Tanks.GameLogic;

namespace Tanks.Tests.PlayMode.ECS
{
    public class InitSystemTests
    {
        private GameLogicController _gameLogicController;

       /* [UnityTest]
        public IEnumerator CreateGameController()
        {
            SceneStaticData staticData =
                Resources.Load<SceneStaticData>("Data/Scenes/MainSceneStaticData");
            RuntimeData runtimeData = Resources.Load<RuntimeData>("Data/Runtime/RuntimeData");
            IInputService inputService = new InputService();
            IPoolService poolService = new PoolService();
            
            _logicController = new GameObject().AddComponent<LogicController>();
            _logicController.Construct(poolService, inputService, staticData, runtimeData);
            yield return null;
        }
        
        [Test]
        public void WhenCreateTank_AndTeamRed_ThenRenderersColorRed()
        {
            var entities =
                _logicController.Contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Team, GameMatcher.MeshRenderer))
                    .GetEntities()
                    .Where(entity => entity.team.Type == TeamType.Red);
            foreach (var entity in entities)
            {
                foreach (var meshRenderer in entity.meshRenderer.Array)
                {
                    meshRenderer.material.color.Should().Be(Color.red);
                }
            }
        }*/
    }
}