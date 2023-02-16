using System.Collections.Generic;

public class Catalog
{
    private List<Film> films;

    public int currentPage = 1;
    public int currentFilm = -1;
    public int lastpage;

    public Catalog()
    {
        films = FilmsManager.getInstance().getFilms();
        if (films.Count % 9 == 0)
            lastpage = films.Count / 9;
        else
            lastpage = films.Count / 9 + 1;
    }

    public int nextPage()
    {
        if (currentPage >= lastpage) currentPage = 1;
        else currentPage++;

        return currentPage;
    }

    public int previousPage()
    {
        if (currentPage <= 1) currentPage = lastpage;
        else currentPage--;

        return currentPage;
    }

    //при нажатие на фильм из каталога 
    public Film intoToFilm(int index)
    {
        currentFilm = index;
        return films[currentFilm];
    }

    public List<Film> toCatalog()
    {
        currentFilm = -1;
        return getPageConent();
    }

    private void calcCurrentPage()
    {
        if (currentFilm % 9 == 0)
            currentPage = currentFilm / 9;
        else
            currentPage = currentFilm / 9 + 1;
    }

    public int nextFilm()
    {
        if (currentFilm >= films.Count - 1) currentFilm = 0;
        else currentFilm++;
        calcCurrentPage();
        return currentFilm;
    }

    public int previousFilm()
    {
        if (currentFilm <= 0) currentFilm = films.Count - 1;
        else currentFilm--;
        calcCurrentPage();
        return currentFilm;
    }

    public List<Film> getPageConentIndex(int index)
    {
        if (index > lastpage)
            index = 0;
        else if (index < 0)
            index = lastpage;
        
        List<Film> filmsOnPage = new List<Film>();
        int startIndex = (index - 1) * 9;
        int endIndex = (index - 1) * 9 + 9;
        for (int i = startIndex; i < films.Count; i++)
        {
            if (i == endIndex)
                break;
            filmsOnPage.Add(films[i]);
        }

        return filmsOnPage;
    }

    public List<Film> getPagePreviusToNextPageConent()
    {
        List<Film> filmsOnThree = new List<Film>();
        films.AddRange(getPageConentIndex(currentPage - 1));
        int startIndex = (currentPage - 1) * 9;
        int endIndex = (currentPage - 1) * 9 + 9;
        for (int i = startIndex; i < films.Count; i++)
        {
            if (i == endIndex)
                break;
            filmsOnThree.Add(films[i]);
        }

        films.AddRange(getPageConentIndex(currentPage + 1));
        return filmsOnThree;
    } 
    public List<Film> getPageConent()
    {
        List<Film> filmsOnPage = new List<Film>();
        int startIndex = (currentPage - 1) * 9;
        int endIndex = (currentPage - 1) * 9 + 9;
        for (int i = startIndex; i < films.Count; i++)
        {
            if (i == endIndex)
                break;
            filmsOnPage.Add(films[i]);
        }

        return filmsOnPage;
    }
}