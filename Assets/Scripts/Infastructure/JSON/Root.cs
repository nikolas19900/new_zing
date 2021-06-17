using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Infastructure.JSON
{
    [System.Serializable]
    class Root
    {
        public List<PlayClassicData> play_classic { get; set; }

        public List<PlaySpecialData> play_special { get; set; }

        public List<LeaderboardData> leaderboard { get; set; }

        public List<PrivateGameData> private_game { get; set; }

        public List<NoticeData> notice { get; set; }
        
        public List<TutorialData> tutorial { get; set; }
        
        public List<HelpData> help { get; set; }

        public List<VipPackData> vip_pack { get; set; }

        public List<InvitesData> invites { get; set; }

        public List<GiftsData> gifts { get; set; }

        public List<FriendsData> friends { get; set; }
        
        public List<SpecialOffersData> special_offers { get; set; }
    }
}
