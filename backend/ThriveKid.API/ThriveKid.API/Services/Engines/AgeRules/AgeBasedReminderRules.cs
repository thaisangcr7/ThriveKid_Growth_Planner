using ThriveKid.API.Models;

namespace ThriveKid.API.Services.Engines.AgeRules
{
    public static class AgeBasedReminderRules
    {
        public static IEnumerable<(string Title, string Notes, RepeatRule Repeat)> GetForAgeMonths(int months)
        {
            if (months < 6)
                yield return ("Tummy time 10–20 min", "Daily supervised tummy time to build neck/core.", RepeatRule.DAILY);
            else if (months < 12)
                yield return ("Stacking cups play 15 min", "Encourage fine motor + problem solving.", RepeatRule.DAILY);
            else if (months < 24)
                yield return ("Read a picture book", "10 minutes of reading together.", RepeatRule.DAILY);
            else
                yield return ("Outdoor play 20 min", "Gross motor + sunlight time.", RepeatRule.DAILY);
        }
    }
}