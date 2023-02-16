public class ProductItem
{
    public string language;

    public Film film;

    public ProductItem(Film film, string language)
    {
        this.film = film;
        this.language = language;
    }
}