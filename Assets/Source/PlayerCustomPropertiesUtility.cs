using ExitGames.Client.Photon;
using Photon.Realtime;

namespace Source
{
    public static class PlayerCustomPropertiesUtility
    {
        public const string SkinId = nameof(SkinId);

        public static int GetSkinId(Player player)
        {
            return (int)player.CustomProperties[SkinId];
        }

        public static void SetSkinId(Player player, int skinId)
        {
            player.SetCustomProperties(new Hashtable { { SkinId, skinId } });
        }
    }
}