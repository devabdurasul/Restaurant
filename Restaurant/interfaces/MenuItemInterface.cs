namespace Restaurant
{
    //TODO: Using IMenuItem is better name for the interface instead of IMenuItem
    public interface IMenuItem
    {
        //TODO: Obtain and Serve method should return itself (this)
        IMenuItem Obtain();
        IMenuItem Serve();
    }
}
