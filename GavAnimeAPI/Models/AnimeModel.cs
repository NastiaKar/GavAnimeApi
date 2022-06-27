namespace GavAnimeAPI.Models;

public class AnimeModel
{
    public Data Data { get; set; }
}

public class AnimeModelArray
{
    public IEnumerable<Data> Data { get; set; }
}

public class Data
{
    public string Id { get; set; }
    public string Type { get; set; }
    public Attributes Attributes { get; set; }
}

public class Attributes
{
    public string CreatedAt { get; set; }
    public string Slug { get; set; }
    public string Synopsis { get; set; }
    public string AgeRating { get; set; }
    public string AgeRatingGuide { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public Titles Titles { get; set; }
    public PosterImage PosterImage { get; set; }
}

public class Titles
{
    public string En_Jp { get; set; }
    public string Ja_Jp { get; set; }
}

public class PosterImage
{
    public string Large { get; set; }
}