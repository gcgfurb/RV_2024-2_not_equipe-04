public interface IObserver
{
    /// <summary>
    /// The update method of the observer design patter.
    /// </summary>
    /// <param name="update">An UpdateData variable to inform the type of the update and any value that should be useful</param>
    void update(UpdateData update);

}