using Newtonsoft.Json;

namespace RpDev.UserData
{
    public class UserData
    {
        [JsonProperty("lastLevelIndex")]  private int _lastLevelIndex;
		[JsonProperty("passedLevels")] private int _passedLevels;
        
        [JsonIgnore]
        public int LastLevelIndex
        {
            get => _lastLevelIndex;
            set => _lastLevelIndex = value;
        }

        [JsonIgnore] 
        public int PassedLevels
        {
            get => _passedLevels;
            set => _passedLevels = value;
        }
    }
}