using GoalZone.UI.Models.Components;
using System.Collections.Generic;

namespace GoalZone.UI.Models.Admin
{
    public class AdminCreateMatchViewModel
    {
        public List<TeamViewModel> Teams { get; set; } = new();
        public List<SeasonViewModel> Seasons { get; set; } = new();
        public List<RefereeViewModel> Referees { get; set; } = new();
    }
}