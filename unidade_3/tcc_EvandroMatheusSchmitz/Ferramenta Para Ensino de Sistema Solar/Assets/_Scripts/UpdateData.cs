using System;

/// <summary>
/// Actions used in the update method:
///  - Speed: a change in the speed of rotation or revolution;
///  - Information: change the information that will be displayed;
///  - Dissection: change the display;
///  - Size: change of size;
///  - Page: book page (not used);
///  - Reset: reset the book (not used).
/// </summary>
public enum Action { SPEED, INFORMATION, DISSECTION, SIZE, PAGE, RESET };

public class UpdateData {

    /// <summary>
    /// The type of the update. The action that show happen, or something that will be changed.
    /// </summary>
    private Action _updateType;
    /// <summary>
    /// The type of the update. The action that show happen, or something that will be changed.
    /// (get, set)
    /// </summary>
    public Action UpdateType
    {
        get { return _updateType; }
        set { _updateType = value; }
    }

    /// <summary>
    /// The data that will be used by the observers, to do the update.
    /// It is an object because this way all data can fit in it and a variable for every type of possible data is not required.
    /// </summary>
    private object _data;

    /// <summary>
    /// The data that will be used by the observers, to do the update.
    /// It is an object because this way all data can fit in it and a variable for every type of possible data is not required.
    /// (get, set)
    /// </summary>
    public object Data
    {
        get { return _data; }
        set { _data = value; }
    }
}
