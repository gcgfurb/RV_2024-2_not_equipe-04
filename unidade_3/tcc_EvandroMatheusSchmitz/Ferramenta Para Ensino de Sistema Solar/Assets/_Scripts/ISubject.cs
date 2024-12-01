public interface ISubject{

    /// <summary>
    /// The update method of the observer design patter.
    /// Adds an observer to this subject.
    /// </summary>
    /// <param name="observer">A concreteimplementation of IObserver</param>
    void addObserver(IObserver observer);

    /// <summary>
    /// The update method of the observer design patter.
    /// Removes an observer to this subject.
    /// </summary>
    /// <param name="observer">A concreteimplementation of IObserver</param>
    void removeObserver(IObserver observer);

    /// <summary>
    /// The update method of the observer design patter.
    /// Notify the observes.
    /// </summary>
    void notify();
}
