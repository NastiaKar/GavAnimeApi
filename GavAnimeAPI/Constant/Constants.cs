namespace GavAnimeAPI.Constant;

public class Constants
{
    public static string baseAddress = "https://kitsu.io/api/edge/";
    public static string? AccessKey = Environment.GetEnvironmentVariable("AccessKey",
        EnvironmentVariableTarget.Machine);
    public static string? SecretKey = Environment.GetEnvironmentVariable("SecretKey",
        EnvironmentVariableTarget.Machine);

    public static string TableName = "FavoriteAnime";
}