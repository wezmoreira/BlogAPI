namespace Blog.ViewModel;

public class ResultViewModel<T>
{
    public T Data { get; private set; }
    public List<string> Errors { get; private set; }

    public ResultViewModel(T data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }

    public ResultViewModel(T data)
    {
        Data = data;
    }

    public ResultViewModel(List<string> errors)
    {
        Errors = errors;
    }

    public ResultViewModel(string error)
    {
        Errors.Add(error);
    }
}