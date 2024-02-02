using System.Collections.Generic;

public interface IMonoHookManager
{
    /// <summary>
    /// Add a listener to the update loop
    /// </summary>
    /// <param name="update"></param>
    void AddUpdateListener(IUpdate update);

    /// <summary>
    /// Remove a listener from the update loop
    /// </summary>
    /// <param name="update"></param>
    void RemoveUpdateListener(IUpdate update);

    /// <summary>
    /// Add a listener to the fixed update loop
    /// </summary>
    /// <param name="update"></param>
    void AddFixedUpdateListener(IFixedUpdate update);

    /// <summary>
    /// Remove a listener from the fixed update loop
    /// </summary>
    /// <param name="update"></param>
    void RemoveFixedUpdateListener(IFixedUpdate update);

    void AddOnApplicationQuitListener(IOnApplicationQuit onApplicationQuit);

    void RemoveOnApplicationQuitListener(IOnApplicationQuit onApplicationQuit);

    /// <summary>
    /// Get an enumerator for the update loop
    /// </summary>
    /// <returns></returns>
    IEnumerable<IUpdate> UpdateEnumerator();

    /// <summary>
    /// Get an enumerator for the fixed update loop
    /// </summary>
    /// <returns></returns>
    IEnumerable<IFixedUpdate> FixedUpdateEnumerator();

    IEnumerable<IOnApplicationQuit> OnApplicationQuitEnumerator();
}