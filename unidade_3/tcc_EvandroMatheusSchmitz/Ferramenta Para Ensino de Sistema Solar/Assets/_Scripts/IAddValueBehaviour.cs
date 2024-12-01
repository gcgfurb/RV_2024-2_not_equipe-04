
public interface IAddValueBehaviour
{

    /// <summary>
    /// It is used to increse or decrese an atribute value.
    /// </summary>
    /// <param name="add">The value to be add if posite or decreased if negative</param>
    void AddValue(float add);

    /// <summary>
    /// If the concrete class has any interation with the user, use this method to show something to him
    /// </summary>
    void ShowData();

    /// <summary>
    /// If the concrete class has any interation with the user, use this method to hide something shown by the ShowData method
    /// </summary>
    void HideData();
	
}
