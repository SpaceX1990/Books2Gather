using System.Collections.ObjectModel;
using Books2Gather.Models;
using Books2Gather.Repository;

namespace Books2Gather.ViewModels;

internal class MainWindowViewModel : MainViewModel
{
    private readonly IRepository<Author> repository;

    public ObservableCollection<Author> Authors { get; set; }

    private Author selectedAuthor;

    public Author SelectedAuthor
    {
        get => selectedAuthor;
        set
        {
            if (selectedAuthor != value)
            {
                selectedAuthor = value;
                OnPropertyChanged(nameof(SelectedAuthor));
            }
        }
    }

    public MainWindowViewModel()
    {
        repository = new AuthorRepository();
        LoadAuthors();
    }

    private void LoadAuthors()
    {
        Authors = new ObservableCollection<Author>(repository.GetAll());
    }

    public void AddAuthor()
    {
        var newAuthor = new Author
        {
            FirstName = "Neuer", LastName = "Autor", BirthDate = new DateTime(2011, 3, 10), Nationality = "deutsch",
            Biography = "Bester Autor aller Zeiten"
        };
        repository.Insert(newAuthor);
        Authors.Add(newAuthor);
    }

    public void UpdateAuthor()
    {
        if (SelectedAuthor != null)
        {
            repository.Update(SelectedAuthor);
        }
    }

    public void DeleteAuthor()
    {
        if (SelectedAuthor != null)
        {
            repository.Delete(SelectedAuthor);
            Authors.Remove(SelectedAuthor);
        }
    }
}