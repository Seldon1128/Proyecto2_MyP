using TagLib;
using System;

public class MP3TagExtractor
{
    public (string Performer, string Title, string Album, uint Track, uint Year, string Genre) ExtractTags(string filePath)
    {
        var file = TagLib.File.Create(filePath);

        string performer = file.Tag.FirstPerformer ?? "Unknown";
        string title = file.Tag.Title ?? "Unknown";
        string album = file.Tag.Album ?? "Unknown";
        uint track = file.Tag.Track > 0 ? file.Tag.Track : 1;
        uint year = file.Tag.Year > 0 ? file.Tag.Year : (uint)DateTime.Now.Year;
        string genre = file.Tag.FirstGenre ?? "Unknown";

        return (performer, title, album, track, year, genre);
    }
}
