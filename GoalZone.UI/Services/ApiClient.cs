using GoalZone.UI.Models.Components;
using GoalZone.WebUI.ViewComponents;
using System.Text;
using System.Text.Json;

namespace GoalZone.WebUI.Services
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        private readonly ILogger<ApiClient> _logger;
        private readonly JsonSerializerOptions _json = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        public ApiClient(HttpClient http, ILogger<ApiClient> logger)
        {
            _http = http;
            _logger = logger;
        }

        private async Task<T?> GetAsync<T>(string url) where T : class
        {
            try
            {
                var res = await _http.GetAsync(url);
                if (!res.IsSuccessStatusCode)
                {
                    _logger.LogWarning("GET {Url} returned {Status}", url, res.StatusCode);
                    return null;
                }
                var json = await res.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(json, _json);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET {Url} failed", url);
                return null;
            }
        }

        private async Task<bool> SendAsync(HttpMethod method, string url, object? body = null)
        {
            try
            {
                HttpContent? content = null;
                if (body != null)
                    content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage(method, url) { Content = content };
                var res = await _http.SendAsync(request);
                if (!res.IsSuccessStatusCode)
                    _logger.LogWarning("{Method} {Url} returned {Status}", method, url, res.StatusCode);
                return res.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Method} {Url} failed", method, url);
                return false;
            }
        }

        public async Task<List<MatchListViewModel>> GetMatchesAsync(int? seasonId = null)
        {
            var url = seasonId.HasValue ? $"api/matches?seasonId={seasonId}" : "api/matches";
            return await GetAsync<List<MatchListViewModel>>(url) ?? new();
        }

        public async Task<List<MatchListViewModel>> GetLiveMatchesAsync() =>
            await GetAsync<List<MatchListViewModel>>("api/matches/live") ?? new();

        public async Task<List<MatchListViewModel>> GetMatchesByWeekAsync(int week, int? seasonId = null)
        {
            var url = seasonId.HasValue ? $"api/matches/week/{week}?seasonId={seasonId}" : $"api/matches/week/{week}";
            return await GetAsync<List<MatchListViewModel>>(url) ?? new();
        }

        public async Task<MatchDetailViewModel?> GetMatchDetailAsync(int id) =>
            await GetAsync<MatchDetailViewModel>($"api/matches/{id}");

        public async Task<List<StandingViewModel>> GetStandingsAsync(int? seasonId = null)
        {
            var url = seasonId.HasValue ? $"api/standings?seasonId={seasonId}" : "api/standings";
            return await GetAsync<List<StandingViewModel>>(url) ?? new();
        }

        public async Task<SeasonViewModel?> GetActiveSeasonAsync() =>
            await GetAsync<SeasonViewModel>("api/seasons/active");

        public async Task<List<SeasonViewModel>> GetSeasonsAsync() =>
            await GetAsync<List<SeasonViewModel>>("api/seasons") ?? new();

        public async Task<bool> ActivateSeasonAsync(int seasonId) =>
            await SendAsync(HttpMethod.Put, $"api/seasons/{seasonId}/activate");

        public async Task<bool> CreateSeasonAsync(string name, int startYear, int endYear, bool setActive) =>
            await SendAsync(HttpMethod.Post, "api/seasons",
                new { name, startYear, endYear, isActive = setActive });

        public async Task<List<TeamViewModel>> GetSeasonTeamsAsync(int seasonId) =>
            await GetAsync<List<TeamViewModel>>($"api/seasons/{seasonId}/teams") ?? new();

        public async Task<bool> AddTeamToSeasonAsync(int seasonId, int teamId) =>
            await SendAsync(HttpMethod.Post, $"api/seasons/{seasonId}/teams/{teamId}");

        public async Task<bool> RemoveTeamFromSeasonAsync(int seasonId, int teamId) =>
            await SendAsync(HttpMethod.Delete, $"api/seasons/{seasonId}/teams/{teamId}");

        public async Task<List<TeamViewModel>> GetTeamsAsync() =>
            await GetAsync<List<TeamViewModel>>("api/teams") ?? new();

        public async Task<bool> ToggleTeamActiveAsync(int teamId) =>
            await SendAsync(HttpMethod.Put, $"api/teams/{teamId}/toggle");

        public async Task<List<RefereeViewModel>> GetRefereesAsync() =>
            await GetAsync<List<RefereeViewModel>>("api/referees") ?? new();

        public async Task<bool> LoginAsync(string username, string password) =>
            await SendAsync(HttpMethod.Post, "api/auth/login", new { username, password });

        public async Task<int?> CreateMatchAsync(object request)
        {
            try
            {
                var body = JsonSerializer.Serialize(request);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var res = await _http.PostAsync("api/admin/matches", content);
                if (!res.IsSuccessStatusCode) return null;
                var json = await res.Content.ReadAsStringAsync();
                var doc = JsonDocument.Parse(json);
                return doc.RootElement.GetProperty("id").GetInt32();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CreateMatch failed");
                return null;
            }
        }

        public async Task<bool> UpdateScoreAsync(int matchId, object request) =>
            await SendAsync(HttpMethod.Put, $"api/admin/matches/{matchId}/score", request);

        public async Task<bool> AddEventAsync(int matchId, object request) =>
            await SendAsync(HttpMethod.Post, $"api/admin/matches/{matchId}/events", request);

        public async Task<bool> DeleteEventAsync(int eventId) =>
            await SendAsync(HttpMethod.Delete, $"api/admin/matches/events/{eventId}");

        public async Task<bool> UpsertStatisticsAsync(int matchId, object request) =>
            await SendAsync(HttpMethod.Put, $"api/admin/matches/{matchId}/statistics", request);
    }
}
