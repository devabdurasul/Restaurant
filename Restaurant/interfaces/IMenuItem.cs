namespace Restaurant
{
    public interface IMenuItem
    {
        IMenuItem Obtain();
        IMenuItem Serve();
    }
}
