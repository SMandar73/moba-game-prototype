# Digital Clock Setup

`DigitalClock.cs` displays the current date and time for multiple time zones in Unity UI.

## Setup

1. Create a Canvas.
2. Add one or more UI `Text` objects, or use TextMeshPro with a small adapter if your project uses TMP.
3. Create an empty GameObject named `DigitalClock`.
4. Add `MobaPrototype.UI.DigitalClock`.
5. Expand the `Clocks` list and assign a label, timezone ID, and output Text component for each clock.
6. Enter Play mode.

## Example timezone IDs

- `Local`
- `UTC`
- `America/New_York`
- `America/Los_Angeles`
- `Europe/London`
- `Europe/Paris`
- `Asia/Kolkata`
- `Asia/Tokyo`
- `Australia/Sydney`

The script uses the platform's `TimeZoneInfo` database and applies daylight-saving changes where the target platform provides them. Some platforms use Windows timezone IDs instead of IANA IDs; if an ID is unavailable, the clock safely falls back to UTC.

## Formatting

- `timeFormat`: default `HH:mm:ss`
- `dateFormat`: default `ddd, dd MMM yyyy`
- `refreshInterval`: default `0.25` seconds
