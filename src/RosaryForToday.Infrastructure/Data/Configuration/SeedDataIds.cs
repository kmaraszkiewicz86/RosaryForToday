namespace RosaryForToday.Infrastructure.Data.Configuration;

public static class SeedDataIds
{
    public static class Languages
    {
        public const int English = 1;
        public const int Polish = 2;
    }

    public static class RosaryTypes
    {
        // Polish records
        public const int JoyfulPolish = 1;
        public const int SorrowfulPolish = 2;
        public const int LuminousPolish = 3;
        public const int GloriousPolish = 4;

        // English counterparts (sequential next IDs)
        public const int JoyfulEnglish = 5;
        public const int SorrowfulEnglish = 6;
        public const int LuminousEnglish = 7;
        public const int GloriousEnglish = 8;
    }
}