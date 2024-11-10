namespace ProtoWorld
{
    public class PlayerService : SingletonMonoBehaviour<PlayerService>
    {
        private PlayerController playerController;
        public PlayerController Controller => playerController;
        
        public void AddPlayerController(PlayerController playerController)
        {
            this.playerController = playerController;
        }
    }
}
