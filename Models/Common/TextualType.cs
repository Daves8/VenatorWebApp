using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models.Common
{
    public enum TextualType
    {
        News,
        Topic,
        Comment,
        Message
    }

    public static class TextualTypeConvertion
    {
        public static TextualType GetTextualType(Textual textual)
        {
            return textual switch
            {
                Comment => TextualType.Comment,
                News => TextualType.News,
                Topic => TextualType.Topic,
                Message => TextualType.Message,
                _ => throw new ArgumentException($"TextualType not found for {textual}"),
            };
        }
    }
}
