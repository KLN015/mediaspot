using Mediaspot.Domain.Enums;
using Mediaspot.Domain.Common;

namespace Mediaspot.Domain.Titles;

public class Title: Entity
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public DateTime? ReleaseDate { get; private set; }
    public TitleType Type { get; private set; }

    private Title()
    {
        Name = string.Empty;
        Description = null;
        ReleaseDate = null;
        Type = 0;
    }

    public Title(
        string name,
        TitleType type,
        string? description = null,
        DateTime? releaseDate = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");
        
        Name = name;
        Type = type;
        Description = description;
        ReleaseDate = releaseDate;
    }

    public void Update(
        string name,
        TitleType type,
        string? description,
        DateTime? releaseDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        Name = name;
        Type = type;
        Description = description;
        ReleaseDate = releaseDate;
    }
}