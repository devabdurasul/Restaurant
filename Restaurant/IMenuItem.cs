namespace Restaurant
{
    public interface IMenuItem
    {
        //TODO: You are not using these 2 methods. Serve method should called by server.Serve, Obtain method should be called by cook.Process.
        IMenuItem Obtain();
        IMenuItem Serve();
    }
}
