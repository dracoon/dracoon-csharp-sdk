namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Enumeration of algorithm states.
    /// </summary>
    public enum AlgorithmState {
        /// <summary>
        ///     The prefered algorithms.
        /// </summary>
        Required,
        /// <summary>
        ///     Algorithms flagged with this state shouldn't be used anymore.
        /// </summary>
        Discouraged
    }
}
