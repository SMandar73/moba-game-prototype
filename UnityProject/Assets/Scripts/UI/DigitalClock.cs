using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace MobaPrototype.UI
{
    [Serializable]
    public class ClockTimeZone
    {
        public string displayName = "Local Time";
        [Tooltip("Windows/IANA timezone ID, for example UTC, America/New_York, Europe/London, Asia/Tokyo.")]
        public string timeZoneId = "UTC";
        public Text output;
    }

    /// <summary>
    /// Displays the current system time in one or more time zones.
    /// Uses TimeZoneInfo so daylight-saving rules are applied when supported by the platform.
    /// </summary>
    public class DigitalClock : MonoBehaviour
    {
        [SerializeField] private List<ClockTimeZone> clocks = new()
        {
            new ClockTimeZone { displayName = "Local", timeZoneId = "Local" },
            new ClockTimeZone { displayName = "UTC", timeZoneId = "UTC" },
            new ClockTimeZone { displayName = "New York", timeZoneId = "America/New_York" },
            new ClockTimeZone { displayName = "London", timeZoneId = "Europe/London" },
            new ClockTimeZone { displayName = "Tokyo", timeZoneId = "Asia/Tokyo" }
        };

        [SerializeField] private string timeFormat = "HH:mm:ss";
        [SerializeField] private string dateFormat = "ddd, dd MMM yyyy";
        [SerializeField] private float refreshInterval = 0.25f;

        private float refreshTimer;

        private void Update()
        {
            refreshTimer -= Time.unscaledDeltaTime;
            if (refreshTimer > 0f) return;

            refreshTimer = Mathf.Max(0.05f, refreshInterval);
            RefreshClocks();
        }

        public void RefreshClocks()
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;

            foreach (ClockTimeZone clock in clocks)
            {
                if (clock == null || clock.output == null) continue;

                TimeZoneInfo zone = ResolveTimeZone(clock.timeZoneId);
                DateTimeOffset zoneTime = TimeZoneInfo.ConvertTime(now, zone);
                clock.output.text = $"{clock.displayName}\n{zoneTime.ToString(timeFormat, CultureInfo.InvariantCulture)}\n{zoneTime.ToString(dateFormat, CultureInfo.InvariantCulture)}";
            }
        }

        private static TimeZoneInfo ResolveTimeZone(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || id.Equals("Local", StringComparison.OrdinalIgnoreCase))
                return TimeZoneInfo.Local;

            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(id);
            }
            catch (TimeZoneNotFoundException)
            {
                Debug.LogWarning($"Timezone '{id}' was not found. Falling back to UTC.");
                return TimeZoneInfo.Utc;
            }
            catch (InvalidTimeZoneException)
            {
                Debug.LogWarning($"Timezone '{id}' is invalid. Falling back to UTC.");
                return TimeZoneInfo.Utc;
            }
        }
    }
}
