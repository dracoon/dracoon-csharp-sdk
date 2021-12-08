namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Informations about the current set avatar.
    /// </summary>
    public class AvatarInfo {

        /// <summary>
        ///     The unique id of the current set avatar image.
        /// </summary>
        public string AvatarUUID { get; set; }

        /// <summary>
        ///     Indicates if the current set avatar is a custom set image or the default server generated image.
        /// </summary>
        public bool IsCustomAvatar { get; set; }
    }
}